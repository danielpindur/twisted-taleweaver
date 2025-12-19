import { useState } from "react";
import { Button } from "@/components/ui/button";
import { CombatEncounterTopPanel } from "./CombatEncounterTopPanel";
import { PreExpeditionPanel } from "./PreExpeditionPanel";
import type { Monster } from "@/interfaces/Monster";
import type { ChatMessage } from "@/interfaces/ChatMessage";
import type { PendingExpedition } from "@/interfaces/PendingExpedition";
import { TurnIndicator } from "./TurnIndicator";
import { ChatInput } from "./ChatInput";
import { ChatMessages } from "./ChatMessages";
import { Sword, Shield, Eye, Sparkles, MessageSquare, Map, Backpack } from "lucide-react";
import { Tooltip, TooltipContent, TooltipTrigger } from "@/components/ui/tooltip";
import { ExpeditionStatus, type Expedition, type ExpeditionParticipant } from "@/generated/twisted-taleweaver-api";
import { getCurrentTurnParticipant } from "@/utils/TurnUtils"
import type { UserResource } from "@clerk/types";

// TODO: Replace with actual API data
import { mockMonsters } from "@/mock/MockMonsters";
import { mockMessages } from "@/mock/MockChatMessages";
import { mockPendingExpedition, mockPreExpeditionMessages } from "@/mock/MockPendingExpeditions";

export interface ChatPanelProps {
  expedition: Expedition,
  partyPanelCollapsed: boolean,
  characterPanelCollapsed: boolean,
  user: UserResource
}

export const ChatPanel = ({ expedition, partyPanelCollapsed, characterPanelCollapsed, user }: ChatPanelProps) => {
  const [isInCombat] = useState(expedition.state.status === ExpeditionStatus.STARTED); // TODO: Get from game state
  const [combatMonsters] = useState<Monster[]>(mockMonsters); // TODO: Get from combat state
  const [messages, setMessages] = useState<ChatMessage[]>(
    expedition.state.status === ExpeditionStatus.CREATED ? mockPreExpeditionMessages : mockMessages
  );
 
  // TODO: Implement actual message sending API call
  const handleSendMessage = (messageToSend: string) => {
    if (messageToSend.trim()) {
      const message: ChatMessage = {
        id: Date.now().toString(),
        sender: "character",
        characterName: user.username!,
        content: messageToSend,
        timestamp: new Date(),
        type: expedition.state.status === ExpeditionStatus.CREATED ? "dialogue" : "action"
      };
      
      setMessages(prev => [...prev, message]);
      
      // TODO: Send to backend via websocket/API
    }
  };

  // TODO: Implement actual expedition start API call
  const handleStartExpedition = () => {
    console.log("Starting expedition:", expedition.expeditionId);
    
    // Add system message for expedition start
    const startMessage: ChatMessage = {
      id: Date.now().toString(),
      sender: "system",
      content: "The expedition begins! May fortune favor the brave...",
      timestamp: new Date(),
      type: "system"
    };
    
    setMessages(prev => [...prev, startMessage]);
    
    // TODO: Transition to active expedition state via API
  };

  // Mock function to simulate loading more messages
  const handleLoadMoreMessages = () => {
    console.log("Loading more messages...");
    
    // Generate some mock older messages
    const olderMessages: ChatMessage[] = [];
    const currentCount = messages.length;
    
    for (let i = 0; i < 10; i++) {
      const messageId = (currentCount + i + 1000).toString();
      const senders = ["taleweaver", "character", "monster", "system"] as const;
      const randomSender = senders[Math.floor(Math.random() * senders.length)];
      
      let message: ChatMessage;
      
      if (randomSender === "taleweaver") {
        message = {
          id: messageId,
          sender: "taleweaver",
          content: [
            "The shadows whisper of ancient secrets hidden in these halls...",
            "A cold wind carries the scent of decay and forgotten magic.",
            "The very walls seem to pulse with malevolent energy.",
            "Your torchlight flickers, as if the darkness itself seeks to snuff it out.",
            "Strange runes carved into the stone begin to glow ominously..."
          ][Math.floor(Math.random() * 5)],
          timestamp: new Date(Date.now() - (i + 1) * 60000) // Messages from 1+ minutes ago
        };
      } else if (randomSender === "character") {
        message = {
          id: messageId,
          sender: "character",
          characterName: user.username!,
          content: [
            "I carefully examine the area for any hidden dangers.",
            "Let me check for traps before we proceed.",
            "I ready my weapon and advance cautiously.",
            "Something doesn't feel right about this place.",
            "I cast a protective spell on the party."
          ][Math.floor(Math.random() * 5)],
          timestamp: new Date(Date.now() - (i + 1) * 60000)
        };
      } else if (randomSender === "monster") {
        message = {
          id: messageId,
          sender: "monster",
          monsterName: "Shadow Beast",
          content: [
            "*growls menacingly from the darkness*",
            "*emerges from the shadows with glowing red eyes*",
            "*lets out a bone-chilling howl*",
            "*stalks the party with predatory intent*"
          ][Math.floor(Math.random() * 4)],
          timestamp: new Date(Date.now() - (i + 1) * 60000)
        };
      } else {
        message = {
          id: messageId,
          sender: "system",
          content: [
            "You hear the distant sound of dripping water.",
            "A torch on the wall gutters and goes out.",
            "The temperature drops noticeably.",
            "You notice fresh scratches on the stone floor."
          ][Math.floor(Math.random() * 4)],
          timestamp: new Date(Date.now() - (i + 1) * 60000)
        };
      }
      
      olderMessages.unshift(message); // Add to beginning (oldest first)
    }
    
    // Add the older messages to the beginning of the current messages
    setMessages(prevMessages => [...olderMessages, ...prevMessages]);
  };
  
  // Find current user based on username
  const currentUser = expedition.participants.find(p => p.user.externalUserId === user.id!);
  
  // Turn-based logic
  //const isUserTurn = canUserAct(characters, user.username!);
  //const currentTurnCharacter = getCurrentTurnCharacter(characters);

  return (
    <div className="flex-1 flex flex-col min-w-0" style={{ minWidth: '200px' }}>
      {expedition.state.status === ExpeditionStatus.CREATED ? (
        <PreExpeditionPanel 
          expedition={expedition} 
          user={user}
          onStartExpedition={handleStartExpedition}
        />
      ) : (
        <>
          {/* <CombatEncounterTopPanel isInCombat={isInCombat} combatMonsters={combatMonsters} /> */} 
          <ChatMessages 
            messages={messages} 
            participants={expedition.participants} 
            onLoadMoreMessages={handleLoadMoreMessages} />

          <TurnIndicator 
            participants={expedition.participants} 
            expeditionState={expedition.state} />

          {/* Message Input */}
          <div className="bg-card border-t border-border p-4 space-y-3">
            <ChatInput 
              handleSendMessage={handleSendMessage} 
              disabled={expedition.state.activeUser?.externalUserId !== user.id}
              currentTurnParticipant={getCurrentTurnParticipant(expedition.participants, expedition.state.activeUser)}
            />
          </div>
        </>
      )}
    </div>
  );
};