import { useState } from "react";
import { ChatRoomHeader} from "@/components/chat/ChatRoomHeader";
import { ChatPanel } from "@/components/chat/ChatPanel";
import { useQuery } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import { Loader2, AlertCircle } from "lucide-react";
import { getUserParticipant } from "@/utils/CharacterUtils"
import { CharacterSidePanel } from "@/components/chat/CharacterSidePanel";
import { PartySidePanel } from "@/components/chat/PartySidePanel";
import { useGraphQlClient } from "@/hooks/useGraphQlClient";
import type { UserResource } from "@clerk/types";

export interface ChatRoomProps {
  expeditionId: string;
  user: UserResource;
  onLeaveRoom: () => void;
};

export const ChatRoom = ({ expeditionId, user, onLeaveRoom }: ChatRoomProps) => {
  const { requestWithAuth } = useGraphQlClient();
  const navigate = useNavigate();

  const { data, isLoading, isError } = useQuery({
    queryKey: ["expedition", expeditionId],
    queryFn: () => {
      return requestWithAuth((sdk) => sdk.GetExpeditionById({ id: expeditionId }));
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

  if (isError || !data?.expeditionById)
  {
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
          expeditionName={data.expeditionById.name}
          onLeaveRoom={onLeaveRoom}
          currentParticipants={data.expeditionById.currentParticipants} />
       
        <div className="flex-1 flex overflow-x-auto">
          <PartySidePanel 
            participants={data.expeditionById.participants}
            expeditionOwner={data.expeditionById.userCreated}
            expeditionState={data.expeditionById.state}
            panelCollapsed={partyPanelCollapsed}
            setPanelCollapsed={setPartyPanelCollapsed}
            activeUser={data.expeditionById.state.activeUser} />
          
          <ChatPanel 
            expedition={data.expeditionById}
            user={user} />
          
          <CharacterSidePanel 
            panelCollapsed={characterPanelCollapsed}
            setPanelCollapsed={setCharacterPanelCollapsed}
            expeditionState={data.expeditionById.state}
            participant={getUserParticipant(data.expeditionById.participants, user)!} />
        </div>
      </div>
  );
};