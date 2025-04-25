using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.Chat.InMemory;

namespace TravelGPT.Server;

internal class Bootstrapper
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddSingleton<IChatRepository, InMemoryChatRepository>(provider =>
                new InMemoryChatRepository(new Dictionary<Guid, IChat>(), id =>
                    new InMemoryChat(
                            new Dictionary<int, IChatMessage>(),
                            [],
                            (id, details) => new InMemoryChatMessage
                            {
                                Id = id,
                                Author = new InMemoryChatParticipant
                                {
                                    Id = details.Author.Id
                                },

                                Text = details.Text,
                                Created = DateTime.Now
                            },
                            (messages, message) => new InMemoryChatMessageContext
                            {
                                Chat = messages,
                                Message = message
                            }
                    )
                    { Id = id }
                )
            )
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddRouting(options => { options.LowercaseUrls = true; })
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

