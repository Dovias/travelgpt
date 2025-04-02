using TravelGPT.Models.Chat;

namespace TravelGPT.Extensions.Chat;

public static class IChatExtensions
{
    private class ChatResponseTaskObserver(TaskCompletionSource<Message> source) : IObserver<MessageContext>
    {
        private readonly TaskCompletionSource<Message> _source = source;

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(MessageContext context) => _source.SetResult(context.Message);
    }

    public static async Task<Message> SendAndWaitForMessage(this IChat chat, Message message)
    {
        // Do not change the execution order of this method!
        // Message should always be added before subscribing for response message chat events
        chat.AddMessage(message);

        var source = new TaskCompletionSource<Message>();
        var observer = chat.Subscribe(new ChatResponseTaskObserver(source));

        message = await source.Task;
        observer.Dispose();

        return message;
    }
}