import { Button } from "@/components/ui/button";
import { ArrowLeft, Users } from "lucide-react";

export interface ChatRoomHeaderProps {
  expeditionId: string;
  expeditionName: string;
  onLeaveRoom: () => void;
  currentParticipants: number;
};

export const ChatRoomHeader = ({ expeditionId, expeditionName, onLeaveRoom, currentParticipants }: ChatRoomHeaderProps) => {

  return (
    <div className="bg-card border-b border-border p-4 shadow-dark">
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-4">
          <Button variant="ghost" size="icon" onClick={onLeaveRoom}>
            <ArrowLeft className="h-4 w-4" />
          </Button>
          <div>
            <h1 className="text-xl font-cinzel font-bold text-primary">
              {expeditionName}
            </h1>
            <p className="text-sm text-muted-foreground font-crimson">
              Room ID: {expeditionId} • Turn-based Adventure
            </p>
          </div>
        </div>
        <div className="flex items-center gap-2">
          <Users className="h-4 w-4 text-muted-foreground" />
          <span className="text-sm text-muted-foreground">{currentParticipants} adventurers</span>
        </div>
      </div>
    </div>);
}