import { useState } from "react";
import { useInfiniteQuery, useQueryClient, type InfiniteData } from "@tanstack/react-query";
import { PreExpeditionPanel } from "./PreExpeditionPanel";
import { TurnIndicator } from "./TurnIndicator";
import { ChatInput } from "./ChatInput";
import { ChatMessages } from "./ChatMessages";
import { getCurrentTurnParticipant } from "@/utils/TurnUtils"
import type { UserResource } from "@clerk/types";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";

import type { Monster } from "@/interfaces/Monster";

// TODO: Replace with actual API data
import { mockMonsters } from "@/mock/MockMonsters";
import { ExpeditionStatus, OnMessageCreatedDocument, type GetMessagesQuery, type MessageSearchInput, type OnMessageCreatedSubscription } from "@/__generated__/twisted-taleweaver-api/types";
import { useGraphQlClient } from "@/hooks/useGraphQlClient";
import { useGraphQlSubscription } from "@/hooks/useGraphQlSubscription";

export interface ChatPanelProps {
  expedition: ExpeditionViewModel,
  user: UserResource
}

export const ChatPanel = ({ expedition, user }: ChatPanelProps) => {
  const { requestWithAuth } = useGraphQlClient();
  const queryClient = useQueryClient();

  const [isInCombat] = useState(expedition.state.status === ExpeditionStatus.Started); // TODO: Get from game state
  const [combatMonsters] = useState<Monster[]>(mockMonsters); // TODO: Get from combat state

  const { data, fetchNextPage, isFetchingNextPage, hasNextPage, isError } = useInfiniteQuery({
    queryKey: ["expedition", expedition.id, "messages"],
    refetchInterval: false,
    refetchOnWindowFocus: false,
    initialPageParam: null as number | null,
    queryFn: ({ pageParam }) => {
      const input: MessageSearchInput = {
        expeditionId: expedition.id,
        pageSize: 10,
        after: pageParam ?? undefined,
      };
      return requestWithAuth((sdk) => sdk.GetMessages({ input }));
    },
    getNextPageParam: (lastResult) => {
      if (!lastResult?.messages?.hasNextPage) {
        return undefined;
      }

      return lastResult?.messages?.endCursor;
    }
  });

  useGraphQlSubscription<OnMessageCreatedSubscription>({
    document: OnMessageCreatedDocument,
    variables: { expeditionId: expedition.id },
    onNext: (event) => {
        const newMessage = event.messageCreated;

        queryClient.setQueryData(
          ["expedition", expedition.id, "messages"],
          (oldData: InfiniteData<GetMessagesQuery>) => {
            if (!oldData) {
              return oldData;
            }

            return {
              ...oldData,
              pages: oldData.pages.map((page, index) => {
                if (index === 0) {
                  
                  // Check if the message already exists
                  const exists = page.messages.items.some(
                    (m: any) => m.id === newMessage.id
                  );
                  if (exists) {
                    // Skip insert if message already exists
                    return page
                  };

                  return {
                    ...page,
                    messages: {
                      ...page.messages,
                      items: [...page.messages.items, newMessage],
                    },
                  };
                }
                return page;
              }),
            };
          }
        );
      }
  });
  
  return (
    <div className="flex-1 flex flex-col min-w-0" style={{ minWidth: '200px' }}>
      {expedition.state.status === ExpeditionStatus.Created ? (
        <PreExpeditionPanel 
          expedition={expedition} 
          user={user}
        />
      ) : (
        <>
          {/* <CombatEncounterTopPanel isInCombat={isInCombat} combatMonsters={combatMonsters} /> */} 
          {<ChatMessages 
            messages={data?.pages.slice().reverse().flatMap(p => p.messages.items) ?? []} 
            participants={expedition.participants} 
            loadMoreMessages={fetchNextPage}
            isLoadingError={isError}
            isFetchingMoreMessages={isFetchingNextPage} 
            canFetchMoreMessages={hasNextPage} />}

          <TurnIndicator expeditionState={expedition.state} />

          {/* Message Input */}
          <div className="bg-card border-t border-border p-4 space-y-3">
            <ChatInput 
              expeditionId={expedition.id}
              user={user}
              currentTurnParticipant={getCurrentTurnParticipant(expedition.participants, expedition.state.activeCreature)}
            />
          </div>
        </>
      )}
    </div>
  );
};