import { useMemo, useEffect, useState } from "react";

type SentChatMessageRequest = {
  text: string;
}

type SentChatMessageResponse = SentChatMessageRequest

type ChatCreationResponse = {
  id: number
};

function App() {
  const [messages, setMessages] = useState<string[]>([]);
  const [input, setInput] = useState("");
  const [chatId, setChatId] = useState<number>();
  useMemo(async () => {
    const response = await fetch("http://localhost:5198/api/v1/chat", { method: "POST" });
    setChatId(((await response.json()) as ChatCreationResponse).id);
  }, [])

  console.log(chatId)
  console.log(`Successfully retrieved chat id from the server: ${chatId}`)

  useEffect(() => {
    if (!chatId) return;
    setMessages(["Hello! 👋 Where would you like to travel today?"]);
  }, [chatId]);

  const sendMessage = async () => {
    if (!input.trim()) return;
  
    setMessages([...messages, input]);
    setInput("");
  
    const modifiedInput = input + "You are a travel guide, help me plan my trip based on my input. Respond in 3-5 points, in a short message. You will ask these things in order, one after another: Firstly i start convesation by telling where (like country, city or route (like along seaside) i would like to visit), you will ask for how long, then based in my inputs provided give 3 reccomendations where to stay and ask if i want reccomendations on: some places to visit, after that - to eat. after that ask if i have any more questions, also all answers where you answer with places, hotels, ect must be bulletpoints, in new like and if i ask something not by plan, just repeat the question untill i answer correctly.";
  
    const response = await fetch(`http://localhost:5198/api/v1/chat/${chatId}`, {
      method: "POST",
      body: JSON.stringify({
        text: modifiedInput
      } as SentChatMessageRequest),
      headers: {
        "Content-Type": "application/json",
      }
    });
  
    setMessages([...messages, input, ((await response.json()) as SentChatMessageResponse).text]);
  };

  return (
    <div className="scheme-dark flex flex-col gap-4 w-screen h-screen overflow-auto bg-neutral-800 text-white">
      <div className="flex-1 overflow-auto">
        <div className="flex flex-col h-full overflow-auto max-w-5xl mx-auto">
          <div className="text-center m-16">
            <h1 className="m-1 text-5xl font-bold">Travel GPT 🗺️</h1>
            <p className="m-1">Your personal travel assistant</p>
          </div>
          <div className="flex flex-col gap-4 overflow-auto p-8">
            {messages.map((message, index) => (
              <div
                key={index}
                className={`w-full ${index % 2 === 0 ? "text-left [&>*]:bg-gray-700" : "text-right [&>*]:bg-blue-500"}`}
              >
                <div
                  className={`inline-block max-w-lg p-3 rounded-xl text-white text-left`}
                >
                  <p className="whitespace-pre-wrap break-words">{message}</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
      <div className="w-full p-4 bg-neutral-900">
        <div className="max-w-5xl m-auto flex gap-4">
          <input
            type="text"
            value={input}
            onChange={(e) => setInput(e.target.value)}
            onKeyDown={(e) => e.key === "Enter" && sendMessage()}
            placeholder="Type where you would like to go..."
            className="w-full p-3 rounded-lg bg-neutral-700 text-white border border-neutral-600 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <button
            onClick={sendMessage}
            className="p-3 bg-blue-500 text-white rounded-lg hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 ml-[0.%]"
          >
            Send
          </button>
        </div>
      </div>
    </div>
  );
}

export default App;