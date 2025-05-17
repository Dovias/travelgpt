import { ChatId, ChatMessage, CreateChatResponse, SendChatMessageResponse } from "../../types/chat";

const API_SERVER_URL = import.meta.env.VITE_API_SERVER_URL;

export async function createChat(message: ChatMessage) {
  const response = await fetch(`${API_SERVER_URL}/chat`, {
    method: "POST",
    body: JSON.stringify(message),
    headers: {
      "Content-Type": "application/json",
    }
  });

  return await response.json() as CreateChatResponse;
}

export async function deleteChat(id: ChatId) {
  await fetch(`${API_SERVER_URL}/chat/${id}`, {
    method: "DELETE"
  });
}

export async function sendChatMessage(id: ChatId, message: ChatMessage) {
  const response = await fetch(`${API_SERVER_URL}/chat/${id}`, {
    method: "POST",
    body: JSON.stringify(message),
    headers: {
      "Content-Type": "application/json",
    }
  });
  return await response.json() as SendChatMessageResponse;
}