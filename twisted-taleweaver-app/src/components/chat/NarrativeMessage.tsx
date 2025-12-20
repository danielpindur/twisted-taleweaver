import { formatTime } from "@/utils/TimeUtils";
import { BookOpen } from "lucide-react";
import type { ChatMessage } from "@/generated/twisted-taleweaver-api/models/ChatMessage";

export interface NarrativeMessageProps {
  message: ChatMessage
};

export const NarrativeMessage = ({ message }: NarrativeMessageProps) => {

  return(
    <div className="bg-gradient-taleweaver p-4 rounded-lg border border-border shadow-evil">
      <div className="flex items-center gap-2 mb-2">
        <BookOpen className="h-4 w-4 text-taleweaver-foreground" />
        <span className="font-cinzel font-medium text-taleweaver-foreground">
          The Twisted Taleweaver
        </span>
        <span className="text-xs text-taleweaver-foreground/70">
          {formatTime(new Date(message.timestamp))}
        </span>
      </div>
      <p className="font-crimson text-taleweaver-foreground italic">
        {message.content}
      </p>
    </div>
  );
};