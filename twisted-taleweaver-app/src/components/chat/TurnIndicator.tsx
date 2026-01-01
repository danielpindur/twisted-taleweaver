import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";
import { Flame } from "lucide-react";

export interface TurnIndicatorProps {
  expeditionState: ExpeditionViewModel["state"]
}

export const TurnIndicator = ({ expeditionState }: TurnIndicatorProps) => {
  return (
    <div>
      {expeditionState.activeCreature && (
        <div className="bg-muted border-t border-border p-3">
          <div className="flex items-center justify-center gap-2 text-sm">
            <Flame className="h-4 w-4 text-primary" />
            <span className="font-crimson">
              It is <span className="text-primary font-medium">
                {expeditionState.activeCreature!.name}'s
              </span> turn to act
            </span>
          </div>
        </div>
      )}
    </div>
  );
}