import { ExpeditionDifficulty } from "@/generated/twisted-taleweaver-api/models/ExpeditionDifficulty";

export const getDifficultyColor = (difficulty: ExpeditionDifficulty) => {
  switch (difficulty) {
    case ExpeditionDifficulty.EASY: return "bg-health text-background";
    case ExpeditionDifficulty.MEDIUM: return "bg-warning text-background";
    case ExpeditionDifficulty.HARD: return "bg-accent text-accent-foreground";
    case ExpeditionDifficulty.NIGHTMARE: return "bg-destructive text-destructive-foreground";
  }
};