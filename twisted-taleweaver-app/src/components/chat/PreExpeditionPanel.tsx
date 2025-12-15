import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Users, Crown, Play, Clock, Loader2 } from "lucide-react";
import type { Character } from "@/interfaces/Character";
import { getClassIcon } from "@/utils/CharacterUtils";
import { ExpeditionStatus, type Expedition, type User } from "@/generated/twisted-taleweaver-api";
import { getDifficultyColor } from "@/utils/ExpeditionDifficultyUtils";
import type { UserResource } from "@clerk/types";

interface PreExpeditionPanelProps {
  expedition: Expedition;
  user: UserResource;
  onStartExpedition?: () => void;
}

export const PreExpeditionPanel = ({ expedition, user, onStartExpedition }: PreExpeditionPanelProps) => {
  
  const [isStarting, setIsStarting] = useState(false);
  const isOwner = expedition.createdByUser.externalUserId === user.id;
  const isReady = expedition.currentParticipants >= expedition.minParticipants;
  const waitingCount = expedition.minParticipants - expedition.currentParticipants;

  const handleStartExpedition = async () => {
    if (!onStartExpedition) return;
    
    setIsStarting(true);
    
    try {
      // Simulate processing delay
      await new Promise(resolve => setTimeout(resolve, 2000));
      onStartExpedition();
    } catch (error) {
      console.error("Failed to start expedition:", error);
    } finally {
      setIsStarting(false);
    }
  };

  return (
    <Card className="bg-card border-border rounded-none h-full flex flex-col">
      <CardHeader>
        <div className="flex items-center justify-between">
          <CardTitle className="font-cinzel text-xl text-primary flex items-center gap-2">
            {expedition.name}
            {isOwner && <Crown className="h-5 w-5 text-warning" />}
          </CardTitle>
          <Badge className={getDifficultyColor(expedition.difficulty)}>
            {expedition.difficulty}
          </Badge>
        </div>
        <p className="text-muted-foreground font-crimson text-sm">
          {expedition.description}
        </p>
      </CardHeader>
      <CardContent>
        <div className="space-y-4">
          {/* Expedition Status */}
          <div className="flex items-center justify-between">
            <div className="flex items-center gap-2">
              <Users className="h-4 w-4" />
              <span className="font-crimson text-sm">
                {expedition.currentParticipants}/{expedition.maxParticipants} adventurers
              </span>
            </div>
            {expedition.status === ExpeditionStatus.CREATED && waitingCount > 0 && (
              <Badge variant="outline" className="flex items-center gap-1">
                <Clock className="h-3 w-3" />
                Waiting for {waitingCount} more
              </Badge>
            )}
            {isReady && (
              <Badge className="bg-player text-background">
                Ready to Begin!
              </Badge>
            )}
          </div>

          {/* Participants List */}
          <div className="space-y-2">
            <h4 className="font-crimson text-sm font-semibold">Brave Souls:</h4>
            <div className="grid grid-cols-2 gap-2">
              {expedition.participants.map((participant) => {
                const character = participant.character;
                const ClassIcon = character ? getClassIcon(character.class) : Users;
                
                return (
                  <div 
                    key={participant.user.userId}
                    className="flex items-center gap-2 p-2 bg-muted/30 rounded text-sm"
                  >
                    <ClassIcon className="w-3 h-3 text-primary" />
                    <span className="font-crimson">
                      {participant.user.name}
                      {participant.user.userId === expedition.createdByUser.userId && " (Leader)"}
                    </span>
                  </div>
                );
              })}
            </div>
          </div>

          {/* Owner Actions */}
          {isOwner && isReady && onStartExpedition && (
            <div className="pt-2 border-t border-border">
              <Button 
                onClick={handleStartExpedition}
                className="w-full gap-2"
                variant="default"
                disabled={isStarting}
              >
                {isStarting ? (
                  <>
                    <Loader2 className="h-4 w-4 animate-spin" />
                    Weaving the Tale...
                  </>
                ) : (
                  <>
                    <Play className="h-4 w-4" />
                    Begin the Expedition
                  </>
                )}
              </Button>
              <p className="text-xs text-muted-foreground font-crimson text-center mt-2">
                {isStarting 
                  ? "Preparing the realm for your adventure..."
                  : "All adventurers have joined. You may now begin the tale..."
                }
              </p>
            </div>
          )}

          {!isOwner && isReady && (
            <div className="pt-2 border-t border-border">
              <p className="text-sm text-center text-muted-foreground font-crimson">
                Waiting for <span className="text-primary font-semibold">{expedition.createdByUser.name}</span> to begin the expedition...
              </p>
            </div>
          )}
        </div>
      </CardContent>
    </Card>
  );
};