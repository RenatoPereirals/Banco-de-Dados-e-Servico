using Bsd.API.Helpers;
using Bsd.API.Middlewares;

using Bsd.Application.Helpers.Interfaces;
using Bsd.Application.Interfaces;
using Bsd.Application.Services;
using Bsd.Application.Helpers;

using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Service.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Persistence.RepositoryImpl;
using Bsd.Domain.Service;
using Bsd.Domain.Services;

using Bsd.Infrastructure.Context;
using Bsd.Infrastructure.RepositoryImpl;

using Microsoft.EntityFrameworkCore;

using Serilog;

// Configuração do Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    // Adicionar o Serilog ao builder
    builder.Host.UseSerilog();

    // Configuração do DbContext
    builder.Services.AddDbContext<BsdDbContext>(options =>
    options.UseSqlite(builder.Configuration
        .GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string not found.")));

    // Configuração dos serviços
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<IBsdService, BsdService>();
    builder.Services.AddScoped<IRubricService, RubricService>();
    builder.Services.AddScoped<IDayTypeChecker, DayTypeChecker>();
    builder.Services.AddScoped<IHolidayChecker, HolidayChecker>();
    builder.Services.AddScoped<IVariableDateHolidayAdjuster, VariableDateHolidayAdjuster>();

    builder.Services.AddScoped<IGeralRepository, GeralRepository>();
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<IBsdRepository, BsdRepository>();
    builder.Services.AddScoped<IRubricRepository, RubricRepository>();

    builder.Services.AddScoped<IBsdApplicationService, BsdApplicationService>();
    builder.Services.AddScoped<IEmployeeApplicationService, EmployeeApplicationService>();
    builder.Services.AddScoped<IRubricApplicationService, RubricApplicationService>();

    builder.Services.AddScoped<IEmployeeValidationService, EmployeeValidationService>();
    builder.Services.AddScoped<IDateHelper, DateHelper>();

    // Configuração dos controllers e Swagger
    builder.Services.AddControllersWithViews();
    builder.Services.AddControllers();
    IServiceCollection serviceCollection = builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    // Configurações de ambiente de desenvolvimento

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            // Adicionar mais versões ou ajustar conforme necessário
        });

        // Página detalhada de exceções para desenvolvimento
        app.UseDeveloperExceptionPage();
    }

    // Middleware
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseMiddleware<LoggingMiddleware>();


    // Configurações gerais
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.Information("Server shutting down...");
    Log.CloseAndFlush();
}
