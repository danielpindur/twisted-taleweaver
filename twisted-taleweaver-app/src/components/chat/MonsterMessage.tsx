import { formatTime } from "@/utils/TimeUtils";
import { Skull } from "lucide-react";
import type { ChatMessage } from "@/generated/twisted-taleweaver-api/models/ChatMessage";


export interface MonsterMessageProps {
  message: ChatMessage
};

export const MonsterMessage = ({ message }: MonsterMessageProps) => {
  return (
    <div className="bg-destructive/10 p-3 rounded-lg border border-destructive/20">
      <div className="flex items-center gap-2 mb-1">
        <Skull className="h-4 w-4 text-destructive" />
        <span className="font-crimson font-medium text-destructive">
          {/* message.monsterName */}
        </span>
        <span className="text-xs text-muted-foreground">
          {formatTime(new Date(message.timestamp))}
        </span>
      </div>
      <p className="font-crimson text-destructive/90">{message.content}</p>
    </div>
  );
};