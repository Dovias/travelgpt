using TravelGPT.Models.Chat;
using TravelGPT.Observers.Chat;
using TravelGPT.Services.Chat;

namespace TravelGPT;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddSingleton<IChatService, ChatInMemoryService>(provider => new ChatInMemoryService(id =>
                new InMemoryChat(
                    new HashSet<IObserver<MessageContext>>() {
                        new GeminiChatObserver(new HttpClient(), builder.Configuration["GeminiApiKey"]!) { Id = Guid.NewGuid() }
                    },
                    []
                )
                { Id = id }
            ))
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

