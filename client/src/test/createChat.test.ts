import { describe, it, expect, vi } from "vitest";

const createChat = async () => {
  const response = await fetch("http://localhost:5198/api/v1/chat", {
    method: "POST",
  });
  const data = (await response.json()) as { id: number };
  console.log(`Successfully retrieved chat id from the server: ${data.id}`);
  return data.id;
};

globalThis.fetch = vi.fn(() =>
  Promise.resolve({
    json: () => Promise.resolve({ id: 123 }),
  })
) as any;

describe("createChat", () => {
  it("fetches chat ID and returns it", async () => {
    const id = await createChat();
    expect(fetch).toHaveBeenCalledWith("http://localhost:5198/api/v1/chat", { method: "POST" });
    expect(id).toBe(123);
  });
});
