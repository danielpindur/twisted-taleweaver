import { useState, useEffect, useRef } from "react";
import { Button } from "@/components/ui/button";
import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Separator } from "@/components/ui/separator";
import { Progress } from "@/components/ui/progress";
import { Tooltip, TooltipContent, TooltipTrigger } from "@/components/ui/tooltip";
import { 
  Heart, 
  Shield, 
  User,
  Star,
  Wind,
  ChevronLeft,
  ChevronRight,
  Sword,
  Sparkles,
  Zap,
  Eye} from "lucide-react";
import type { Character } from "@/interfaces/Character";
import { getGenderSymbol } from "@/utils/CharacterUtils";
import { getHealthColor, getHealthPercentage } from "@/utils/HealthUtils";
import { formatModifier, getAttributeModifier } from "@/utils/AttributeUtils";
import { canUserAct } from "@/utils/TurnUtils";
import type { UserResource } from "@clerk/types";

export interface CharacterSidePanelProps {
  character: Character,
  panelCollapsed: boolean,
  setPanelCollapsed: (collapsed: boolean) => void,
  user: UserResource,
  isInCombat: boolean,
  characters: Character[]
};

export const CharacterSidePanel = ({ character, panelCollapsed, setPanelCollapsed, user, isInCombat, characters }: CharacterSidePanelProps) => {
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
  }, [character, panelCollapsed]);
  
  const isUserTurn = canUserAct(characters, user.username!);
  
  const handleActionClick = (actionName: string) => {
    if (!isUserTurn) return;
    console.log(`Using action: ${actionName}`);
    // TODO: Implement action execution
  };

  return (
    <div className={`${panelCollapsed ? 'w-12' : 'w-56 md:w-72'} flex-shrink-0 bg-card border-l border-border transition-[width] duration-300 overflow-hidden`}>
      {panelCollapsed ? (
        <div className="p-2 h-full flex flex-col items-center justify-start pt-4 gap-3">
          <Button variant="ghost" size="icon" onClick={() => setPanelCollapsed(false)} className="h-8 w-8">
            <ChevronLeft className="h-4 w-4" />
          </Button>
          <User className="h-5 w-5 text-muted-foreground" />
          <div className="writing-mode-vertical text-xs text-muted-foreground font-cinzel transform rotate-180" style={{ writingMode: 'vertical-rl' }}>
            CHARACTER
          </div>
        </div>
      ) : (
        <div 
          ref={scrollContainerRef}
          className={`w-72 px-4 pt-4 overflow-y-auto h-full ${needsScrollPadding ? 'pr-2' : 'pr-4'}`}
        >
          <div className="flex items-center justify-between mb-4">
            <Button variant="ghost" size="icon" onClick={() => setPanelCollapsed(true)}>
              <ChevronRight className="h-4 w-4" />
            </Button>
            <h2 className="text-lg font-cinzel font-semibold text-primary">
              Your Character
            </h2>
          </div>

      <Card className="bg-muted border-border">
        <CardContent className="space-y-4 pt-6">
          {/* Character Info */}
          <div className="text-center space-y-1">
            <div className="font-cinzel text-lg font-bold text-foreground">
              {character.name}
            </div>
            <div className="text-xs text-muted-foreground capitalize">
              Lvl {character.level} {character.class} • {character.creatureType} {getGenderSymbol(character.gender)}
            </div>
          </div>

          <Separator />

          {/* Stats */}
          <div className="space-y-2">
            <div>
              <div className="flex justify-between text-sm mb-1">
                <span className="flex items-center gap-1 font-crimson">
                  <Heart className="h-4 w-4" />
                  Health
                </span>
                <span>{character.health}/{character.maxHealth}</span>
              </div>
              <Progress 
                value={getHealthPercentage(character.health, character.maxHealth)}
                className={`h-2 ${getHealthColor(getHealthPercentage(character.health, character.maxHealth))}`}
              />
            </div>
            
            <div className="flex justify-between text-sm">
              <span className="flex items-center gap-1 font-crimson">
                <Shield className="h-4 w-4" />
                Armor Class
              </span>
              <span className="font-medium">{character.armor}</span>
            </div>
          </div>

          <Separator />

          {/* Attributes */}
          <div>
            <h3 className="font-cinzel font-medium text-sm mb-2">Attributes</h3>
            <div className="grid grid-cols-2 gap-x-4 gap-y-1">
              {Object.entries(character.attributes).map(([attr, value]) => (
                <div key={attr} className="flex justify-between items-center text-xs">
                  <span className="font-crimson capitalize">{attr}</span>
                  <div className="flex items-center gap-1">
                    <span className="font-medium">{value}</span>
                    <span className="text-muted-foreground">
                      {formatModifier(getAttributeModifier(value))}
                    </span>
                  </div>
                </div>
              ))}
            </div>
          </div>

          <Separator />

          {/* Essential Info */}
          <div className="space-y-3">
            {/* Top Traits as badges */}
            <div className="flex items-center gap-1 flex-wrap">
              {character.traits.slice(0, 2).map((trait, idx) => {
                console.log('Rendering tooltip for trait:', trait.name);
                return (
                  <Tooltip key={idx}>
                    <TooltipTrigger asChild>
                      <div 
                        className="inline-flex"
                        onMouseEnter={() => console.log('Mouse entered trait badge:', trait.name)}
                      >
                        <Badge variant="secondary" className="text-xs px-2 py-0.5 cursor-help">
                          <Star className="h-2 w-2 mr-1" />
                          {trait.name}
                        </Badge>
                      </div>
                    </TooltipTrigger>
                    <TooltipContent side="top" className="max-w-xs bg-popover border border-border text-popover-foreground shadow-md z-50">
                      <div className="space-y-1 p-2">
                        <p className="font-medium">{trait.name}</p>
                        <p className="text-sm">{trait.effect}</p>
                        {trait.conditionForPerform && (
                          <p className="text-xs text-accent">{trait.conditionForPerform}</p>
                        )}
                      </div>
                    </TooltipContent>
                  </Tooltip>
                );
              })}
            </div>

            {/* Speed */}
            <div className="flex items-center justify-between text-xs">
              <span className="flex items-center gap-1 text-muted-foreground">
                <Wind className="h-3 w-3" />
                Speed
              </span>
              <span className="font-medium">{character.speedScores.walking} ft</span>
            </div>
          </div>

          <Separator />

          {/* Actions */}
          <div>
            <h3 className="font-cinzel font-medium text-sm mb-2">Actions</h3>
            <div className="flex gap-2 justify-center">
              {(isInCombat ? character.combatActions : character.nonCombatActions).slice(0, 4).map((action, idx) => {
                const getActionIcon = (actionName: string) => {
                  if (actionName.toLowerCase().includes('attack') || actionName.toLowerCase().includes('strike') || actionName.toLowerCase().includes('slash') || actionName.toLowerCase().includes('whirlwind')) return Sword;
                  if (actionName.toLowerCase().includes('spell') || actionName.toLowerCase().includes('magic')) return Sparkles;
                  if (actionName.toLowerCase().includes('defend') || actionName.toLowerCase().includes('block') || actionName.toLowerCase().includes('shield') || actionName.toLowerCase().includes('bash')) return Shield;
                  if (actionName.toLowerCase().includes('shock') || actionName.toLowerCase().includes('lightning')) return Zap;
                  return Eye; // Default for search/investigate actions
                };

                const getActionColor = (actionName: string) => {
                  const name = actionName.toLowerCase();
                  // Elemental colors
                  if (name.includes('ice') || name.includes('frost') || name.includes('cold')) {
                    return 'bg-blue-500/20 text-blue-400 hover:bg-blue-500/30';
                  }
                  if (name.includes('fire') || name.includes('flame') || name.includes('burn')) {
                    return 'bg-red-500/20 text-red-400 hover:bg-red-500/30';
                  }
                  if (name.includes('lightning') || name.includes('shock') || name.includes('thunder')) {
                    return 'bg-yellow-500/20 text-yellow-400 hover:bg-yellow-500/30';
                  }
                  if (name.includes('poison') || name.includes('toxic') || name.includes('venom')) {
                    return 'bg-green-500/20 text-green-400 hover:bg-green-500/30';
                  }
                  if (name.includes('shadow') || name.includes('dark') || name.includes('void')) {
                    return 'bg-purple-500/20 text-purple-400 hover:bg-purple-500/30';
                  }
                  if (name.includes('holy') || name.includes('divine') || name.includes('light')) {
                    return 'bg-amber-500/20 text-amber-400 hover:bg-amber-500/30';
                  }
                  // Default for regular actions
                  return 'bg-primary/10 text-primary hover:bg-primary/20';
                };

                const ActionIcon = getActionIcon(action.name);
                const isRecharging = action.recharge != null && action.recharge > 0;
                const isDisabled = !isUserTurn || isRecharging;
                const colorClass = isDisabled ? 'bg-muted text-muted-foreground cursor-not-allowed' : getActionColor(action.name);

                return (
                  <Tooltip key={idx}>
                    <TooltipTrigger asChild>
                      <button
                        className={`p-1.5 rounded-full transition-all ${!isDisabled ? 'hover:scale-110' : ''} ${colorClass}`}
                        onClick={() => handleActionClick(action.name)}
                        disabled={isDisabled}
                      >
                        <ActionIcon className="h-4 w-4" />
                      </button>
                    </TooltipTrigger>
                    <TooltipContent side="left" className="max-w-xs bg-popover border border-border text-popover-foreground shadow-md z-50">
                      <div className="space-y-1 p-2">
                        <p className="font-medium text-sm">{action.name}</p>
                        <p className="text-xs text-muted-foreground">{action.description}</p>
                        {action.range && (
                          <p className="text-xs text-muted-foreground">Range: {action.range} ft</p>
                        )}
                        {action.cost && (
                          <p className="text-xs text-muted-foreground">Cost: {action.cost}</p>
                        )}
                        {action.recharge && (
                          <p className="text-xs text-accent">Recharge: {action.recharge}+</p>
                        )}
                        {!isUserTurn && (
                          <p className="text-xs text-warning">Wait for your turn</p>
                        )}
                      </div>
                    </TooltipContent>
                  </Tooltip>
                );
              })}
            </div>
          </div>

        </CardContent>
      </Card>

        </div>
      )}
    </div>
  );
};