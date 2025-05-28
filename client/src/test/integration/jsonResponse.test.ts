import { describe, it, expect } from "vitest";
import { createChat } from "../../main/functions/chat/server";
import { ChatCreateResponseSchema, ChatMessageSchema } from "../../main/types/chat";

async function getChatMessages(chatId: string): Promise<string[]> {
  const response = await fetch(`${import.meta.env.VITE_API_SERVER_URL}/chat/${chatId}`);
  return await response.json();
}

describe("Chat API contract", () => {
  it("Create chat", async () => {
    const response = await createChat("TravelGPT");
    ChatCreateResponseSchema.parse(response);
  });

  it("GET /chat/{chatId} returns valid array of chat messages", async () => {
    const chat = await createChat("TravelGPT");
    ChatCreateResponseSchema.parse(chat);
    const messages = await getChatMessages(chat.id);
    expect(Array.isArray(messages)).toBe(true);
    messages.forEach((msg) => ChatMessageSchema.parse(msg));
  });
});
