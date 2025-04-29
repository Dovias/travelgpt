using System.Reactive;
using Moq;
using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.Chat.InMemory;

namespace TravelGPT.Server.Tests;

[TestClass]
public class InMemoryChatTest
{
    [TestMethod]
    public void TestChatAdd()
    {
        int? generatedId = default;
        InMemoryChat chat = new(
            new Dictionary<int, IChatMessage>(),
            [],
            (id, details) =>
            {
                generatedId = id;
                return new Mock<IChatMessage>().Object;
            },
            (messages, message) => new Mock<IChatMessageContext>().Object
        )
        { Id = 0 };

        chat.Add(new Mock<IChatMessageDetails>().Object);
        Assert.IsNotNull(generatedId);
    }

    [TestMethod]
    public void TestChatRemove()
    {
        IChatMessage mock = new Mock<IChatMessage>().Object;
        InMemoryChat chat = new(
            new Dictionary<int, IChatMessage>() {
                { 0, mock }
            },
            [],
            (id, details) => mock,
            (messages, message) => new Mock<IChatMessageContext>().Object
        )
        { Id = 0 };

        Assert.IsTrue(chat.Remove(0));
        Assert.IsFalse(chat.TryGet(0, out IChatMessage? removedMessage));
        Assert.IsNull(removedMessage);

        Assert.IsFalse(chat.Remove(1));
    }

    [TestMethod]
    public void TestChatTryGetValue()
    {
        IChatMessage mock = new Mock<IChatMessage>().Object;
        InMemoryChat chat = new(
            new Dictionary<int, IChatMessage>()
            {
                { 0, mock }
            },
            [],
            (id, details) => mock,
            (messages, message) => new Mock<IChatMessageContext>().Object
        )
        { Id = 0 };

        Assert.IsTrue(chat.TryGet(0, out IChatMessage? existantMessage));
        Assert.IsFalse(chat.TryGet(1, out IChatMessage? nullMessage));

        Assert.IsNotNull(existantMessage);
        Assert.IsNull(nullMessage);
    }


    [TestMethod]
    public void TestChatContains()
    {
        IChatMessage mock = new Mock<IChatMessage>().Object;
        InMemoryChat chat = new(
            new Dictionary<int, IChatMessage>()
            {
                { 0, mock }
            },
            [],
            (id, details) => mock,
            (messages, message) => new Mock<IChatMessageContext>().Object
        )
        { Id = 0 };

        Assert.IsTrue(chat.Contains(0));
        Assert.IsFalse(chat.Contains(1));
    }

    [TestMethod]
    public void TestChatIndexer()
    {
        IChatMessage mock = new Mock<IChatMessage>().Object;
        InMemoryChat chat = new(
            new Dictionary<int, IChatMessage>()
            {
                { 0, mock }
            },
            [],
            (id, details) => mock,
            (messages, message) => new Mock<IChatMessageContext>().Object
        )
        { Id = 0 };

        Assert.AreEqual(chat[0], mock);
        Assert.ThrowsException<KeyNotFoundException>(() => chat[1]);
    }

    [TestMethod]
    public void TestChatSubscribe()
    {
        ICollection<IObserver<IChatMessageContext>> observers = [];
        InMemoryChat chat = new(
            new Dictionary<int, IChatMessage>(),
            observers,
            (id, details) => new Mock<IChatMessage>().Object,
            (messages, message) => new Mock<IChatMessageContext>().Object
        )
        { Id = 0 };

        IObserver<IChatMessageContext> observer = Observer.Create<IChatMessageContext>(context => { });
        IDisposable disposable = chat.Subscribe(observer);
        CollectionAssert.Contains((System.Collections.ICollection?)observers, observer);
        disposable.Dispose();
        CollectionAssert.DoesNotContain((System.Collections.ICollection?)observers, observer);
    }

    [TestMethod]
    public void TestChatGetEnumerator()
    {
        int generatedId = default;
        InMemoryChat chat = new(
            new Dictionary<int, IChatMessage>(),
            [],
            (id, details) =>
            {
                generatedId = id;
                return new Mock<IChatMessage>().Object;
            },
            (messages, message) => new Mock<IChatMessageContext>().Object
        )
        { Id = 0 };

        Assert.IsFalse(chat.GetEnumerator().MoveNext());

        IChatMessage message = chat.Add(new Mock<IChatMessageDetails>().Object);
        IEnumerator<IChatMessage> messages = chat.GetEnumerator();
        Assert.IsTrue(messages.MoveNext());
        Assert.AreEqual(messages.Current, message);

        chat.Remove(generatedId);
        Assert.IsFalse(chat.GetEnumerator().MoveNext());
    }
}