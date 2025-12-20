import { formatTime } from "@/utils/TimeUtils";
import { getClassIcon } from "@/utils/CharacterUtils";
import type { ExpeditionParticipant } from "@/generated/twisted-taleweaver-api/models/ExpeditionParticipant";
import type { ChatMessage } from "@/generated/twisted-taleweaver-api/models/ChatMessage";

export interface CharacterMessageProps {
  message: ChatMessage,
  participants: ExpeditionParticipant[]
};

export const CharacterMessage = ({ message, participants }: CharacterMessageProps) => {
  var messageSender = participants.find(p => p.character?.characterId == message.sender?.creatureId)!;
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