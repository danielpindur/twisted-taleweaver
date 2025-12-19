import { Flame } from "lucide-react";
import { getCurrentTurnParticipant } from "@/utils/TurnUtils"
import type { ExpeditionParticipant, ExpeditionState } from "@/generated/twisted-taleweaver-api";

export interface TurnIndicatorProps {
  participants: ExpeditionParticipant[],
  expeditionState: ExpeditionState
}

export const TurnIndicator = ({ participants, expeditionState }: TurnIndicatorProps) => {
  return (
    <div>
      {expeditionState.activeUser && (
        <div className="bg-muted border-t border-border p-3">
          <div className="flex items-center justify-center gap-2 text-sm">
            <Flame className="h-4 w-4 text-primary" />
            <span className="font-crimson">
              It is <span className="text-primary font-medium">
                {getCurrentTurnParticipant(participants, expeditionState.activeUser)!.character!.name}'s
              </span> turn to act
            </span>
          </div>
        </div>
      )}
    </div>
  );
}