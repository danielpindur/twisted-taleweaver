import { formatTime } from "@/utils/TimeUtils";
import { Eye } from "lucide-react";
import type { ChatMessage } from "@/generated/twisted-taleweaver-api/models/ChatMessage";

export interface SystemMessageProps {
  message: ChatMessage
};

export const SystemMessage = ({ message }: SystemMessageProps) => {

  return(
    <div className="bg-muted/50 p-2 rounded-md border border-border">
      <div className="flex items-center gap-2">
        <Eye className="h-3 w-3 text-muted-foreground" />
        <span className="text-sm font-crimson text-muted-foreground">
          {message.content}
        </span>
        <span className="text-xs text-muted-foreground ml-auto">
          {formatTime(new Date(message.timestamp))}
        </span>
      </div>
    </div>
  )
};