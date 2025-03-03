using JAT.IdentityService.Api;
using JAT.IdentityService.Application;
using JAT.IdentityService.Infrastructure;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationServices()
            .AddInfrastructureServices(builder.Configuration)
            .AddMediatrConfigs();

        builder.Services.AddControllers();
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddProblemDetails();

        var app = builder.Build();
        
        app.Services.ApplyContextUpdates();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseExceptionHandler();

        app.UseRouting();

        app.MapControllers();

        app.Run();
    }
}