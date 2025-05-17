import { render, screen, fireEvent } from "@testing-library/react";
import { describe, it, expect, vi } from "vitest";
import Input from "../../main/components/Input";

describe("Input component", () => {
  it("renders input with correct placeholder", () => {
    render(<Input onSendMessage={() => {}} disconnected={false} />);
    expect(screen.getByPlaceholderText("Type where you would like to go...")).toBeInTheDocument();
  });

  it("calls onSendMessage when Enter is pressed with input", () => {
    const mockSend = vi.fn();
    render(<Input onSendMessage={mockSend} disconnected={false} />);
    const input = screen.getByPlaceholderText(/type/i);
    fireEvent.change(input, { target: { value: "Hello" } });
    fireEvent.keyDown(input, { key: "Enter", code: "Enter" });
    expect(mockSend).toHaveBeenCalledWith("Hello");
  });

  it("does not call onSendMessage if input is empty", () => {
    const mockSend = vi.fn();
    render(<Input onSendMessage={mockSend} disconnected={false} />);
    const input = screen.getByPlaceholderText(/type/i);
    fireEvent.keyDown(input, { key: "Enter", code: "Enter" });
    expect(mockSend).not.toHaveBeenCalled();
  });

  it("disables input and button when disconnected", () => {
    render(<Input onSendMessage={() => {}} disconnected={true} />);
    expect(screen.getByPlaceholderText(/unavailable/i)).toBeDisabled();
    expect(screen.getByRole("button")).toBeDisabled();
  });
});
