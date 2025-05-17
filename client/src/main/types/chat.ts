import { z } from "zod";

export const ChatIdSchema = z.string().uuid();

export const ChatMessageTextSchema = z.string();

export const ChatMessageSchema = z.object({
  text: ChatMessageTextSchema
});

export const ChatSchema = z.object({
  id: ChatIdSchema,
  messages: ChatMessageSchema.array()
});

export const CreateChatResponseSchema = z.object({
  id: ChatIdSchema,
  text: ChatMessageTextSchema
})

export const SendChatMessageResponseSchema = z.object({
  text: ChatMessageTextSchema
})

export type ChatId = z.infer<typeof ChatIdSchema>;
export type ChatMessageText = z.infer<typeof ChatMessageTextSchema>;
export type ChatMessage = z.infer<typeof ChatMessageSchema>
export type Chat = z.infer<typeof ChatSchema>;

export type CreateChatResponse = z.infer<typeof CreateChatResponseSchema>;
export type SendChatMessageResponse = z.infer<typeof SendChatMessageResponseSchema>;