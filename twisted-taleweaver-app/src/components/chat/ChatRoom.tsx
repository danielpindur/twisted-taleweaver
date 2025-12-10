import { useState } from "react";
import { TooltipProvider } from "@/components/ui/tooltip";
import { ChatRoomHeader} from "./ChatRoomHeader";
import type { Character } from "@/interfaces/Character";
import { ChatPanel } from "./ChatPanel";
import type { UserResource } from "@clerk/types";

// TODO: Replace with actual API data
import { mockCharacters } from "@/mock/MockCharacters";

export interface ChatRoomProps {
  expeditionId: string;
  user: UserResource;
  onLeaveRoom: () => void;
};

export const ChatRoom = ({ expeditionId, user, onLeaveRoom }: ChatRoomProps) => {

  //const [currentUser] = useState<Character>(mockCharacters[0]); // TODO: Get current user data
  const [partyPanelCollapsed, setPartyPanelCollapsed] = useState(false);
  const [characterPanelCollapsed, setCharacterPanelCollapsed] = useState(false);
  //const [isInCombat] = useState(true); // TODO: Get from actual game state
  const [expeditionStatus] = useState<"pre-expedition" | "active" | "completed">("pre-expedition"); // TODO: Get from API
  
  return (
    <TooltipProvider>
      <div className="h-screen bg-background flex flex-col">
        <ChatRoomHeader 
          expeditionId={expeditionId}
          onLeaveRoom={onLeaveRoom}
          characters={mockCharacters} />
       
        <div className="flex-1 flex overflow-x-auto">
          {/*}
          <PartySidePannel 
            characters={mockCharacters}
            panelCollapsed={partyPanelCollapsed}
            setPanelCollapsed={setPartyPanelCollapsed} />
          */}
          
          <ChatPanel 
            characters={mockCharacters}
            partyPanelCollapsed={partyPanelCollapsed}
            characterPanelCollapsed={characterPanelCollapsed}
            user={user}
            expeditionStatus={expeditionStatus} />
          
          {/* 
          <CharacterSidePanel 
            character={currentUser}
            panelCollapsed={characterPanelCollapsed}
            setPanelCollapsed={setCharacterPanelCollapsed}
            user={user}
            isInCombat={isInCombat}
            characters={mockCharacters} />
          */}
        </div>
      </div>
    </TooltipProvider>
  );
};