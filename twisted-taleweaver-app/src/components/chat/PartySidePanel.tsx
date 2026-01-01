import { useState, useEffect, useRef } from "react";
import { Button } from "@/components/ui/button";
import { 
  Crown, 
  Heart, 
  Shield, 
  Users,
  Star,
  Wind,
  ChevronLeft,
  ChevronRight,
  Loader2,
  UserCircle } from "lucide-react";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Separator } from "@/components/ui/separator";
import { Progress } from "@/components/ui/progress";
import { getCreatureCurrentHealth, getCreatureStatus, getGenderSymbol, getStatusColor } from "@/utils/CharacterUtils";
import { getHealthPercentage, getHealthColor } from "@/utils/HealthUtils";
import { formatModifier, getAttributeModifier } from "@/utils/AttributeUtils";
import { canParticipantAct } from "@/utils/TurnUtils"
import { ATTRIBUTE_KEYS } from "@/viewmodels/AttributeScoresViewModel";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";

export interface PartySidePanelProps {
  participants: ExpeditionViewModel["participants"],
  expeditionState: ExpeditionViewModel["state"],
  expeditionOwner: ExpeditionViewModel["userCreated"],
  panelCollapsed: boolean,
  activeUser: ExpeditionViewModel["state"]["activeUser"], 
  setPanelCollapsed: (collapsed: boolean) => void;
};

