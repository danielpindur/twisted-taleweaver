import { formatTime } from "@/utils/TimeUtils";
import { getClassIcon } from "@/utils/CharacterUtils";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";
import type { ChatMessage } from "@/__generated__/twisted-taleweaver-api/types";

export interface CharacterMessageProps {
  message: ChatMessage,
  participants: ExpeditionViewModel["participants"]
};

export const CharacterMessage = ({ message, participants }: CharacterMessageProps) => {
  var messageSender = participants.find(p => p.character?.id == message.sender?.creatureId)!;
  var ClassIcon = getClassIcon(messageSender.character!.class);

  return(
    <div className="bg-Character/10 p-3 rounded-lg border border-Character/20">
      <div className="flex items-center gap-2 mb-1">
        <ClassIcon className="h-4 w-4 text-Character" />
        <span className="font-crimson font-medium text-Character">
          {messageSender.character!.name}
        </span>
        <span className="text-xs text-muted-foreground">
          {formatTime(new Date(message.timestamp))}
        </span>
      </div>
      <p className="font-crimson">{message.content}</p>
    </div>
  );
};