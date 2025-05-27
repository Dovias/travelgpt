import { JSX } from "react";

interface Props {
  messages: string[];
}

function parseMessage(message: string): JSX.Element[] {
  return message.split("\n").map((line, idx) => {
    const parts = line.split(/(\[\[[^\]]+\]\]|\{\{[^}]+\}\}|\*\*[^*]+\*\*)/g).map((part, i) => {
    //const parts = line.split(/(\[\[[^\]]+\]\]|\{\{[^}]+\}\})|\*\*[^*]+\*\*/g).map((part, i) => {

      if (/^\*\*[^*]+\*\*$/.test(part)) {
        return <strong key={i}>{part.replace(/^\*\*|\*\*$/g, "")}</strong>;
      }

      if (/^\[\[[^\]]+\]\]$/.test(part)) {
        const url = part.slice(2, -2);
        return (
          <a key={i} href={url} target="_blank" className="underline text-blue-400 hover:text-blue-300">
            Link
          </a>
        );
      }

      if (/^\{\{[^}]+\}\}$/.test(part)) {
        const url = part.slice(2, -2);
        return (
          <a key={i} href={url} target="_blank" className="underline text-green-400 hover:text-green-300">
            Location
          </a>
        );
      }

      // [[link]] for websites, {{link}} for locations, will need to be implemented in backend, here it's done.
      return <span key={i}>{part}</span>;     
    });

    return (
      <p key={idx} className="whitespace-pre-wrap break-words">
        {parts}
      </p>
    );
  });
}
function MessageList({ messages }: Props) {
  return (
    <div className="flex flex-col gap-4 overflow-auto h-full p-8">
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
