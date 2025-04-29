
interface Props {
  onNewChat: () => void;
}

function NewChatButton({onNewChat}: Props) {
  return (
        <button onClick={onNewChat} className="px-2 py-1 text-sm bg-gray-600 rounded hover:bg-gray-500">
          New Chat
        </button>
    );
}

export default NewChatButton;