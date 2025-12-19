import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Separator } from "@/components/ui/separator";
import { Progress } from "@/components/ui/progress";
import { Tooltip, TooltipContent, TooltipTrigger } from "@/components/ui/tooltip";
import { Heart, Shield, Star, Wind } from "lucide-react";
import { getGenderSymbol, getCreatureCurrentHealth } from "@/utils/CharacterUtils";
import { getHealthColor, getHealthPercentage } from "@/utils/HealthUtils";
import { formatModifier, getAttributeModifier } from "@/utils/AttributeUtils";
import type { Character, ExpeditionState } from "@/generated/twisted-taleweaver-api";

export interface CharacterSidePanelContentProps {
  character: Character,
  expeditionState: ExpeditionState
};

export const CharacterSidePanelContent = ({ character, expeditionState }: CharacterSidePanelContentProps) => {
  return (
    <Card className="bg-muted border-border">
      <CardContent className="space-y-4 pt-6">
        {/* Character Info */}
        <div className="text-center space-y-1">
          <div className="font-cinzel text-lg font-bold text-foreground">
            {character.name}
          </div>
          <div className="text-xs text-muted-foreground capitalize">
            {character.class} • {character.creatureType} {getGenderSymbol(character.gender)}
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
              <span>{getCreatureCurrentHealth(character, expeditionState)}/{character.maxHealth}</span>
            </div>
            <Progress 
              value={getHealthPercentage(getCreatureCurrentHealth(character, expeditionState), character.maxHealth)}
              className={`h-2 ${getHealthColor(getHealthPercentage(getCreatureCurrentHealth(character, expeditionState), character.maxHealth))}`}
            />
          </div>
          
          <div className="flex justify-between text-sm">
            <span className="flex items-center gap-1 font-crimson">
              <Shield className="h-4 w-4" />
              Armor Class
            </span>
            <span className="font-medium">{character.armorClass}</span>
          </div>
        </div>

        <Separator />

        {/* Attributes */}
        <div>
          <h3 className="font-cinzel font-medium text-sm mb-2">Attributes</h3>
          <div className="grid grid-cols-2 gap-x-4 gap-y-1">
            {Object.entries(character.attributeScores).map(([attr, value]) => (
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
            {character.traits.map((trait, idx) => {
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

      </CardContent>
    </Card>
  );
};