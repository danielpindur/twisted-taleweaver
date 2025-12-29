import { ExpeditionDifficulty } from "@/__generated__/twisted-taleweaver-api/types";

export const getDifficultyColor = (difficulty: ExpeditionDifficulty) => {
  switch (difficulty) {
    case ExpeditionDifficulty.Easy: return "bg-health text-background";
    case ExpeditionDifficulty.Medium: return "bg-warning text-background";
    case ExpeditionDifficulty.Hard: return "bg-accent text-accent-foreground";
    case ExpeditionDifficulty.Nightmare: return "bg-destructive text-destructive-foreground";
  }
};