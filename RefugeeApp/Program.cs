using Microsoft.EntityFrameworkCore;
using RefugeeApp.Data;
using RefugeeApp.Repositories;
using RefugeeApp.Services;





var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RefugeeContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRefugeeRepository, RefugeeRepository>();
builder.Services.AddScoped<IFamilyRepository, FamilyRepository>();
builder.Services.AddScoped<IResidenceRepository, ResidenceRepository>();

builder.Services.AddScoped<IRefugeeService, RefugeeService>();
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IResidenceService, ResidenceService>();
// Add services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // 👈 viser stacktrace i browser/Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.MapControllers();

DbInitializer.Seed(app);

app.Run();