import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Progress } from "@/components/ui/progress";
import { 
  Heart, 
  Shield, 
  Skull,
  Gauge} from "lucide-react";
import type { Monster } from "@/interfaces/Monster";
import { getHealthPercentage } from "@/utils/HealthUtils";

export interface MonsterCardProps {
  monster: Monster;
}

export const MonsterCard = ({ monster }: MonsterCardProps) => {
  return (
    <Card className="min-w-[180px] bg-muted/50 border-destructive/30 cursor-pointer hover:bg-muted/70 transition-colors">
      <CardContent className="p-3">
        <div className="flex items-center justify-between mb-2">
          <div>
            <div className="flex items-center gap-2">
              <Skull className={`h-4 w-4 ${monster.isBoss ? 'text-warning' : 'text-destructive'}`} />
              <span className="font-crimson font-medium text-sm">{monster.name}</span>
            </div>
            <div className="text-xs text-muted-foreground capitalize">
              {monster.size} {monster.creatureType}
            </div>
          </div>
          <Badge variant="outline" className={`${monster.status === 'alive' ? 'text-health' : monster.status === 'injured' ? 'text-warning' : 'text-destructive'}`}>
            {monster.status}
          </Badge>
        </div>
        
        {/* Health Bar */}
        <div className="space-y-1 mb-2">
          <div className="flex items-center justify-between text-xs">
            <span className="flex items-center gap-1">
              <Heart className="h-3 w-3" />
              Health
            </span>
            <span>{monster.health}/{monster.maxHealth}</span>
          </div>
          <Progress 
            value={getHealthPercentage(monster.health, monster.maxHealth)} 
            className="h-2"
          />
        </div>

        {/* Armor */}
        <div className="flex items-center justify-between text-xs text-muted-foreground">
          <span className="flex items-center gap-1">
            <Shield className="h-3 w-3" />
            AC
          </span>
          <span>{monster.armor}</span>
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