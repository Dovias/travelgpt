import { describe, it, expect, vi } from "vitest";
import fetchMessage from "../functions/fetchMessage";

globalThis.fetch = vi.fn(() =>
  Promise.resolve({
    json: () => Promise.resolve({ text: "Mock response" }),
  })
) as any;

describe("fetchMessage", () => {
  it("calls fetch correctly", async () => {
    await fetchMessage("Hello", 123);

    expect(fetch).toHaveBeenCalled();
  });
});
