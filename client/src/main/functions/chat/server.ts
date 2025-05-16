import { ChatId, ChatMessage } from "../../types/chat";

export type CreateChatResponse = {
  id: string
} & ChatMessage

export type SendChatMessageResponse = ChatMessage;

export async function createChat(message: ChatMessage) {
  const response = await fetch("/api/chat", {
    method: "POST",
    body: JSON.stringify(message),
    headers: {
      "Content-Type": "application/json",
    }
  });

  return await response.json() as CreateChatResponse;
}

export async function deleteChat(id: ChatId) {
  await fetch(`/api/chat/${id}`, {
    method: "DELETE"
  });
}

export async function sendChatMessage(id: ChatId, message: ChatMessage) {
  const response = await fetch(`/api/chat/${id}`, {
    method: "POST",
    body: JSON.stringify(message),
    headers: {
      "Content-Type": "application/json",
    }
  });
  return await response.json() as SendChatMessageResponse;
}