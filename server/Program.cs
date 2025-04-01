using TravelGPT.Services.Chat;
using TravelGPT.Services.Chat.Gemini;
using static TravelGPT.Services.Chat.IChat;

namespace TravelGPT;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddSingleton<IChatService, ChatInMemoryService>(provider => new ChatInMemoryService(id => new InMemoryChat(new HashSet<Action<MessageContext>>() { GeminiChat.ReplyToMessage }) { Id = id }))
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

