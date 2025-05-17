using TravelGPT.Server.Extensions;

namespace TravelGPT.Server;

internal class Bootstrapper
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddDirectChat()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddRouting()
            .AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(options => options
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed(origin => true)
            );
        }

        app.MapControllers();

        app.Run();
    }
}

