import { useEffect, useState } from "react";
import Input from "./components/Input";
import MessageList from "./components/MessageList";
import ExportChatButton from "./components/ExportChatButton";
import NewChatButton from "./components/NewChatButton";
import { createChat, sendChatMessage } from "./functions/chat/server";
import { ChatId, ChatMessageText } from "./types/chat";

function App() {
  const [disconnected, setDisconnected] = useState<boolean>(true);
  const [messages, setMessages] = useState<ChatMessageText[]>([]);
  const [chatId, setChatId] = useState<ChatId>();

  const handleSendMessage = async (input: ChatMessageText) => {
    setMessages([...messages, input]);
    const response = await sendChatMessage(chatId!, {
      text: input
    });
    setMessages([
      ...messages,
      input,
      response.text,
    ]);
  };

  const initChat = async () => {
    try {
      const { id, text } = await createChat({
        text: "We're starting a conversation. Greet me ONLY THIS TIME AND NEVER AGAIN depending on the time of day it is, current time: " +
              new Date().getHours() +
              " hours. And then ask where I want to travel?"
      });
      setChatId(id);
      setMessages((prev) => [...prev, text]);
      setDisconnected(false);
    } catch {
      /* empty */
    }
  };

  useEffect(() => {
    initChat();
  }, []);

  const createNewChat = () => {
    setMessages([]);
    setChatId(undefined);
    initChat();
  };

  return (
    <div className="scheme-light dark:scheme-dark flex flex-col gap-4 w-screen h-screen overflow-auto bg-neutral-800 text-white relative">
      <div className="absolute top-4 right-4 flex flex-col gap-2">
        <NewChatButton onNewChat={createNewChat}></NewChatButton>
        <ExportChatButton
          messages={messages}
          chatId={chatId}
        ></ExportChatButton>
        {/* <DarkModeToggle></DarkModeToggle> */}
      </div>
      <div className="flex-1 overflow-auto">
        <div className="flex flex-col h-full overflow-auto max-w-5xl mx-auto relative">
          <div className="text-center m-16">
            <h1 className="m-1 text-5xl font-bold">Travel GPT 🗺️</h1>
            <p className="m-1">Your personal travel assistant</p>
          </div>
          <MessageList messages={messages} />
          <Input
            onSendMessage={handleSendMessage}
            disconnected={disconnected}
          />
        </div>
      </div>
    </div>
  );
}

export default App;
