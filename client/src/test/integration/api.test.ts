import { describe, it } from "vitest";
import {
  ChatMessage,
  ChatMessageSendResponseSchema,
} from "../../main/types/chat";
import { createChat, sendChatMessage } from "../../main/functions/chat/server";

describe("Chat API validation", () => {
  it("Send multiple messages in one chat", async () => {
    const chat = await createChat("Start conversation");
    const messages = [
      "What is the capital of France?",
      "And Germany?",
      "Tell me about Lithuania.",
    ];
    for (const msg of messages) {
      const response = await sendChatMessage(chat.id, msg);
      ChatMessageSendResponseSchema.parse(response);
    }
  });
  it("Reuses existing chat session", async () => {
    const chat = await createChat("Initial message");
    await new Promise((resolve) => setTimeout(resolve, 100));
    const followUp: ChatMessage = "Follow-up question";
    const response = await sendChatMessage(chat.id, followUp);
    ChatMessageSendResponseSchema.parse(response);
  });
});
