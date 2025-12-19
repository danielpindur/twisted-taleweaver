import { useState } from "react";
import { ChatRoomHeader} from "@/components/chat/ChatRoomHeader";
import { ChatPanel } from "@/components/chat/ChatPanel";
import { useQuery } from "@tanstack/react-query";
import { useApiClient } from "@/hooks/useApiClient";
import { ExpeditionsService } from "@/generated/twisted-taleweaver-api/services/ExpeditionsService";
import { useNavigate } from "react-router-dom";
import { Loader2, AlertCircle } from "lucide-react";
import { getUserParticipant } from "@/utils/CharacterUtils"
import { CharacterSidePanel } from "@/components/chat/CharacterSidePanel";
import { PartySidePanel } from "@/components/chat/PartySidePanel";
import type { UserResource } from "@clerk/types";

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
      <div className="h-screen bg-background flex flex-col">
        <ChatRoomHeader 
          expeditionId={expeditionId}
          onLeaveRoom={onLeaveRoom}
          currentParticipants={data.currentParticipants} />
       
        <div className="flex-1 flex overflow-x-auto">
          <PartySidePanel 
            participants={data.participants}
            expeditionOwner={data.createdByUser}
            expeditionState={data.state}
            panelCollapsed={partyPanelCollapsed}
            setPanelCollapsed={setPartyPanelCollapsed}
            activeUser={data.state.activeUser} />
          
          <ChatPanel 
            expedition={data}
            partyPanelCollapsed={partyPanelCollapsed}
            characterPanelCollapsed={characterPanelCollapsed}
            user={user} />
          
          <CharacterSidePanel 
            panelCollapsed={characterPanelCollapsed}
            setPanelCollapsed={setCharacterPanelCollapsed}
            expeditionState={data.state}
            participant={getUserParticipant(data.participants, user)!} />
        </div>
      </div>
  );
};