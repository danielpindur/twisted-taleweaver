import { Badge } from "@/components/ui/badge";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Separator } from "@/components/ui/separator";
import { Progress } from "@/components/ui/progress";
import { DialogContent, DialogDescription, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { 
  Crown, 
  Heart, 
  Shield, 
  Swords, 
  Skull,
  Star,
  Wind,
  Clock} from "lucide-react";
import type { Monster } from "@/interfaces/Monster";
import { getHealthPercentage } from "@/utils/HealthUtils";
import { formatModifier, getAttributeModifier } from "@/utils/AttributeUtils";

export interface MonsterDialogContentProps {
  monster: Monster;
}

export const MonsterDialogContent = ({monster}: MonsterDialogContentProps) => {

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
              <span className="text-muted-foreground">Size:</span>
              <span className="ml-2 font-medium capitalize">{monster.size}</span>
            </div>
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
              <Badge variant="outline" className={`ml-2 ${monster.status === 'alive' ? 'text-health' : monster.status === 'injured' ? 'text-warning' : 'text-destructive'}`}>
                {monster.status}
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
                <span>{monster.health}/{monster.maxHealth}</span>
              </div>
              <Progress 
                value={getHealthPercentage(monster.health, monster.maxHealth)}
                className="h-3"
              />
            </div>

            <div className="flex justify-between text-sm">
              <span className="flex items-center gap-1 font-crimson">
                <Shield className="h-4 w-4" />
                Armor Class
              </span>
              <span className="font-medium">{monster.armor}</span>
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
            <h3 className="font-cinzel font-medium mb-2 text-sm">Attributes</h3>
            <div className="grid grid-cols-2 gap-x-6 gap-y-2 text-sm">
              {Object.entries(monster.attributes).map(([attr, value]) => (
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

            <details className="group">
              <summary className="cursor-pointer font-cinzel font-medium text-sm flex items-center gap-2 p-2 hover:bg-muted/50 rounded">
                <Swords className="h-4 w-4" />
                Actions ({monster.actions.length})
              </summary>
              <div className="mt-2 space-y-2 pl-6">
                {monster.actions.map((action, idx) => (
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

            {monster.legendaryActions && monster.legendaryActions.length > 0 && (
              <details className="group">
                <summary className="cursor-pointer font-cinzel font-medium text-sm flex items-center gap-2 p-2 hover:bg-muted/50 rounded text-warning">
                  <Crown className="h-4 w-4" />
                  Legendary Actions ({monster.legendaryActions.length})
                </summary>
                <div className="mt-2 space-y-2 pl-6">
                  <div className="text-xs text-muted-foreground mb-2 p-2 bg-warning/10 rounded border border-warning/20">
                    <strong>Legendary Actions:</strong> The {monster.name.toLowerCase()} can take 3 legendary actions, choosing from the options below. Only one legendary action option can be used at a time and only at the end of another creature's turn. The {monster.name.toLowerCase()} regains spent legendary actions at the start of its turn.
                  </div>
                  {monster.legendaryActions.map((action, idx) => (
                    <div key={idx} className="p-2 bg-warning/10 rounded text-xs border border-warning/20">
                      <div className="flex justify-between items-start mb-1">
                        <span className="font-medium text-sm">{action.name}</span>
                        <Badge variant="outline" className="text-xs px-1 text-warning border-warning">
                          Cost: {action.cost}
                        </Badge>
                      </div>
                      <div className="text-muted-foreground">{action.description}</div>
                      {action.range && (
                        <div className="text-accent mt-1">Range: {action.range} ft</div>
                      )}
                    </div>
                  ))}
                </div>
              </details>
            )}
          </div>
        </div>
      </ScrollArea>
    </DialogContent>
  );
};