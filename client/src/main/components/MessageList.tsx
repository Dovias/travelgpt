interface Props {
  messages: string[];
}

function MessageList({ messages }: Props) {
  return (
    <div className="flex flex-col gap-4 overflow-auto p-8">
      {messages.map((message, index) => (
        <div
          key={index}
          className={`w-full even:text-right even:[&>*]:bg-blue-500 odd:[&>*]:bg-gray-700`}
        >
          <div
            className={`inline-block max-w-lg p-3 rounded-xl text-white text-left`}
          >
            <p className="whitespace-pre-wrap break-words">{message}</p>
          </div>
        </div>
      ))}
    </div>
  );
}

export default MessageList;
