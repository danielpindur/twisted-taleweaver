import { ExpeditionDifficulty, ExpeditionOutcome, ExpeditionStatus, type GetArchivedExpeditionByIdQuery, type GetArchivedExpeditionsQuery } from "@/__generated__/twisted-taleweaver-api/types";

export const getDifficultyColor = (difficulty: ExpeditionDifficulty) => {
  switch (difficulty) {
    case ExpeditionDifficulty.Easy: return "bg-health text-background";
    case ExpeditionDifficulty.Medium: return "bg-warning text-background";
    case ExpeditionDifficulty.Hard: return "bg-accent text-accent-foreground";
    case ExpeditionDifficulty.Nightmare: return "bg-destructive text-destructive-foreground";
  }
};

export const getStatusColor = (status: ExpeditionStatus) => {
	switch (status) {
		case ExpeditionStatus.Created: return "bg-warning text-background";
		case ExpeditionStatus.Started: return "bg-player text-background";
		case ExpeditionStatus.Completed: return "bg-secondary text-secondary-foreground";
	}
};

export const getOutcomeColor = (outcome: ExpeditionOutcome) => {
  switch (outcome) {
    case ExpeditionOutcome.Success: return "bg-health text-background";
    case ExpeditionOutcome.Failure: return "bg-destructive text-destructive-foreground";
  }
};

export const getOutcomeIcon = (outcome: ExpeditionOutcome) => {
  switch (outcome) {
    case ExpeditionOutcome.Success: return "⚔️";
    case ExpeditionOutcome.Failure: return "💀";
  }
};