export const PartySidePanel = ({ participants, expeditionState, expeditionOwner, panelCollapsed, activeUser, setPanelCollapsed }: PartySidePanelProps) => {
  const [needsScrollPadding, setNeedsScrollPadding] = useState(false);
  const scrollContainerRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const checkScrollNeed = () => {
      if (scrollContainerRef.current) {
        const { scrollHeight, clientHeight } = scrollContainerRef.current;
        setNeedsScrollPadding(scrollHeight > clientHeight);
      }
    };

    checkScrollNeed();
    window.addEventListener('resize', checkScrollNeed);
    return () => window.removeEventListener('resize', checkScrollNeed);
  }, [participants, panelCollapsed]);

  return (
    <div className={`${panelCollapsed ? 'w-12' : 'w-56 md:w-72'} flex-shrink-0 bg-card border-r border-border transition-[width] duration-300 overflow-hidden`}>
      {panelCollapsed ? (
        <div className="p-2 h-full flex flex-col items-center justify-start pt-4 gap-3">
          <Button variant="ghost" size="icon" onClick={() => setPanelCollapsed(false)} className="h-8 w-8">
            <ChevronRight className="h-4 w-4" />
          </Button>
          <Users className="h-5 w-5 text-muted-foreground" />
          <div className="writing-mode-vertical text-xs text-muted-foreground font-cinzel transform rotate-180" style={{ writingMode: 'vertical-rl' }}>
            PARTY
          </div>
        </div>
      ) : (
        <div 
          ref={scrollContainerRef}
          className={`w-72 px-4 pt-4 overflow-y-auto h-full ${needsScrollPadding ? 'pr-2' : 'pr-4'}`}
        >
          <div className="flex items-center justify-between mb-4">
            <h2 className="text-lg font-cinzel font-semibold text-primary">
              The Doomed Party
            </h2>
            <Button variant="ghost" size="icon" onClick={() => setPanelCollapsed(true)}>
              <ChevronLeft className="h-4 w-4" />
            </Button>
          </div>
      <div className="space-y-3">
        {participants.map((participant) => (
          participant.character ? 
            <Dialog key={participant.user.id}>
              <DialogTrigger asChild>
                <Card className={`bg-muted border cursor-pointer hover:bg-muted/80 transition-colors ${canParticipantAct(participant, activeUser) ? 'border-primary shadow-glow' : 'border-border'}`}>
                  <CardContent className="p-3">
                    <div className="flex items-center justify-between mb-2">
                      <div>
                        <div className="flex items-center gap-2">
                          {participant.user.id === expeditionOwner.id && <Crown className="h-4 w-4 text-primary" />}
                          <span className="font-crimson font-medium">{participant.character.name}</span>
                        </div>
                        <div className="text-xs text-muted-foreground capitalize">
                          {participant.character.creatureType} {getGenderSymbol(participant.character.gender)}
                        </div>
                      </div>
                      <Badge variant="outline" className={getStatusColor(getCreatureStatus(participant.character, expeditionState))}>
                        {getCreatureStatus(participant.character, expeditionState)}
                      </Badge>
                    </div>
                    
                    {/* Health Bar */}
                    <div className="space-y-1 mb-2">
                      <div className="flex items-center justify-between text-xs">
                        <span className="flex items-center gap-1">
                          <Heart className="h-3 w-3" />
                          Health
                        </span>
                        <span>{getCreatureCurrentHealth(participant.character, expeditionState)}/{participant.character.maxHealth}</span>
                      </div>
                      <Progress 
                        value={getHealthPercentage(getCreatureCurrentHealth(participant.character, expeditionState), participant.character.maxHealth)} 
                        className={`h-2 ${getHealthColor(getHealthPercentage(getCreatureCurrentHealth(participant.character, expeditionState), participant.character.maxHealth))}`}
                      />
                    </div>

                    {/* Armor */}
                    <div className="flex items-center justify-between text-xs text-muted-foreground">
                      <span className="flex items-center gap-1">
                        <Shield className="h-3 w-3" />
                        Armor
                      </span>
                      <span>{participant.character.armorClass}</span>
                    </div>
                  </CardContent>
                </Card>
              </DialogTrigger>
              
              <DialogContent className="bg-card border-border max-w-2xl max-h-[90vh] overflow-hidden">
                <DialogHeader>
                  <DialogTitle className="font-cinzel text-primary">
                    {participant.character.name}
                  </DialogTitle>
                  <DialogDescription className="text-sm text-muted-foreground">
                    {participant.character.description}
                  </DialogDescription>
                </DialogHeader>
                
                <ScrollArea className="max-h-[75vh] pr-4">
                  
                  <div className="space-y-4">

                    <Separator />

                    {/* Character Info */}
                    <div className="grid grid-cols-2 gap-4 text-sm">
                      <div>
                        <span className="text-muted-foreground">Class:</span>
                        <span className="ml-2 font-medium">{participant.character.class}</span>
                      </div>
                      <div>
                        <span className="text-muted-foreground">Type:</span>
                        <span className="ml-2 font-medium capitalize">{participant.character.creatureType}</span>
                      </div>
                      <div>
                        <span className="text-muted-foreground">Gender:</span>
                        <span className="ml-2 font-medium capitalize">{participant.character.gender}</span>
                      </div>
                    </div>

                    <Separator />

                    {/* Health & Status */}
                    <div className="space-y-3">
                      <div>
                        <div className="flex justify-between text-sm mb-1">
                          <span className="flex items-center gap-1 font-crimson">
                            <Heart className="h-4 w-4" />
                            Health
                          </span>
                          {<span>{getCreatureCurrentHealth(participant.character, expeditionState)}/{participant.character.maxHealth}</span>}
                        </div>
                        <Progress 
                          value={getHealthPercentage(getCreatureCurrentHealth(participant.character, expeditionState), participant.character.maxHealth)}
                          className={`h-3 ${getHealthColor(getHealthPercentage(getCreatureCurrentHealth(participant.character, expeditionState), participant.character.maxHealth))}`}
                        />
                      </div>

                      <div className="grid grid-cols-2 gap-4 text-sm">
                        <div className="flex justify-between">
                          <span className="flex items-center gap-1 font-crimson">
                            <Shield className="h-4 w-4" />
                            AC
                          </span>
                          <span className="font-medium">{participant.character.armorClass}</span>
                        </div>
                        <div className="flex justify-between">
                          <span className="font-crimson">Status</span>
                          <Badge variant="outline" className={getStatusColor(getCreatureStatus(participant.character, expeditionState))}>
                            {getCreatureStatus(participant.character, expeditionState)}
                          </Badge>
                        </div>
                      </div>
                    </div>

                    <Separator />

                    {/* Movement Speeds */}
                    <div>
                      <h3 className="font-cinzel font-medium mb-2 text-sm flex items-center gap-2">
                        <Wind className="h-4 w-4" />
                        Movement
                      </h3>
                      <div className="grid grid-cols-2 gap-3 text-sm">
                        <div className="flex justify-between">
                          <span className="text-muted-foreground">Walking</span>
                          <span className="font-medium">{participant.character?.speedScores.walking} ft</span>
                        </div>
                        {participant.character?.speedScores.flying && (
                          <div className="flex justify-between">
                            <span className="text-muted-foreground">Flying</span>
                            <span className="font-medium">{participant.character?.speedScores.flying} ft</span>
                          </div>
                        )}
                        {participant.character?.speedScores.swimming && (
                          <div className="flex justify-between">
                            <span className="text-muted-foreground">Swimming</span>
                            <span className="font-medium">{participant.character?.speedScores.swimming} ft</span>
                          </div>
                        )}
                        {participant.character?.speedScores.climbing && (
                          <div className="flex justify-between">
                            <span className="text-muted-foreground">Climbing</span>
                            <span className="font-medium">{participant.character?.speedScores.climbing} ft</span>
                          </div>
                        )}
                        {participant.character?.speedScores.burrowing && (
                          <div className="flex justify-between">
                            <span className="text-muted-foreground">Burrowing</span>
                            <span className="font-medium">{participant.character?.speedScores.burrowing} ft</span>
                          </div>
                        )}
                        {participant.character?.speedScores.teleportation && (
                          <div className="flex justify-between">
                            <span className="text-muted-foreground">Teleportation</span>
                            <span className="font-medium">{participant.character?.speedScores.teleportation} ft</span>
                          </div>
                        )}
                      </div>
                    </div>

                    <Separator />

                    {/* Attributes */}
                    <div>
                      <h3 className="font-cinzel font-medium mb-2 text-sm">Attributes</h3>
                      <div className="grid grid-cols-2 gap-x-6 gap-y-2 text-sm">
                        {participant.character &&
                          ATTRIBUTE_KEYS.map((attr) => {
                            const value = participant.character!.attributeScores[attr];

                            return (
                              <div key={attr} className="flex justify-between items-center">
                                <span className="font-crimson capitalize">{attr}</span>

                                <div className="flex items-center gap-2">
                                  <span className="font-medium">{value}</span>

                                  <span className="text-xs text-muted-foreground min-w-[2rem] text-right">
                                    {formatModifier(getAttributeModifier(value))}
                                  </span>
                                </div>
                              </div>
                            );
                          })}
                      </div>
                    </div>

                    {/* Collapsible Sections */}
                    <div className="space-y-2">
                      <details className="group">
                        <summary className="cursor-pointer font-cinzel font-medium text-sm flex items-center gap-2 p-2 hover:bg-muted/50 rounded">
                          <Star className="h-4 w-4" />
                          Traits ({participant.character?.traits.length ?? 0})
                        </summary>
                        <div className="mt-2 space-y-2 pl-6">
                          {participant.character?.traits.map((trait, idx) => (
                            <div key={idx} className="p-2 bg-muted/30 rounded text-xs">
                              <div className="font-medium text-sm">{trait.name}</div>
                              <div className="text-muted-foreground">{trait.effect}</div>
                              {trait.conditionForPerform && (
                                <div className="text-accent mt-1">{trait.conditionForPerform}</div>
                              )}
                            </div>
                          ))}
                        </div>
                      </details>

                    </div>
                  </div>
                </ScrollArea>
              </DialogContent>
            </Dialog> :
        
            <Card 
              key={participant.user.id} 
              className="bg-muted/50 border border-dashed border-border"
            >
              <CardContent className="p-3">
                <div className="flex items-center justify-between mb-2">
                  <div>
                    <div className="flex items-center gap-2">
                      <UserCircle className="h-4 w-4 text-muted-foreground" />
                      <span className="font-crimson font-medium text-muted-foreground">
                        {participant.user.name}
                      </span>
                    </div>
                    <div className="text-xs text-muted-foreground italic mt-1">
                      Creating character...
                    </div>
                  </div>
                  <Badge variant="outline" className="text-amber-500 border-amber-500/50">
                    <Loader2 className="h-3 w-3 mr-1 animate-spin" />
                    Pending
                  </Badge>
                </div>
              </CardContent>
            </Card>
          
        ))}
        </div>
        </div>
      )}
    </div>  
  );
};