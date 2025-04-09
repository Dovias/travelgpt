using TravelGPT.Models.Chat;

namespace TravelGPT.Observers.Chat;

public class ChatResponseTaskObserver(TaskCompletionSource<IChatMessageContext> source) : IObserver<IChatMessageContext>
{
    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(IChatMessageContext context) => source.SetResult(context);
}

