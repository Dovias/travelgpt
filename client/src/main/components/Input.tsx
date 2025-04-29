import { useState } from "react";

interface Props {
  onSendMessage: (message: string) => void;
  disconnected: boolean;
}

function Input({ onSendMessage, disconnected }: Props) {
  const [input, setInput] = useState("");
  const sendMessage = () => {
    if (!input.trim()) return;
    onSendMessage(input);
    setInput("");
  };

  return (
    <div className="rounded-lg m-4 p-4 bg-neutral-900 absolute bottom-0 left-0 right-0">
      <div className="max-w-5xl m-auto flex gap-4">
        <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && sendMessage()}
          placeholder={disconnected ? "TravelGPT server is currently unavailable." : "Type where you would like to go..."}
          className="w-full p-3 rounded-lg bg-neutral-700 text-white border border-neutral-600 focus:outline-none focus:ring-2 focus:ring-blue-500"
          disabled={disconnected}
        />
        <button
          onClick={sendMessage}
          className="p-3 bg-blue-500 text-white rounded-lg hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 ml-[0.%]"
          disabled={disconnected}
        >
          Send
        </button>
      </div>
    </div>
  );
}

export default Input;
