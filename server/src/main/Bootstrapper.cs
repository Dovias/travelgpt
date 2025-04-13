using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.Chat.InMemory;

namespace TravelGPT.Server;

internal class Bootstrapper
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddSingleton<IChatService, InMemoryChatService>(provider => new InMemoryChatService(new Dictionary<int, IChatContext>()))
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddDistributedMemoryCache()
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
            })
            .AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.Run();
    }
}

