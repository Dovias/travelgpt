import { describe, it } from "vitest";
import { createChat, sendChatMessage } from "../../main/functions/chat/server";
import {
  ChatCreateResponseSchema,
  ChatMessageSchema,
} from "../../main/types/chat";

describe("Chat API contract", () => {
  it("Create chat", async () => {
    const response = await createChat("TravelGPT");

    ChatCreateResponseSchema.parse(response);
  });

  it("Send chat message", async () => {
    const response = await sendChatMessage(
      (
        await createChat("Dovias")
      ).id,
      "Wrote"
    );

    ChatMessageSchema.parse(response);
  });

  it("Edit chat message", async () => {
    const response = await sendChatMessage(
      (
        await createChat("This")
      ).id,
      "Code"
    );

    ChatMessageSchema.parse(response);
  });
});
