export type ChatId = string;
export type ChatMessageText = string;
export type ChatMessage = {
  text: ChatMessageText
}

export type Chat = {
  id: ChatId,
  messages: ChatMessage[]
}
