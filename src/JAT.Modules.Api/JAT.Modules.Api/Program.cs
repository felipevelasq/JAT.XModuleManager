using System.Net;
using JAT.Modules.Api;
using JAT.Modules.Application;
using JAT.Modules.Infrastructure;
using Microsoft.AspNetCore.Mvc;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        // TODO: Add health checks

        // Add db context and other services
        builder.Services.AddApplicationServices()
            .AddInfrastructureServices(builder.Configuration)
            .AddMediatrConfigs();

        builder.Services.AddSwaggerGen();

        builder.Services.AddAuth(builder.Configuration);

        builder.Services.AddProblemDetails();

        var app = builder.Build();
        
        // Apply db context updates (migrations)
        app.Services.ApplyContextUpdates();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseExceptionHandler();

        app.UseStatusCodePages(async context =>
        {
            if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                    Status = (int)HttpStatusCode.Unauthorized,
                    Title = "Unauthorized",
                    Detail = "You are not authorized to access this resource.",
                };
                await context.HttpContext.Response.WriteAsJsonAsync(problemDetails);
            }
        });

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}