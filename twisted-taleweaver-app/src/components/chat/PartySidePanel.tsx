import { useState, useEffect, useRef } from "react";
import { Button } from "@/components/ui/button";
import { 
  Crown, 
  Heart, 
  Shield, 
  Swords, 
  Users,
  Brain,
  Star,
  Award,
  Target,
  Wind,
  Wrench,
  Clock,
  ChevronLeft,
  ChevronRight} from "lucide-react";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Separator } from "@/components/ui/separator";
import { Progress } from "@/components/ui/progress";
import { Tooltip, TooltipContent, TooltipTrigger } from "@/components/ui/tooltip";
import { getGenderSymbol, getStatusColor } from "@/utils/CharacterUtils";
import { getHealthPercentage } from "@/utils/HealthUtils";
import { formatModifier, getAttributeModifier } from "@/utils/AttributeUtils";
import type { ExpeditionParticipant } from "@/generated/twisted-taleweaver-api";

export interface PartySidePannelProps {
  participants: ExpeditionParticipant[],
  panelCollapsed: boolean,
  setPanelCollapsed: (collapsed: boolean) => void;
};

export const PartySidePannel = ({ participants, panelCollapsed, setPanelCollapsed }: PartySidePannelProps) => {
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
          <Dialog key={participant.user.userId}>
            <DialogTrigger asChild>
              <Card className={`bg-muted border cursor-pointer hover:bg-muted/80 transition-colors ${player.isCurrentTurn ? 'border-primary shadow-glow' : 'border-border'}`}>
                <CardContent className="p-3">
                  <div className="flex items-center justify-between mb-2">
                    <div>
                      <div className="flex items-center gap-2">
                        {player.isCurrentTurn && <Crown className="h-4 w-4 text-primary" />}
                        <span className="font-crimson font-medium">{player.name}</span>
                      </div>
                      <div className="text-xs text-muted-foreground capitalize">
                        {player.creatureType} {getGenderSymbol(player.gender)}
                      </div>
                    </div>
                    <Badge variant="outline" className={getStatusColor(player.status)}>
                      {player.status}
                    </Badge>
                  </div>
                  
                  {/* Health Bar */}
                  <div className="space-y-1 mb-2">
                    <div className="flex items-center justify-between text-xs">
                      <span className="flex items-center gap-1">
                        <Heart className="h-3 w-3" />
                        Health
                      </span>
                      <span>{player.health}/{player.maxHealth}</span>
                    </div>
                    <Progress 
                      value={getHealthPercentage(player.health, player.maxHealth)} 
                      className="h-2"
                    />
                  </div>

                  {/* Armor */}
                  <div className="flex items-center justify-between text-xs text-muted-foreground">
                    <span className="flex items-center gap-1">
                      <Shield className="h-3 w-3" />
                      Armor
                    </span>
                    <span>{player.armor}</span>
                  </div>
                </CardContent>
              </Card>
            </DialogTrigger>
            
            <DialogContent className="bg-card border-border max-w-2xl max-h-[90vh] overflow-hidden">
              <DialogHeader>
                <DialogTitle className="font-cinzel text-primary">
                  {player.name}
                </DialogTitle>
                <DialogDescription className="text-sm text-muted-foreground">
                  Character details and abilities
                </DialogDescription>
              </DialogHeader>
              
              <ScrollArea className="max-h-[75vh] pr-4">
                <div className="space-y-4">
                  {/* Character Info */}
                  <div className="grid grid-cols-2 gap-4 text-sm">
                    <div>
                      <span className="text-muted-foreground">Level:</span>
                      <span className="ml-2 font-medium">{player.level}</span>
                    </div>
                    <div>
                      <span className="text-muted-foreground">Class:</span>
                      <span className="ml-2 font-medium">{player.class}</span>
                    </div>
                    <div>
                      <span className="text-muted-foreground">Type:</span>
                      <span className="ml-2 font-medium capitalize">{player.creatureType}</span>
                    </div>
                    <div>
                      <span className="text-muted-foreground">Gender:</span>
                      <span className="ml-2 font-medium capitalize">{player.gender}</span>
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
                        <span>{player.health}/{player.maxHealth}</span>
                      </div>
                      <Progress 
                        value={getHealthPercentage(player.health, player.maxHealth)}
                        className="h-3"
                      />
                    </div>

                    <div className="grid grid-cols-2 gap-4 text-sm">
                      <div className="flex justify-between">
                        <span className="flex items-center gap-1 font-crimson">
                          <Shield className="h-4 w-4" />
                          AC
                        </span>
                        <span className="font-medium">{player.armor}</span>
                      </div>
                      <div className="flex justify-between">
                        <span className="font-crimson">Status</span>
                        <Badge variant="outline" className={getStatusColor(player.status)}>
                          {player.status}
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
                        <span className="font-medium">{player.speedScores.walking} ft</span>
                      </div>
                      {player.speedScores.flying && (
                        <div className="flex justify-between">
                          <span className="text-muted-foreground">Flying</span>
                          <span className="font-medium">{player.speedScores.flying} ft</span>
                        </div>
                      )}
                      {player.speedScores.swimming && (
                        <div className="flex justify-between">
                          <span className="text-muted-foreground">Swimming</span>
                          <span className="font-medium">{player.speedScores.swimming} ft</span>
                        </div>
                      )}
                      {player.speedScores.climbing && (
                        <div className="flex justify-between">
                          <span className="text-muted-foreground">Climbing</span>
                          <span className="font-medium">{player.speedScores.climbing} ft</span>
                        </div>
                      )}
                    </div>
                  </div>

                  <Separator />

                  {/* Attributes */}
                  <div>
                    <h3 className="font-cinzel font-medium mb-2 text-sm">Attributes</h3>
                    <div className="grid grid-cols-2 gap-x-6 gap-y-2 text-sm">
                      {Object.entries(player.attributes).map(([attr, value]) => (
                        <div key={attr} className="flex justify-between items-center">
                          <span className="font-crimson capitalize">{attr}</span>
                          <div className="flex items-center gap-2">
                            <span className="font-medium">{value}</span>
                            <span className="text-xs text-muted-foreground min-w-[2rem] text-right">
                              {formatModifier(getAttributeModifier(value))}
                            </span>
                          </div>
                        </div>
                      ))}
                    </div>
                  </div>

                  {/* Collapsible Sections */}
                  <div className="space-y-2">
                    <details className="group">
                      <summary className="cursor-pointer font-cinzel font-medium text-sm flex items-center gap-2 p-2 hover:bg-muted/50 rounded">
                        <Star className="h-4 w-4" />
                        Traits ({player.traits.length})
                      </summary>
                      <div className="mt-2 space-y-2 pl-6">
                        {player.traits.map((trait, idx) => (
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

                    <details className="group">
                      <summary className="cursor-pointer font-cinzel font-medium text-sm flex items-center gap-2 p-2 hover:bg-muted/50 rounded">
                        <Swords className="h-4 w-4" />
                        Combat Actions ({player.combatActions.length})
                      </summary>
                      <div className="mt-2 space-y-2 pl-6">
                        {player.combatActions.map((action, idx) => (
                          <div key={idx} className="p-2 bg-muted/30 rounded text-xs">
                            <div className="flex justify-between items-start mb-1">
                              <span className="font-medium text-sm">{action.name}</span>
                              <div className="flex items-center gap-1">
                                <Badge variant="outline" className="text-xs px-1">
                                  Cost: {action.cost}
                                </Badge>
                                {action.recharge && (
                                  <Badge variant="outline" className="text-xs px-1">
                                    <Clock className="h-2 w-2 mr-1" />
                                    {action.recharge}+
                                  </Badge>
                                )}
                              </div>
                            </div>
                            <div className="text-muted-foreground">{action.description}</div>
                            {action.range && (
                              <div className="text-accent mt-1">Range: {action.range} ft</div>
                            )}
                          </div>
                        ))}
                      </div>
                    </details>

                    <details className="group">
                      <summary className="cursor-pointer font-cinzel font-medium text-sm flex items-center gap-2 p-2 hover:bg-muted/50 rounded">
                        <Wrench className="h-4 w-4" />
                        Utility Actions ({player.nonCombatActions.length})
                      </summary>
                      <div className="mt-2 space-y-2 pl-6">
                        {player.nonCombatActions.map((action, idx) => (
                          <div key={idx} className="p-2 bg-muted/30 rounded text-xs">
                            <div className="flex justify-between items-start mb-1">
                              <span className="font-medium text-sm">{action.name}</span>
                              <div className="flex items-center gap-1">
                                <Badge variant="outline" className="text-xs px-1">
                                  Cost: {action.cost}
                                </Badge>
                                {action.recharge && (
                                  <Badge variant="outline" className="text-xs px-1">
                                    <Clock className="h-2 w-2 mr-1" />
                                    {action.recharge}+
                                  </Badge>
                                )}
                              </div>
                            </div>
                            <div className="text-muted-foreground">{action.description}</div>
                            {action.range && (
                              <div className="text-accent mt-1">Range: {action.range} ft</div>
                            )}
                          </div>
                        ))}
                      </div>
                    </details>

                    <details className="group">
                      <summary className="cursor-pointer font-cinzel font-medium text-sm flex items-center gap-2 p-2 hover:bg-muted/50 rounded">
                        <Award className="h-4 w-4" />
                        Proficiencies ({player.proficiencies.length})
                      </summary>
                      <div className="mt-2 space-y-3 pl-6">
                        {['Skill', 'Weapon', 'Armor', 'SavingThrow', 'Tool', 'Other'].map(type => {
                          const profs = player.proficiencies.filter(p => p.type === type);
                          if (profs.length === 0) return null;
                          
                          const getProficiencyIcon = (profType: string) => {
                            switch (profType) {
                              case 'Skill': return Brain;
                              case 'Weapon': return Swords;
                              case 'Armor': return Shield;
                              case 'SavingThrow': return Heart;
                              case 'Tool': return Wrench;
                              default: return Target;
                            }
                          };
                          
                          const Icon = getProficiencyIcon(type);
                          
                          return (
                            <div key={type}>
                              <h4 className="font-crimson text-sm text-muted-foreground mb-2 flex items-center gap-1">
                                <Icon className="h-3 w-3" />
                                {type === 'SavingThrow' ? 'Saving Throws' : type}s
                              </h4>
                              <div className="flex flex-wrap gap-1">
                                {profs.map((prof, idx) => (
                                  <Tooltip key={idx}>
                                    <TooltipTrigger asChild>
                                      <div className="inline-flex">
                                        <Badge variant="outline" className="text-xs cursor-help">
                                          {prof.target}
                                        </Badge>
                                      </div>
                                    </TooltipTrigger>
                                    <TooltipContent side="bottom" align="start" avoidCollisions={true} collisionPadding={20} sideOffset={5} className="max-w-xs bg-popover border border-border text-popover-foreground shadow-md z-50">
                                      <div className="space-y-1 p-2">
                                        <p className="font-medium">{prof.target}</p>
                                        <p className="text-sm capitalize">
                                          {type === 'SavingThrow' ? 'Saving Throw' : type} Proficiency
                                        </p>
                                        {type === 'Skill' && <p className="text-xs text-accent">Adds proficiency bonus to checks</p>}
                                        {type === 'Weapon' && <p className="text-xs text-accent">Adds proficiency bonus to attacks</p>}
                                        {type === 'SavingThrow' && <p className="text-xs text-accent">Adds proficiency bonus to saves</p>}
                                        {type === 'Tool' && <p className="text-xs text-accent">Adds proficiency bonus to tool use</p>}
                                        {type === 'Armor' && <p className="text-xs text-accent">Proficient with this armor type</p>}
                                      </div>
                                    </TooltipContent>
                                  </Tooltip>
                                ))}
                              </div>
                            </div>
                          );
                        })}
                      </div>
                    </details>
                  </div>
                </div>
              </ScrollArea>
            </DialogContent>
          </Dialog>
        ))}
        </div>
        </div>
      )}
    </div>  
  );
};