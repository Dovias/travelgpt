import { useMemo, useState } from "react";

export type ChatId = string;
export type Message = string;
export type MessageResponse = Message;

export async function createChat(): Promise<ChatId> {
    const response = await fetch("http://localhost:5198/api/v1/chat", { method: "POST" });
    return (await response.json()).id
}

export async function getChatMessages(id: ChatId): Promise<Message[]> {
    const response = await fetch(`http://localhost:5198/api/v1/chat/${id}`);
    return (await response.json()).messages.map(message => message.text)
}

export async function sendChatMessage(id: ChatId, message: Message): Promise<MessageResponse> {
    const response = await fetch(`http://localhost:5198/api/v1/chat/${id}`, {
        method: "POST",
        body: JSON.stringify({
            text: message
        }),
        headers: {
            "Content-Type": "application/json",
        }
    });

    return (await response.json()).text;
}

export type Chat = {
    messages: Message[],
    sendMessage: (...parameters: (Parameters<typeof sendChatMessage> extends [infer _, ...infer Tail] ? Tail : never)) => ReturnType<typeof sendChatMessage>
}

export function useChat(): Chat {
    const [messages, setMessages] = useState<string[]>([]);
    const [id, setId] = useState<ChatId>();
    useMemo(async () => {
        let id = localStorage.getItem("chat") as ChatId;
        if (!id) {
            id = await createChat();
            localStorage.setItem("chat", id);
        }
        setId(id);

        setMessages(await getChatMessages(id));
    }, [])


    return {
        messages,
        sendMessage: async (message) => {
            setMessages([...messages, message]);
            const response = await sendChatMessage(id!, message);
            setMessages([...messages, message, response]);

            return response;
        }
    }
}

export default useChat;