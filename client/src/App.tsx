import { useEffect, useState } from "react";
import Input from "./components/Input";
import MessageList from "./components/MessageList";
import {
  ChatCreationResponse,
  SentChatMessageResponse,
} from "./types/chatTypes";
import fetchMessage from "./functions/fetchMessage";

function App() {
  const [messages, setMessages] = useState<string[]>([]);
  const [chatId, setChatId] = useState<number>();

  const handleSendMessage = async (input: string) => {
    setMessages([...messages, input]);
    const response = await fetchMessage(input, chatId);
    setMessages([
      ...messages,
      input,
      ((await response.json()) as SentChatMessageResponse).text,
    ]);
  };

  const createChat = async () => {
    const response = await fetch("http://localhost:5198/api/v1/chat", {
      method: "POST",
    });
    const data = (await response.json()) as ChatCreationResponse;
    setChatId(data.id);
    console.log(`Successfully retrieved chat id from the server: ${data.id}`);
    return data.id;
  };

  useEffect(() => {
    const initChat = async () => {
      const tempChatId = await createChat();
      const response = await fetchMessage(
        "We're starting a conversation. Greet me ONLY THIS TIME AND NEVER AGAIN depending on the time of day it is, current time: " +
          new Date().getHours() +
          " hours.",
        tempChatId
      );
      setMessages([
        ...messages,
        ((await response.json()) as SentChatMessageResponse).text,
      ]);
    };

    initChat();
  }, []);

  return (
    <div className="scheme-dark flex flex-col gap-4 w-screen h-screen overflow-auto bg-neutral-800 text-white">
      <div className="flex-1 overflow-auto">
        <div className="flex flex-col h-full overflow-auto max-w-5xl mx-auto relative">
          <div className="text-center m-16">
            <h1 className="m-1 text-5xl font-bold">Gemini.NET 🗺️</h1>
            <p className="m-1">Gemini LLM wrapper in React and ASP.NET!</p>
          </div>
          <MessageList messages={messages}></MessageList>
          <Input onSendMessage={handleSendMessage}></Input>
        </div>
      </div>
    </div>
  );
}

export default App;
