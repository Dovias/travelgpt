import { useState } from "react";

function App() {
  const [messages, setMessages] = useState<{ text: string; sender: "user" | "bot" }[]>([]);
  const [input, setInput] = useState("");

  const sendMessage = () => {
    if (!input.trim()) return;

    const userMessage: { text: string; sender: "user" | "bot" } = { text: input, sender: "user" };

    const botMessage: { text: string; sender: "user" | "bot" } = {
      text: `Message length: ${input.length} \ncharacters. Content: ${input}`,
      sender: "bot",
    };

    setMessages([...messages, userMessage, botMessage]);
    setInput("");
  };

  return (
    <div className="w-screen h-screen flex flex-col justify-between bg-neutral-800 text-white">
      <div className="text-center">
        <h1 className="m-1 text-5xl font-bold">TravelGPT 🗺️</h1>
        <p className="m-1">Hello world from the TravelGPT client!</p>
      </div>

      <div className="flex flex-col-reverse overflow-y-auto flex-grow px-6 py-4 space-y-4">
        {messages.slice().reverse().map((msg, index) => (
          <div
            key={index}
            className={`flex w-full ${msg.sender === "user" ? "justify-end" : "justify-start"}`}
          >
            <div
              className={`p-3 max-w-[50%] rounded-xl ${
                msg.sender === "user"
                  ? "bg-blue-500 text-white ml-[10%] mr-[15%]" // User
                  : "bg-gray-700 text-white mr-[10%] ml-[15%]" // Bot
              }`}
            >
              <p className="whitespace-pre-wrap break-words">{msg.text}</p>
            </div>
          </div>
        ))}
      </div>

      <div className="w-full p-4 bg-neutral-900 flex justify-center">
        <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && sendMessage()}
          placeholder="Type a message..."
          className="w-1/2 p-3 rounded-lg bg-neutral-700 text-white border border-neutral-600 focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <button
          onClick={sendMessage}
          className="p-3 bg-blue-500 text-white rounded-lg hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 ml-[0.%]"
        >
          Send
        </button>
      </div>
    </div>
  );
}

export default App;
