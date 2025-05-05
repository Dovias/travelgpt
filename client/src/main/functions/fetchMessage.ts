import { SentChatMessageRequest } from "../types/chatTypes";

const fetchMessage = async (input: string, chatId: string | undefined) => {
  const response = await fetch(`/api/chat/${chatId}`, {
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
