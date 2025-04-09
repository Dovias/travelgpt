using TravelGPT.Models.Chat;

namespace TravelGPT.Observers.Chat;

public class ChatResponseTaskObserver(TaskCompletionSource<IUserChatMessageContext> source) : IObserver<IUserChatMessageContext>
{
    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(IUserChatMessageContext context) => source.SetResult(context);
}

