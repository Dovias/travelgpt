using TravelGPT.Models.Chat;
using TravelGPT.Models.Chat.InMemory;
using TravelGPT.Services.Chat;

namespace TravelGPT;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddSingleton<IChatService, ChatInMemoryService>(provider => new ChatInMemoryService(id => new InMemoryChat(
                new Dictionary<int, IUserChatContext>(),
                new Dictionary<int, IUserChatMessageContext>(),
                new HashSet<IObserver<IUserChatMessageContext>>()
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

