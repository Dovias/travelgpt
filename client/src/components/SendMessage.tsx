import { useState } from "react";

// Aš, kaip sistemos vartotojas, norėčiau turėti galimybę išsiųsti pranešimą asistentui sistemoje. TGPT-10
function SendMessage() {
    const [input, setInput] = useState("");
    
    const submitMessage = async () => {
        const messageData = {
            message: input
        }
        try {
            const response = await fetch('idk', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(messageData)
            })
            const result = await response.json();
            console.log(result)
            setInput("");
        } catch (error) {
            console.error(error)
        }
    }
    return (
        <div className="w-full p-4 bg-neutral-900 flex justify-center">
        <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          onKeyDown={(event) => event.key === "Enter" && submitMessage()}
          placeholder="Type a message..."
          className="w-1/2 p-3 rounded-lg bg-neutral-700 text-white border border-neutral-600 focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <button
          onClick={submitMessage}
          className="p-3 bg-blue-500 text-white rounded-lg hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 ml-[0.%]"
        >
          Send
        </button>
      </div>
    )
}

export default SendMessage;