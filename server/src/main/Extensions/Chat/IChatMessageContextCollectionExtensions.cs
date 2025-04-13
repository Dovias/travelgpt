using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Observers.Chat;

namespace TravelGPT.Server.Extensions.Chat;

public static class IChatMessageContextCollectionExtensions
{
    public static async Task<IChatMessageContext> Wait(this IChatMessageContextCollection collection)
    {
        TaskCompletionSource<IChatMessageContext> source = new();
        IDisposable disposable = collection.Subscribe(new ChatResponseTaskObserver(source));

        IChatMessageContext context = await source.Task;
        disposable.Dispose();

        return context;
    }
}
