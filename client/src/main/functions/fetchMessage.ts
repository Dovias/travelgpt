import { SentChatMessageRequest } from "../types/chatTypes";

const fetchMessage = async (input: string, chatId: number | undefined) => {
  const response = await fetch(`http://localhost:5198/chat/${chatId}`, {
    method: "POST",
    body: JSON.stringify({
      text: input,
    } as SentChatMessageRequest),
    headers: {
      "Content-Type": "application/json",
    },
  });
  console.log(input);
  return response;
};

export default fetchMessage;
