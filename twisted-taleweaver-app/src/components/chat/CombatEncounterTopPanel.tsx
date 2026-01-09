import { useState, useRef } from "react";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Dialog, DialogTrigger } from "@/components/ui/dialog";
import { 
  Swords, 
  ChevronUp,
  ChevronDown} from "lucide-react";
import { MonsterDialogContent } from "@/components/popups/MonsterDialogContent";
import { MonsterCard } from "./MonsterCard";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";

export interface CombatEncounterTopPanelProps {
  monsters: ExpeditionViewModel["state"]["monsters"];
  expeditionState: ExpeditionViewModel["state"];
}; 

export const CombatEncounterTopPanel = ({ monsters, expeditionState }: CombatEncounterTopPanelProps) => {
  const [combatEncounterCollapsed, setCombatEncounterCollapsed] = useState(false);
  const scrollContainerRef = useRef<HTMLDivElement>(null);

  const activeMonsters = monsters.filter(m => {
    const state = expeditionState.creatureStates.find(cs => cs.creatureId === m.id);
    return state;
  });

  return (
    <div>
      <div className={`bg-destructive/10 border-b border-destructive/20 transition-[height,opacity] duration-300 overflow-hidden ${
        activeMonsters.length > 0 && !combatEncounterCollapsed 
          ? 'h-auto opacity-100' 
          : 'h-0 opacity-0'
      }`}>
        {activeMonsters.length > 0 && (
          <div className="px-3 py-3 min-w-0">
            <div className="flex items-center justify-between mb-3">
              <h3 className="font-cinzel font-semibold text-sm text-destructive flex items-center gap-2">
                <Swords className="h-4 w-4" />
                Combat Encounter
              </h3>
              <div className="flex items-center gap-2">
                <Badge variant="destructive" className="text-xs">
                  {activeMonsters.length} {activeMonsters.length === 1 ? 'Enemy' : 'Enemies'}
                </Badge>
                <Button variant="ghost" size="icon" onClick={() => setCombatEncounterCollapsed(true)} className="h-6 w-6">
                  <ChevronUp className="h-3 w-3" />
                </Button>
              </div>
            </div>
            
            <div 
              ref={scrollContainerRef}
              className="flex gap-3 overflow-x-auto max-w-full scrollbar-gutter-stable"
            >
              <div className="flex gap-3 min-w-max">
                {activeMonsters.map((monster) => (
                  <Dialog key={monster.id}>
                    <DialogTrigger asChild>
                      <MonsterCard monster={monster} expeditionState={expeditionState} />
                    </DialogTrigger>
                    <MonsterDialogContent monster={monster} expeditionState={expeditionState} />
                  </Dialog>
                ))}
              </div>
            </div>
          </div>
        )}
      </div>
      
      {/* Collapsed Combat Encounter Toggle */}
      <div className={`bg-destructive/10 border-b border-destructive/20 transition-[height,opacity] duration-300 overflow-hidden ${
        activeMonsters.length > 0 && combatEncounterCollapsed 
          ? 'h-auto opacity-100' 
          : 'h-0 opacity-0'
      }`}>
        {activeMonsters.length > 0 && combatEncounterCollapsed && (
          <div className="p-2">
            <div className="flex items-center justify-between">
              <div className="flex items-center gap-2">
                <Swords className="h-4 w-4 text-destructive" />
                <span className="font-cinzel font-semibold text-sm text-destructive">Combat Encounter</span>
                <Badge variant="destructive" className="text-xs">
                  {activeMonsters.length}
                </Badge>
              </div>
              <Button variant="ghost" size="icon" onClick={() => setCombatEncounterCollapsed(false)} className="h-6 w-6">
                <ChevronDown className="h-3 w-3" />
              </Button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}