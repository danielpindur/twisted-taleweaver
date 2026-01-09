import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Badge } from "@/components/ui/badge";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { ScrollArea } from "@/components/ui/scroll-area";
import { 
  Users, 
  Skull, 
  Calendar, 
  Clock, 
  Scroll, 
  Sword, 
  Heart,
  User,
  Loader2,
  AlertCircle
} from "lucide-react";
import { getDifficultyColor, getOutcomeColor } from "@/utils/ExpeditionUtils";
import { formatDuration } from "@/utils/TimeUtils";
import { useGraphQlClient } from "@/hooks/useGraphQlClient";
import { useQuery } from "@tanstack/react-query";
import type { ArchivedExpeditionViewModel } from "@/viewmodels/ArchivedExpeditionViewModel";

interface ExpeditionDetailsDialogProps {
  expeditionId: string | null;
  open: boolean;
  onOpenChange: (open: boolean) => void;
}

export function ExpeditionDetailsDialog({ expeditionId, open, onOpenChange }: ExpeditionDetailsDialogProps) {
  
  const { requestWithAuth } = useGraphQlClient();

  const { data, isLoading, isError } = useQuery({
    queryKey: ["expeditions-archive", expeditionId],
    queryFn: () => {
      return requestWithAuth((sdk) => sdk.GetArchivedExpeditionById({ id: expeditionId }));
    },
  });

  const getParticipantOutcomeColor = (participant: ArchivedExpeditionViewModel["participants"][number]) => {
    const participantState = data?.expeditionById!.state.creatureStates.find(
      cs => cs.creatureId === participant.character?.id
    );

    if (participantState?.currentHealth === participant.character!.maxHealth) {
      return "text-health";
    }

    if (participantState?.currentHealth! > 0) {
      return "text-warning";
    }
    
    return "text-destructive";
  };

  const getParticipantOutcome = (participant: ArchivedExpeditionViewModel["participants"][number]) => {
    const participantState = data?.expeditionById!.state.creatureStates.find(
      cs => cs.creatureId === participant.character?.id
    );

    if (participantState?.currentHealth === participant.character!.maxHealth) {
      return "Survived";
    }

    if (participantState?.currentHealth! > 0) {
      return "Injured";
    }
    
    return "Perished";
  };

  const getMonsterSurvived = (monster: ArchivedExpeditionViewModel["state"]["monsters"][number]) => {
    const participantState = data?.expeditionById!.state.creatureStates.find(
      cs => cs.creatureId === monster.id
    );

    return (participantState?.currentHealth ?? 0) > 0;
  };

  const countSurvivors = (expedition: ArchivedExpeditionViewModel) => {
    let aliveCount = 0;
  
    for (const participant of expedition.participants) {
      const characterId = participant.character?.id;
      if (!characterId) {
        continue;
      }
  
      // find the creature state matching the character
      const creatureState = expedition.state.creatureStates.find(
        cs => cs.creatureId === characterId
      );
  
      if (creatureState!.currentHealth > 0) {
        aliveCount++;
      }
    }
  
    return aliveCount;
  }

  if (!expeditionId) {
    return null;
  }

  if (isLoading) {
    return (
      <Dialog open={open} onOpenChange={onOpenChange}>
        <DialogContent className="max-w-4xl h-[90vh] flex flex-col">
          <div className="col-span-full flex justify-center items-center h-32">
            <Loader2 className="h-8 w-8 animate-spin text-primary" />
          </div>
        </DialogContent>
      </Dialog>
    );
  }

  if (isError || !data?.expeditionById)
  {
    return (
      <Dialog open={open} onOpenChange={onOpenChange}>
        <DialogContent className="max-w-4xl h-[90vh] flex flex-col">
          <div className="col-span-full flex justify-center items-center text-center text-red-500 font-crimson">
            <AlertCircle className="inline-block h-5 w-5 mr-1" />
            Failed to load expedition. Try refreshing the page.
          </div>
        </DialogContent>
      </Dialog>
    );
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="max-w-4xl h-[90vh] flex flex-col">
        <DialogHeader>
          <DialogTitle className="font-cinzel text-2xl text-primary">
            {data.expeditionById.name}
          </DialogTitle>
          <div className="flex gap-2 mt-2">
            <Badge className={getDifficultyColor(data.expeditionById.difficulty)}>
              {data.expeditionById.difficulty.toUpperCase()}
            </Badge>
            <Badge className={getOutcomeColor(data.expeditionById.outcome!)}>
              {data.expeditionById.outcome!.toUpperCase()}
            </Badge>
          </div>
        </DialogHeader>

        <ScrollArea className="flex-1 pr-4 h-full">
          <div className="space-y-6">
            {/* Expedition Summary */}
            <Card className="bg-card border-border">
              <CardHeader>
                <CardTitle className="font-cinzel text-lg text-primary flex items-center gap-2">
                  <Scroll className="h-5 w-5" />
                  Expedition Summary
                </CardTitle>
              </CardHeader>
              <CardContent className="space-y-4">
                <p className="font-crimson text-foreground">{data.expeditionById.description}</p>
                
                <div className="grid grid-cols-2 md:grid-cols-4 gap-4 text-sm">
                  <div className="flex items-center gap-2">
                    <Calendar className="h-4 w-4 text-muted-foreground" />
                    <span className="font-crimson">{new Date(data.expeditionById.completedAt!).toLocaleDateString()}</span>
                  </div>
                  <div className="flex items-center gap-2">
                    <Clock className="h-4 w-4 text-muted-foreground" />
                    <span className="font-crimson">{formatDuration(data.expeditionById.startedAt!, data.expeditionById.completedAt!)}</span>
                  </div>
                  <div className="flex items-center gap-2">
                    <Users className="h-4 w-4 text-muted-foreground" />
                    <span className="font-crimson">{data.expeditionById.currentParticipants} Heroes</span>
                  </div>
                  <div className="flex items-center gap-2">
                    <Heart className="h-4 w-4 text-health" />
                    <span className="font-crimson">{countSurvivors(data.expeditionById)} Survivors</span>
                  </div>
                </div>
              </CardContent>
            </Card>

            {/* Participants */}
            {<Card className="bg-card border-border">
              <CardHeader>
                <CardTitle className="font-cinzel text-lg text-primary flex items-center gap-2">
                  <Users className="h-5 w-5" />
                  Heroes of the Expedition
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
                  {data.expeditionById.participants.map((participant, index) => (
                    <div 
                      key={index}
                      className="flex items-center gap-3 p-3 rounded-lg bg-background border border-border"
                    >
                      <User className="h-5 w-5 text-muted-foreground" />
                      <div className="flex-1">
                        <div className="font-cinzel font-semibold">{participant.character!.name}</div>
                        <div className="text-sm text-muted-foreground font-crimson">{participant.character!.class}</div>
                      </div>
                      <div className={`text-sm font-crimson font-semibold ${getParticipantOutcomeColor(participant)}`}>
                        {getParticipantOutcome(participant).toUpperCase()}
                      </div>
                    </div>
                  ))}
                </div>
              </CardContent>
            </Card>}

            {/* Chronicle of Key Events */}
            {<Card className="bg-card border-border">
              <CardHeader>
                <CardTitle className="font-cinzel text-lg text-primary flex items-center gap-2">
                  <Scroll className="h-5 w-5" />
                  Chronicle of Events
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="space-y-4">
                  {data.expeditionById.narrativeMessages.map((message, index) => (
                    <div key={index} className="flex gap-3">
                      <div className="flex-1 pb-4">
                        <div className="text-sm text-muted-foreground font-crimson mb-1">
                          {new Date(message.timestamp).toLocaleString()}
                        </div>
                        <div className="font-crimson text-foreground">
                          {message.content}
                        </div>
                      </div>
                    </div>
                  ))}
                </div>
              </CardContent>
            </Card>}

            {/* Enemies Encountered */}
            <Card className="bg-card border-border">
              <CardHeader>
                <CardTitle className="font-cinzel text-lg text-primary flex items-center gap-2">
                  <Sword className="h-5 w-5" />
                  Enemies Encountered
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="space-y-3">
                  {data.expeditionById.state.monsters.map((monster, index) => (
                    <div 
                      key={index}
                      className="flex items-start gap-3 p-3 rounded-lg bg-background border border-border"
                    >
                      <Skull className={`h-5 w-5 mt-0.5 ${getMonsterSurvived(monster) ? 'text-destructive' : 'text-health'}`} />
                      <div className="flex-1">
                        <div className="font-cinzel font-semibold flex items-center gap-2">
                          {monster.name}
                          <Badge variant={getMonsterSurvived(monster) ? "destructive" : "default"} className="text-xs">
                            {getMonsterSurvived(monster)? "Survived" : "Defeated"}
                          </Badge>
                        </div>
                        <div className="text-sm text-muted-foreground font-crimson mt-1">
                          {monster.description}
                        </div>
                      </div>
                    </div>
                  ))}
                </div>
              </CardContent>
            </Card>

            {/* Legacy */}
            <Card className="bg-card border-border">
              <CardHeader>
                <CardTitle className="font-cinzel text-lg text-primary flex items-center gap-2">
                  <Scroll className="h-5 w-5" />
                  Legacy & Lessons
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="font-crimson text-foreground italic border-l-4 border-primary pl-4">
                  "{data.expeditionById.lessonLearned}"
                </div>
                <div className="text-right mt-4 text-sm text-muted-foreground font-crimson">
                  — From the Chronicles of the Shadowlands
                </div>
              </CardContent>
            </Card>
          </div>
        </ScrollArea>
      </DialogContent>
    </Dialog>
  );
}