import type { ChatMessage } from "@/__generated__/twisted-taleweaver-api/types";
import { formatTime } from "@/utils/TimeUtils";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";
import { Skull } from "lucide-react";


export interface MonsterMessageProps {
  message: ChatMessage,
  monsters: ExpeditionViewModel["state"]["monsters"]
};

export const MonsterMessage = ({ message, monsters }: MonsterMessageProps) => {
  const monster = monsters.find(m => m.id === message.sender!.creatureId)!;

  return (
    <div className="bg-destructive/10 p-3 rounded-lg border border-destructive/20">
      <div className="flex items-center gap-2 mb-1">
        <Skull className="h-4 w-4 text-destructive" />
        <span className="font-crimson font-medium text-destructive">
          {monster.name}
        </span>
        <span className="text-xs text-muted-foreground">
          {formatTime(new Date(message.timestamp))}
        </span>
      </div>
      <p className="font-crimson text-destructive/90">{message.content}</p>
    </div>
  );
};