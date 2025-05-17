import { describe, it } from "vitest"
import { createChat, sendChatMessage } from "../../main/functions/chat/server"
import { ChatMessage, CreateChatResponseSchema, SendChatMessageResponseSchema } from "../../main/types/chat";

const message: ChatMessage = {
  text: "Vilnius"
}

describe('Chat API contract', () => {
  it('Create chat', async () => {
    const response = await createChat(message);

    CreateChatResponseSchema.parse(response)
  });

  it('Send chat message', async () => {
    const response = await sendChatMessage((await createChat(message)).id, message);

    SendChatMessageResponseSchema.parse(response);
  })
})