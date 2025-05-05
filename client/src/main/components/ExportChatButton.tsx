
interface Props {
  messages: string[]
  chatId: string | undefined;
}

function ExportChatButton({ messages, chatId }: Props) {
  const exportChat = () => {
    const text = messages.map((message, index) => {
      const sender = index % 2 === 0 ? "Assistent" : "Me";
      return `${sender}: ${message}`;
    }).join("\n\n");
    const blob = new Blob([text], { type: "text/plain" });
    const url = URL.createObjectURL(blob);
    const link = document.createElement("a");
    link.href = url;
    link.download = "chat-history-" + chatId + ".txt";
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    URL.revokeObjectURL(url);
  }
  return (
    <button onClick={exportChat} className="px-2 py-1 text-sm bg-gray-600 rounded hover:bg-gray-500">
      Export Chat
    </button>
  );
}

export default ExportChatButton;