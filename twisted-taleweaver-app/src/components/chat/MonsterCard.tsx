import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Progress } from "@/components/ui/progress";
import { 
  Heart, 
  Shield, 
  Skull,
  Gauge} from "lucide-react";
import { getHealthPercentage } from "@/utils/HealthUtils";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";
import { getMonsterCurrentHealth, getMonsterStatus } from "@/utils/CharacterUtils";
import { CreatureStatus } from "@/__generated__/twisted-taleweaver-api/types";

export interface MonsterCardProps {
  monster: ExpeditionViewModel["state"]["monsters"][number];
  expeditionState: ExpeditionViewModel["state"];
}

export const MonsterCard = ({ monster, expeditionState }: MonsterCardProps) => {
  const isBossMonster = monster.challengeRating >= 10;
  const currentHealth = getMonsterCurrentHealth(monster, expeditionState);
  const status = getMonsterStatus(monster, expeditionState);

  return (
    <Card className="min-w-[180px] bg-muted/50 border-destructive/30 cursor-pointer hover:bg-muted/70 transition-colors">
      <CardContent className="p-3">
        <div className="flex items-center justify-between mb-2">
          <div>
            <div className="flex items-center gap-2">
              <Skull className={`h-4 w-4 ${isBossMonster ? 'text-warning' : 'text-destructive'}`} />
              <span className="font-crimson font-medium text-sm">{monster.name}</span>
            </div>
            <div className="text-xs text-muted-foreground capitalize">
              {monster.creatureType}
            </div>
          </div>
          <Badge variant="outline" className={`${status === CreatureStatus.Alive ? 'text-health' : status === CreatureStatus.Injured ? 'text-warning' : 'text-destructive'}`}>
            {status}
          </Badge>
        </div>
        
        {/* Health Bar */}
        <div className="space-y-1 mb-2">
          <div className="flex items-center justify-between text-xs">
            <span className="flex items-center gap-1">
              <Heart className="h-3 w-3" />
              Health
            </span>
            <span>{currentHealth}/{monster.maxHealth}</span>
          </div>
          <Progress 
            value={getHealthPercentage(currentHealth, monster.maxHealth)} 
            className="h-2"
          />
        </div>

        {/* Armor */}
        <div className="flex items-center justify-between text-xs text-muted-foreground">
          <span className="flex items-center gap-1">
            <Shield className="h-3 w-3" />
            AC
          </span>
          <span>{monster.armorClass}</span>
        </div>

        {/* Challenge Rating */}
        <div className="flex items-center justify-between text-xs text-muted-foreground">
          <span className="flex items-center gap-1">
            <Gauge className="h-3 w-3" />
            CR
          </span>
          <span>{monster.challengeRating}</span>
        </div>
      </CardContent>
    </Card>
  );
};