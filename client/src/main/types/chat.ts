import { z } from "zod";

export const ChatIdSchema = z.string();
export const ChatMessageIdSchema = z.number();

export const ChatMessageSchema = z.string();
export const ChatResponseSchema = z.string();

export const ChatSchema = z.object({
  id: ChatIdSchema,
  messages: ChatMessageSchema.array(),
});

export const ChatCreateResponseSchema = z.object({
  id: ChatIdSchema,
  response: ChatMessageSchema,
});

export const ChatEditMessageResponseSchema = ChatResponseSchema.array();

export type ChatId = z.infer<typeof ChatIdSchema>;
export type ChatMessageId = z.infer<typeof ChatMessageIdSchema>;
export type ChatMessage = z.infer<typeof ChatMessageSchema>;
export type ChatResponse = z.infer<typeof ChatResponseSchema>;
export type Chat = z.infer<typeof ChatSchema>;

export type ChatCreateResponse = z.infer<typeof ChatCreateResponseSchema>;
export type ChatEditMessageResponse = z.infer<
  typeof ChatEditMessageResponseSchema
>;
