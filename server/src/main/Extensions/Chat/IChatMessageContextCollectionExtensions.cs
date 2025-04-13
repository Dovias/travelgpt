using TravelGPT.Models.Chat;
using TravelGPT.Observers.Chat;

namespace TravelGPT.Extensions.Chat;

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
