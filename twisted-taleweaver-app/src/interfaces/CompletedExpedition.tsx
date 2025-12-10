export interface CompletedExpedition {
  id: string;
  name: string;
  description: string;
  participants: number;
  maxPlayers: number;
  difficulty: "easy" | "medium" | "hard" | "nightmare";
  completedDate: string;
  duration: string;
  outcome: "victory" | "defeat";
  survivors: number;
  
  // Detailed information for dialog
  participantDetails: {
    name: string;
    class: string;
    outcome: "survived" | "injured" | "perished";
  }[];
  
  enemiesEncountered: {
    name: string;
    defeated: boolean;
    description: string;
  }[];
  
  keyEvents: {
    time: string;
    event: string;
    significance: "critical" | "major" | "minor";
  }[];
  
  finalBattle?: {
    enemy: string;
    description: string;
    result: string;
  };
  
  lessonLearned: string;
}