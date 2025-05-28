import {
  ChatId,
  ChatMessage,
  ChatMessageId,
  ChatCreateResponse,
  ChatMessageEditResponse,
  ChatMessageSendResponse,
} from "../../types/chat";

const API_SERVER_URL = import.meta.env.VITE_API_SERVER_URL;

export async function createChat(
  message: ChatMessage
): Promise<ChatCreateResponse> {
  const response = await fetch(`${API_SERVER_URL}/chat`, {
    method: "POST",
    body: message,
    headers: {
      "Content-Type": "text/plain",
    },
  });

  return await response.json();
}

export async function deleteChat(id: ChatId) {
  await fetch(`${API_SERVER_URL}/chat/${id}`, {
    method: "DELETE",
  });
}

export async function sendChatMessage(
  chatId: ChatId,
  message: ChatMessage
): Promise<ChatMessageSendResponse> {
  const response = await fetch(`${API_SERVER_URL}/chat/${chatId}`, {
    method: "POST",
    body: message,
    headers: {
      "Content-Type": "text/plain",
    },
  });

  return response.text();
}

export async function editChatMessage(
  chatId: ChatId,
  chatMessageId: ChatMessageId,
  message: ChatMessage
): Promise<ChatMessageEditResponse> {
  const response = await fetch(
    `${API_SERVER_URL}/chat/${chatId}/${chatMessageId}`,
    {
      method: "PUT",
      body: message,
      headers: {
        "Content-Type": "text/plain",
      },
    }
  );

  return response.json();
}

export async function deleteChatMessage(
  chatId: ChatId,
  messageId: ChatMessageId
) {
  await fetch(`${API_SERVER_URL}/chat/${chatId}/${messageId}`, {
    method: "DELETE",
  });
}
