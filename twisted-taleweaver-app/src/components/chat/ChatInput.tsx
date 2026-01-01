import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Send } from "lucide-react";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";
import { useMutation } from "@tanstack/react-query";
import type { AddMessageInput } from "@/__generated__/twisted-taleweaver-api/types";
import { useGraphQlClient } from "@/hooks/useGraphQlClient";
import type { UserResource } from "@clerk/types";

export interface ChatInputProps {
  expeditionId: string;
  currentTurnParticipant?: ExpeditionViewModel["participants"][number];
  user: UserResource
};

export const ChatInput = ({ expeditionId, user, currentTurnParticipant }: ChatInputProps) => {
  const { requestWithAuth } = useGraphQlClient();

  const [newMessage, setNewMessage] = useState("");
  
  const handleSendMessage = () => {
    const input: AddMessageInput = {
      expeditionId: expeditionId,
      content: newMessage.trim()
    };

    addMessageMutation.mutate(input);
  };

  const addMessageMutation = useMutation({
    mutationFn: (input: AddMessageInput) =>
      requestWithAuth((sdk) => sdk.AddMessage({ input })),
    onSuccess: () => {
      setNewMessage("");
    }
  });

  const isUserTurn = currentTurnParticipant?.user.externalId === user.id;
  const disabled = !isUserTurn || addMessageMutation.isPending;

  return (
    <div className="flex gap-2 items-center">
      <Input
        placeholder={disabled && currentTurnParticipant 
          ? `Waiting for ${currentTurnParticipant.character!.name}'s turn...` 
          : "Describe your action... or final words..."
        }
        value={newMessage}
        onChange={(e) => setNewMessage(e.target.value)}
        onKeyUp={(e) => {
          if (e.key === "Enter" && !disabled) {
            e.preventDefault();
            handleSendMessage();
          }
        }}
        className="bg-input border-border font-crimson"
        disabled={disabled}
      />
      <Button 
        onClick={handleSendMessage} 
        disabled={disabled || !newMessage.trim()}
      >
        <Send className="h-4 w-4" />
      </Button>
    </div>
  );
}