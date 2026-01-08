import { Badge } from "@/components/ui/badge";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Separator } from "@/components/ui/separator";
import { Progress } from "@/components/ui/progress";
import { DialogContent, DialogDescription, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { 
  Heart, 
  Shield, 
  Skull,
  Star,
  Wind } from "lucide-react";
import { getHealthPercentage } from "@/utils/HealthUtils";
import { formatModifier, getAttributeModifier } from "@/utils/AttributeUtils";
import { CreatureStatus, type Monster } from "@/__generated__/twisted-taleweaver-api/types";
import { getMonsterCurrentHealth, getMonsterStatus } from "@/utils/CharacterUtils";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";
import { ATTRIBUTE_KEYS } from "@/viewmodels/AttributeScoresViewModel";

export interface MonsterDialogContentProps {
  monster: Monster,
  expeditionState: ExpeditionViewModel["state"];
}

export const MonsterDialogContent = ({ monster, expeditionState }: MonsterDialogContentProps) => {
  const status = getMonsterStatus(monster, expeditionState);
  const currentHealth = getMonsterCurrentHealth(monster, expeditionState);

  return (
    <DialogContent className="bg-card border-border max-w-2xl max-h-[90vh] overflow-hidden">
      <DialogHeader>
        <DialogTitle className="font-cinzel text-destructive flex items-center gap-2">
          <Skull className="h-5 w-5" />
          {monster.name}
        </DialogTitle>
        <DialogDescription className="text-sm text-muted-foreground">
          Monster details and abilities
        </DialogDescription>
      </DialogHeader>
      
      <ScrollArea className="max-h-[75vh] pr-4">
        <div className="space-y-4">
          {/* Monster Info */}
          <div className="grid grid-cols-2 gap-4 text-sm">
            <div>
              <span className="text-muted-foreground">Type:</span>
              <span className="ml-2 font-medium capitalize">{monster.creatureType}</span>
            </div>
            <div>
              <span className="text-muted-foreground">Challenge Rating:</span>
              <span className="ml-2 font-medium">{monster.challengeRating}</span>
            </div>
            <div>
              <span className="text-muted-foreground">Status:</span>
              <Badge variant="outline" className={`ml-2 ${status === CreatureStatus.Alive ? 'text-health' : status === CreatureStatus.Injured ? 'text-warning' : 'text-destructive'}`}>
                {status}
              </Badge>
            </div>
          </div>

          <Separator />

          {/* Health & AC */}
          <div className="space-y-3">
            <div>
              <div className="flex justify-between text-sm mb-1">
                <span className="flex items-center gap-1 font-crimson">
                  <Heart className="h-4 w-4" />
                  Health
                </span>
                <span>{currentHealth}/{monster.maxHealth}</span>
              </div>
              <Progress 
                value={getHealthPercentage(currentHealth, monster.maxHealth)}
                className="h-3"
              />
            </div>

            <div className="flex justify-between text-sm">
              <span className="flex items-center gap-1 font-crimson">
                <Shield className="h-4 w-4" />
                Armor Class
              </span>
              <span className="font-medium">{monster.armorClass}</span>
            </div>
          </div>

          <Separator />

          {/* Movement */}
          <div>
            <h3 className="font-cinzel font-medium mb-2 text-sm flex items-center gap-2">
              <Wind className="h-4 w-4" />
              Movement
            </h3>
            <div className="grid grid-cols-2 gap-3 text-sm">
              <div className="flex justify-between">
                <span className="text-muted-foreground">Walking</span>
                <span className="font-medium">{monster.speedScores.walking} ft</span>
              </div>
              {monster.speedScores.flying && (
                <div className="flex justify-between">
                  <span className="text-muted-foreground">Flying</span>
                  <span className="font-medium">{monster.speedScores.flying} ft</span>
                </div>
              )}
              {monster.speedScores.swimming && (
                <div className="flex justify-between">
                  <span className="text-muted-foreground">Swimming</span>
                  <span className="font-medium">{monster.speedScores.swimming} ft</span>
                </div>
              )}
              {monster.speedScores.climbing && (
                <div className="flex justify-between">
                  <span className="text-muted-foreground">Climbing</span>
                  <span className="font-medium">{monster.speedScores.climbing} ft</span>
                </div>
              )}
            </div>
          </div>

          <Separator />


          {/* Attributes */}
          <div>
            <h3 className="font-cinzel font-medium text-sm mb-2">Attributes</h3>
            <div className="grid grid-cols-2 gap-x-4 gap-y-1">
              {monster &&
                ATTRIBUTE_KEYS.map((attr) => {
                  const value = monster!.attributeScores[attr];
  
                  return (
                    <div key={attr} className="flex justify-between items-center text-xs">
                      <span className="font-crimson capitalize">{attr}</span>
  
                      <div className="flex items-center gap-1">
                        <span className="font-medium">{value}</span>
  
                        <span className="text-xs text-muted-foreground">
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
                Traits ({monster.traits.length})
              </summary>
              <div className="mt-2 space-y-2 pl-6">
                {monster.traits.map((trait, idx) => (
                  <div key={idx} className="p-2 bg-muted/30 rounded text-xs">
                    <div className="font-medium text-sm">{trait.name}</div>
                    <div className="text-muted-foreground">{trait.effect}</div>
                  </div>
                ))}
              </div>
            </details>
          </div>
        </div>
      </ScrollArea>
    </DialogContent>
  );
};