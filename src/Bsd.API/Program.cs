using Bsd.API.Helpers;
using Bsd.Application.Helpers.Interfaces;
using Bsd.Application.Interfaces;
using Bsd.Application.Services;

using Bsd.Domain.Entities;
using Bsd.Domain.Persistence.RepositoryImpl;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Service;
using Bsd.Domain.Service.Interfaces;
using Bsd.Domain.Services;
using Bsd.Domain.Services.Interfaces;
using Bsd.Infrastructure.Context;
using Bsd.Infrastructure.Data;
using Bsd.Infrastructure.RepositoryImpl;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BsdDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddTransient<EmployeeSeeder>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IBsdService, BsdService>();
builder.Services.AddScoped<IRubricService, RubricService>();
builder.Services.AddScoped<IDayTypeChecker, DayTypeChecker>();
builder.Services.AddScoped<IHoliDayChecker, HoliDayChecker>();
builder.Services.AddScoped<IVariableDateHolidayAdjuster, VariableDateHolidayAdjuster>();

builder.Services.AddScoped<IGeralRepository, GeralRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IBsdRepository, BsdRepository>();
builder.Services.AddScoped<IRubricRepository, RubricRepository>();

builder.Services.AddScoped<IBsdApplicationService, BsdApplicationService>();
builder.Services.AddScoped<IEmployeeApplicationService, EmployeeApplicationService>();
builder.Services.AddScoped<IRubricApplicationService, RubricApplicationService>();

builder.Services.AddScoped<IEmployeeValidationService, EmployeeValidationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
