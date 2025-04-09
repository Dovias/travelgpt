using TravelGPT.Models.Chat;
using TravelGPT.Models.Chat.InMemory;

namespace TravelGPT;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddSingleton<IChatService, InMemoryChatService>(provider => new InMemoryChatService(new Dictionary<int, IChatContext>(), 0))
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

