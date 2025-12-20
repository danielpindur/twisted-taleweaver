import { useState } from "react";
import { useInfiniteQuery } from "@tanstack/react-query";
import { PreExpeditionPanel } from "./PreExpeditionPanel";
import { TurnIndicator } from "./TurnIndicator";
import { ChatInput } from "./ChatInput";
import { ChatMessages } from "./ChatMessages";
import { ExpeditionStatus, MessagesService, type Expedition } from "@/generated/twisted-taleweaver-api";
import { getCurrentTurnParticipant } from "@/utils/TurnUtils"
import type { UserResource } from "@clerk/types";
import type { Monster } from "@/interfaces/Monster";

// TODO: Replace with actual API data
import { mockMonsters } from "@/mock/MockMonsters";

export interface ChatPanelProps {
  expedition: Expedition,
  user: UserResource
}

export const ChatPanel = ({ expedition, user }: ChatPanelProps) => {
  const [isInCombat] = useState(expedition.state.status === ExpeditionStatus.STARTED); // TODO: Get from game state
  const [combatMonsters] = useState<Monster[]>(mockMonsters); // TODO: Get from combat state
 
  // TODO: Implement actual message sending API call
  const handleSendMessage = (messageToSend: string) => {
    
  };

  const { data, fetchNextPage, isFetchingNextPage, hasNextPage, isError } = useInfiniteQuery({
    queryKey: ["expedition", expedition.expeditionId, "messages"],
    initialPageParam: null as number | null,
    queryFn: ({ pageParam }) =>
      MessagesService.getApiV1Messages(expedition.expeditionId, pageParam ?? undefined),
    getNextPageParam: (lastPage) => {
      if (!lastPage || lastPage.length === 0) {
        return undefined;
      }

      const lastMessage = lastPage[lastPage.length - 1];
      return lastMessage.expeditionMessageId;
    }
  });
  
  return (
    <div className="flex-1 flex flex-col min-w-0" style={{ minWidth: '200px' }}>
      {expedition.state.status === ExpeditionStatus.CREATED ? (
        <PreExpeditionPanel 
          expedition={expedition} 
          user={user}
        />
      ) : (
        <>
          {/* <CombatEncounterTopPanel isInCombat={isInCombat} combatMonsters={combatMonsters} /> */} 
          <ChatMessages 
            messages={data?.pages.flatMap(p => p) ?? []} 
            participants={expedition.participants} 
            loadMoreMessages={fetchNextPage}
            isLoadingError={isError}
            isFetchingMoreMessages={isFetchingNextPage} 
            canFetchMoreMessages={hasNextPage} />

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