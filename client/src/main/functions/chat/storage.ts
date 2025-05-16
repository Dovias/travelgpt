import { Chat } from "../../types/chat";

export function storeChats(...chats: Chat[]) {
  localStorage.setItem("chats", JSON.stringify(chats));
}

export function getStoredChats() {
  return JSON.parse(localStorage.getItem("chats")!) as Chat[];
}