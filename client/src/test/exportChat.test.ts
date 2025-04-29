import { describe, it, vi } from "vitest";

globalThis.URL.createObjectURL = vi.fn(() => "blob:url");
globalThis.URL.revokeObjectURL = vi.fn();
globalThis.document.createElement = vi.fn(() => ({ click: vi.fn() })) as any;
globalThis.document.body.appendChild = vi.fn();
globalThis.document.body.removeChild = vi.fn();

const messages = ["Hello", "Hi"];
const chatId = 123;

const exportChat = () => {
  const text = messages.map((m, i) => `${i % 2 === 0 ? "Assistent" : "Me"}: ${m}`).join("\n\n");
  const blob = new Blob([text], { type: "text/plain" });
  const url = URL.createObjectURL(blob);
  const link = document.createElement("a");
  link.href = url;
  link.download = `chat-history-${chatId}.txt`;
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
  URL.revokeObjectURL(url);
};

describe("exportChat", () => {
  it("runs without crashing", () => {
    exportChat();
  });
});
