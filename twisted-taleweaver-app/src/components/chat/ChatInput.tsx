import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Send } from "lucide-react";
import type { Character } from "@/interfaces/Character";

export interface ChatInputProps {
  handleSendMessage: (message: string) => void;
  disabled?: boolean;
  currentTurnCharacter?: Character;
};

export const ChatInput = ({handleSendMessage, disabled = false, currentTurnCharacter}: ChatInputProps) => {
  const [newMessage, setNewMessage] = useState("");
  
  const sendButtonClicked = () => {
    if (!disabled) {
      handleSendMessage(newMessage);
      setNewMessage("");
    }
  };

  return (
    <div className="flex gap-2">
      <Input
        placeholder={disabled && currentTurnCharacter 
          ? `Waiting for ${currentTurnCharacter.name}'s turn...` 
          : "Describe your action... or final words..."
        }
        value={newMessage}
        onChange={(e) => setNewMessage(e.target.value)}
        onKeyUp={(e) => e.key === "Enter" && !disabled && sendButtonClicked()}
        className="bg-input border-border font-crimson"
        disabled={disabled}
      />
      <Button 
        onClick={sendButtonClicked} 
        disabled={disabled || !newMessage.trim()}
      >
        <Send className="h-4 w-4" />
      </Button>
    </div>
  );
}