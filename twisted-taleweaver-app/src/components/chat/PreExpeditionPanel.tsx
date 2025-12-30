import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { User, Users, Crown, Play, Clock, Loader2 } from "lucide-react";
import { CharacterCreationForm } from "@/components/chat/CharacterCreationForm";
import { getClassIcon } from "@/utils/CharacterUtils";
import { getDifficultyColor } from "@/utils/ExpeditionDifficultyUtils";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useToast } from "@/hooks/useToast";
import type { UserResource } from "@clerk/types";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";
import { useGraphQlClient } from "@/hooks/useGraphQlClient";
import { ExpeditionStatus, type StartExpeditionInput } from "@/__generated__/twisted-taleweaver-api/types";

interface PreExpeditionPanelProps {
  expedition: ExpeditionViewModel;
  user: UserResource;
}

export const PreExpeditionPanel = ({ expedition, user }: PreExpeditionPanelProps) => {
  const queryClient = useQueryClient();
  const { requestWithAuth } = useGraphQlClient();
  const { toast } = useToast();

  const isOwner = expedition.userCreated.externalId === user.id;
  const isReady = expedition.currentParticipants >= expedition.minParticipants;
  const waitingCount = expedition.minParticipants - expedition.currentParticipants;
  const currentParticipant = expedition.participants.find(p => p.user.externalId === user.id);
  const hasCharacter = currentParticipant && currentParticipant.character;
  const allHaveCharacters = expedition.participants.every(p => p.character);

  const startExpeditionMutation = useMutation({
    mutationFn: (input: StartExpeditionInput) =>
      requestWithAuth((sdk) => sdk.StartExpedition({ input })),

    onSuccess: (_, input) => {
      queryClient.invalidateQueries({ queryKey: ["expedition", input.expeditionId] });
    },

    onError: (_) => {
      toast({
        title: "Expedition failed to start",
        description: "Failed to start the expedtion. Try again.",
        variant: "destructive",
      });
    },
  });

  const handleStartExpedition = () => {
    const input: StartExpeditionInput = {
      expeditionId: expedition.id,
    }
    startExpeditionMutation.mutate(input);
  };

  return (
    <Card className="bg-card border-border rounded-none h-full flex flex-col">
      <CardHeader>
        <div className="flex items-center justify-between">
          <CardTitle className="font-cinzel text-xl text-primary flex items-center gap-2">
            {expedition.name}
          </CardTitle>
          <Badge className={getDifficultyColor(expedition.difficulty)}>
            {expedition.difficulty}
          </Badge>
        </div>
        <p className="text-muted-foreground font-crimson text-sm">
          {expedition.description}
        </p>
      </CardHeader>
      <CardContent className="flex-1 flex flex-col">
        <div className="space-y-4 flex-1">
          {/* Expedition Status */}
          <div className="flex items-center justify-between">
            <div className="flex items-center gap-2">
              <Users className="h-4 w-4" />
              <span className="font-crimson text-sm">
                {expedition.currentParticipants}/{expedition.maxParticipants} adventurers
              </span>
            </div>
            {expedition.state.status === ExpeditionStatus.Created && waitingCount > 0 && (
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

          {/* Character Creation Form */}
          {!hasCharacter && (
            <CharacterCreationForm expeditionId={expedition.id} />
          )}

          {/* Participants List */}
          <div className="space-y-2">
            <h4 className="font-crimson text-sm font-semibold">Brave Souls:</h4>
            <div className="space-y-2">
              <div className="grid grid-cols-2 gap-2">
                {expedition.participants.map((participant) => {
                  const character = participant.character;
                  const ClassIcon = character ? getClassIcon(character.class) : User;
                  const isCurrentUser = participant.user.externalId === user.id;
                  const isLeader = participant.user.externalId === expedition.userCreated.externalId;
                  
                  return (
                    <div 
                      key={participant.user.id}
                      className={`flex items-center justify-between p-2 rounded text-sm ${
                        isCurrentUser ? "bg-primary/10 border border-primary/20" : "bg-muted/30"
                      }`}
                    >
                      <div className="flex items-center gap-2">
                        <ClassIcon className="w-4 h-4 text-primary" />
                        <div className="flex flex-col">
                          <div className="flex items-center gap-1">
                            <span className="font-crimson text-xs text-muted-foreground">
                              {participant.user.name}
                            </span>
                            {isLeader && (
                              <Crown className="h-3.5 w-3.5 text-warning shrink-0" />
                            )}
                          </div>
                          {character ? (
                            <span className="font-crimson font-medium">
                              {character.name}
                              {character.class && (
                                <span className="text-muted-foreground text-xs ml-1">
                                  • {character.class}
                                </span>
                              )}
                            </span>
                          ) : (
                            <span className="font-crimson text-muted-foreground italic text-xs">
                              Creating character...
                            </span>
                          )}
                        </div>
                      </div>
                    </div>
                  );
                })}
              </div>
            </div>
          </div>
        </div>

        {/* Owner Actions */}
        {isOwner && isReady && (
          <div className="pt-4 mt-4 border-t border-border">
            <Button 
              onClick={handleStartExpedition}
              className="w-full gap-2"
              variant="default"
              disabled={startExpeditionMutation.isPending || startExpeditionMutation.isSuccess || !allHaveCharacters}
            >
              {startExpeditionMutation.isPending || startExpeditionMutation.isSuccess ? (
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
              { startExpeditionMutation.isPending || startExpeditionMutation.isSuccess 
                ? "Preparing the realm for your adventure..."
                : !allHaveCharacters
                  ? "Waiting for all adventurers to create their characters..." 
                  : "All adventurers have joined. You may now begin the tale..."
              }
            </p>
          </div>
        )}

        {!isOwner && isReady && (
          <div className="pt-4 mt-4 border-t border-border">
            <p className="text-sm text-center text-muted-foreground font-crimson">
              Waiting for <span className="text-primary font-semibold">{expedition.userCreated.name}</span> to begin the expedition...
            </p>
          </div>
        )}
      </CardContent>
    </Card>
  );
};