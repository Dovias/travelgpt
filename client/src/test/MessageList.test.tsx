import { render } from '@testing-library/react';
import { describe, it, expect } from 'vitest';
import MessageList from '../main/components/MessageList';

describe("MessageList", () => {
    it("Displays MessageList, passes if the hello message exists", () => {
        const { getByText } = render(<MessageList messages={["hello"]} />);
        expect(getByText("hello")).toBeTruthy();
    });
    // it("Fails when a non-existent message is expected", () => {
    //     const { getByText } = render(<MessageList messages={["hello", "1"]} />);
    //     expect(getByText("I like to travel")).toBeTruthy(); // will throw
    //   });
    it("Displays MessageList, passes if the message map can contain multiple entries", () => {
        const { getByText } = render(<MessageList messages={["Hello, where you would like to travel?", "I would like to travel top vilnius"]} />);
        expect((getByText("Hello, where you would like to travel?"), "I would like to travel top vilnius")).toBeTruthy();
    });

});
