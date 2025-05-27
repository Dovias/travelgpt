import { JSX } from "react";

interface Props {
  messages: string[];
}

function parseMessage(message: string): JSX.Element[] {
  return message.split("\n").map((line, idx) => {
    const parts = line.split(/(\*\*[^*]+\*\*)/g).map((part, i) => {
      if (/^\*\*[^*]+\*\*$/.test(part)) {
        return <strong key={i}>{part.replace(/^\*\*|\*\*$/g, "")}</strong>;
      }
      return <span key={i}>{part}</span>;
    });

    return (
      <p key={idx}>
        {parts}
      </p>
    );
  });
}

function MessageList({ messages }: Props) {
  return (
    <div className="flex flex-col gap-4 overflow-auto p-8">
      {messages.map((message, index) => (
        <div
          key={index}
          className={`w-full even:text-right even:[&>*]:bg-blue-500 odd:[&>*]:bg-gray-700`}
        >
          <div className="inline-block max-w-lg p-3 rounded-xl text-white text-left whitespace-pre-wrap break-words">
            {parseMessage(message)}
          </div>
        </div>
      ))}
    </div>
  );
}

export default MessageList;
