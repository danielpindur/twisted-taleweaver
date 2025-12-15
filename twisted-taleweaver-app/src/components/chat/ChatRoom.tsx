import { useState } from "react";
import { TooltipProvider } from "@/components/ui/tooltip";
import { ChatRoomHeader} from "./ChatRoomHeader";
import { ChatPanel } from "./ChatPanel";
import type { UserResource } from "@clerk/types";
import { useQuery } from "@tanstack/react-query";
import { useApiClient } from "@/hooks/useApiClient";
import { ExpeditionsService } from "@/generated/twisted-taleweaver-api/services/ExpeditionsService";
import { useNavigate } from "react-router-dom";
import { Loader2, AlertCircle } from "lucide-react";

export interface ChatRoomProps {
  expeditionId: string;
  user: UserResource;
  onLeaveRoom: () => void;
};

export const ChatRoom = ({ expeditionId, user, onLeaveRoom }: ChatRoomProps) => {
  const { requestWithAuth } = useApiClient();
  const navigate = useNavigate();

  const { data, isLoading, isError } = useQuery({
    queryKey: ["expedition", expeditionId],
    queryFn: () => {
      return requestWithAuth(() =>
        ExpeditionsService.getApiExpeditions1(expeditionId)
      );
    },
  });

  //const [currentUser] = useState<Character>(mockCharacters[0]); // TODO: Get current user data
  const [partyPanelCollapsed, setPartyPanelCollapsed] = useState(false);
  const [characterPanelCollapsed, setCharacterPanelCollapsed] = useState(false);
  //const [isInCombat] = useState(true); // TODO: Get from actual game state
  
  if (isLoading) {
    return (
      <div className="col-span-full flex justify-center items-center h-32">
        <Loader2 className="h-8 w-8 animate-spin text-primary" />
      </div>
    )
  }

  if (isError) {
    return (
      <div className="col-span-full flex justify-center items-center text-center text-red-500 font-crimson">
        <AlertCircle className="inline-block h-5 w-5 mr-1" />
        Failed to load expedition. Try refreshing the page.
      </div>
    );
  }
      
  if (!data) {
    navigate("/");
    return null;
  }
        

  return (
    <TooltipProvider>
      <div className="h-screen bg-background flex flex-col">
        <ChatRoomHeader 
          expeditionId={expeditionId}
          onLeaveRoom={onLeaveRoom}
          currentParticipants={data.currentParticipants} />
       
        <div className="flex-1 flex overflow-x-auto">
          {/*}
          <PartySidePannel 
            characters={mockCharacters}
            panelCollapsed={partyPanelCollapsed}
            setPanelCollapsed={setPartyPanelCollapsed} />
          */}
          
          <ChatPanel 
            expedition={data}
            partyPanelCollapsed={partyPanelCollapsed}
            characterPanelCollapsed={characterPanelCollapsed}
            user={user} />
          
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