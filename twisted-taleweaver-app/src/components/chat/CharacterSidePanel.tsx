import { useState, useEffect, useRef } from "react";
import { Button } from "@/components/ui/button";
import { User as UserIcon, ChevronLeft, ChevronRight } from "lucide-react";
import { NoCharacterSidePanelContent } from "./NoCharacterSidePanelContent";
import { CharacterSidePanelContent } from "./CharacterSidePanelContent";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";

export interface CharacterSidePanelProps {
  panelCollapsed: boolean,
  setPanelCollapsed: (collapsed: boolean) => void,
  participant: ExpeditionViewModel["participants"][number],
  expeditionState: ExpeditionViewModel["state"]
};

export const CharacterSidePanel = ({ panelCollapsed, setPanelCollapsed, participant, expeditionState }: CharacterSidePanelProps) => {
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
  }, [participant, panelCollapsed]);

  return (
    <div className={`${panelCollapsed ? 'w-12' : 'w-56 md:w-72'} flex-shrink-0 bg-card border-l border-border transition-[width] duration-300 overflow-hidden`}>
      {panelCollapsed ? (
        <div className="p-2 h-full flex flex-col items-center justify-start pt-4 gap-3">
          <Button variant="ghost" size="icon" onClick={() => setPanelCollapsed(false)} className="h-8 w-8">
            <ChevronLeft className="h-4 w-4" />
          </Button>
          <UserIcon className="h-5 w-5 text-muted-foreground" />
          <div className="writing-mode-vertical text-xs text-muted-foreground font-cinzel transform rotate-180" style={{ writingMode: 'vertical-rl' }}>
            CHARACTER
          </div>
        </div>
      ) : (
        <div ref={scrollContainerRef} 
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
          { participant.character ? 
              <CharacterSidePanelContent character={participant.character} expeditionState={expeditionState} /> : 
              <NoCharacterSidePanelContent participant={participant} />
          }
        </div>
      )}
    </div>
  );
};