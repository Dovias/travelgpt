import { z } from "zod";

export const ChatIdSchema = z.string();
export const ChatMessageIdSchema = z.number();

export const ChatMessageSchema = z.string();
export const ChatResponseSchema = ChatMessageSchema;

export const ChatSchema = z.object({
  id: ChatIdSchema,
  messages: ChatMessageSchema.array(),
});

export const ChatCreateResponseSchema = z.object({
  id: ChatIdSchema,
  response: ChatMessageSchema,
});

export const ChatMessageEditResponseSchema = ChatMessageSchema.array();
export const ChatMessageSendResponseSchema = ChatMessageSchema;

export type ChatId = z.infer<typeof ChatIdSchema>;
export type ChatMessageId = z.infer<typeof ChatMessageIdSchema>;
export type ChatMessage = z.infer<typeof ChatMessageSchema>;
export type ChatResponse = z.infer<typeof ChatResponseSchema>;
export type Chat = z.infer<typeof ChatSchema>;

export type ChatCreateResponse = z.infer<typeof ChatCreateResponseSchema>;
export type ChatMessageEditResponse = z.infer<
  typeof ChatMessageEditResponseSchema
>;
export type ChatMessageSendResponse = z.infer<
  typeof ChatMessageSendResponseSchema
>;
