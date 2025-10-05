using System.Text.Json;
using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using Serilog;
using SpaceHouse.Application;
using SpaceHouse.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithThreadId()
                .Enrich.WithMachineName()
                .WriteTo.Console();

Log.Logger = loggerConfiguration.CreateLogger();


try
{
    Log.Information("ENVIRONNEMENT ACTIF : {Env}", environment);
    Log.Logger.Information("Démarrage de du web service Kudikila...");

    builder.Host.UseSerilog();


    builder.Services.AddControllers()
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                     options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Ignore cycles in JSON serialization
                     options.JsonSerializerOptions.WriteIndented = true;
                 });

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddOpenApiDocument(settings =>
    {
        settings.Title = "API Documentation";
        settings.Version = "v1";
        settings.DocumentName = "v1";
        settings.PostProcess = document =>
        {
            document.Info.Description = "API Du Defis Nasa Logicielle";
        };
    });


    // Enregistrement des services des autres couches
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);


    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowMyFrontend",
            policy =>
            {
                policy.WithOrigins("https://kudikilafront.onrender.com", "http://localhost:4200")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                policy.SetPreflightMaxAge(TimeSpan.FromMinutes(10));
            });
    });


    builder.Services.AddOpenApi();

    var app = builder.Build();

    app.UseOpenApi(settings =>
    {
        settings.Path = "/openapi/v1.json"; // Specify the path for the OpenAPI document
        settings.DocumentName = "v1"; // Specify the document name
    }); 


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors("AllowMyFrontend");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();



    app.Map("/", context =>
    {
        context.Response.Redirect("/scalar");
        return Task.CompletedTask;
    });

    app.MapGet("/healthz", () => Results.Ok("SpaceHouse API is running!"))
        .WithName("HealthCheck")
        .WithTags("Health");



    app.Run();
}
catch
{
    Log.Fatal("Application start-up failed");
    throw;
}
finally
{
    Log.Information("Application started successfully");
    Log.CloseAndFlush();
    Log.Information("Application stopped");
}