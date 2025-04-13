using TravelGPT.Server.Models.Chat;

namespace TravelGPT.Server.Observers.Chat;

public class ChatResponseTaskObserver(TaskCompletionSource<IChatMessageContext> source) : IObserver<IChatMessageContext>
{
    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(IChatMessageContext context) => source.SetResult(context);
}

