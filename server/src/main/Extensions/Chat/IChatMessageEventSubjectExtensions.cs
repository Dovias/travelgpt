using System.Reactive.Subjects;
using TravelGPT.Server.Models.Chat;

namespace TravelGPT.Server.Extensions.Chat;

public static class IChatMessageEventSubjectExtensions
{
    public static void OnNext(this ISubject<ChatMessageEvent> subject, ChatContext chat, ChatMessageContext message)
    {
        subject.OnNext(new ChatMessageEvent()
        {
            Chat = chat,
            Message = message
        });
    }
}
