using TravelGPT.Server.Factories.Chat;
using TravelGPT.Server.Formatters;
using TravelGPT.Server.Models.Llm.Gemini;
using TravelGPT.Server.Repositories.Chat;
using TravelGPT.Server.Services;

namespace TravelGPT.Server;

internal class Bootstrapper
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddSingleton(new ChatService(new InMemoryChatRepository(), new TravelChatConversationResponseFactory(
                new GeminiLlmClient(builder.Configuration["GEMINI_API_KEY"] ?? throw new KeyNotFoundException("Missing Gemini API key"))
            )))
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddRouting()
            .AddControllers(options => options.InputFormatters.Add(new PlainTextSingleValueFormatter()));

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

