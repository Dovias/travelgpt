import { describe, it } from "vitest";
import { ChatMessage, CreateChatResponseSchema, SendChatMessageResponseSchema } from "../../main/types/chat";
import { createChat, sendChatMessage } from "../../main/functions/chat/server";
import { expect } from "vitest";

describe("Chat API validation", () => {
    it('Send multiple messages in one chat', async () => {
        const chat = await createChat({ text: "Start conversation" });
        const messages = [
            { text: "What is the capital of France?" },
            { text: "And Germany?" },
            { text: "Tell me about Lithuania." }
        ];
        for (const msg of messages) {
            const response = await sendChatMessage(chat.id, msg);
            SendChatMessageResponseSchema.parse(response);
        }
    })
    it('Reuses existing chat session', async () => {
        const chat = await createChat({ text: "Initial message" });
        await new Promise(resolve => setTimeout(resolve, 100));
        const followUp: ChatMessage = { text: "Follow-up question" };
        const response = await sendChatMessage(chat.id, followUp);
        SendChatMessageResponseSchema.parse(response);
    })
})