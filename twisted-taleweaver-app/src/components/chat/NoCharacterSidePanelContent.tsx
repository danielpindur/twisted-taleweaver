import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Separator } from "@/components/ui/separator";
import { 
  User as UserIcon,
  UserCircle,
  Loader2 } from "lucide-react";
import type { ExpeditionParticipant } from "@/generated/twisted-taleweaver-api";

export interface NoCharacterSidePanelContentProps {
  participant: ExpeditionParticipant
};

export const NoCharacterSidePanelContent = ({ participant }: NoCharacterSidePanelContentProps) => {
  return (
      <Card className="bg-muted/50 border border-dashed border-border">
        <CardContent className="space-y-4 pt-6">
          <div className="text-center space-y-3">
            <div className="flex justify-center">
              <div className="w-16 h-16 rounded-full bg-muted flex items-center justify-center">
                <UserCircle className="h-10 w-10 text-muted-foreground" />
              </div>
            </div>
            
            <div className="space-y-1">
              <div className="flex items-center justify-center gap-2 text-muted-foreground">
                <UserIcon className="h-4 w-4" />
                <span className="font-crimson">{participant.user.name}</span>
              </div>
                                  
              <div className="text-sm text-muted-foreground italic">
                No character name yet
              </div>
              
              <div className="text-sm text-muted-foreground italic">
                No class selected
              </div>
            </div>
          </div>

          <Separator />

          <div className="flex items-center justify-center gap-2">
            <Badge variant="outline" className="text-amber-500 border-amber-500/50">
              <Loader2 className="h-3 w-3 mr-1 animate-spin" />
              Creating Character
            </Badge>
          </div>

          <p className="text-xs text-center text-muted-foreground">
            Complete character creation to view full stats and abilities
          </p>
        </CardContent>
      </Card>
  );
};