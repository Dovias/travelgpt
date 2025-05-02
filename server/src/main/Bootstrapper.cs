using TravelGPT.Server.Extensions;
using TravelGPT.Server.Models.Chat.Llm;
using TravelGPT.Server.Models.Chat.Response;

namespace TravelGPT.Server;

internal class Bootstrapper
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddDirectServerChatRepository([
                new LlmDirectServerChatResponseStep() {
                    Client = new GeminiLlmClient() {
                        HttpClient = new HttpClient(),
                        ApiKey = builder.Configuration["GeminiApiKey"]
                            ?? throw new KeyNotFoundException("Missing Gemini API key")
                    },
                    Instructions = []
                }

            ])
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
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

