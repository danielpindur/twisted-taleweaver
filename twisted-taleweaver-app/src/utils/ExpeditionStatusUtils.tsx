import { ExpeditionStatus } from "@/__generated__/twisted-taleweaver-api/types";

export const getStatusColor = (status: ExpeditionStatus) => {
	switch (status) {
		case ExpeditionStatus.Created: return "bg-warning text-background";
		case ExpeditionStatus.Started: return "bg-player text-background";
		case ExpeditionStatus.Completed: return "bg-secondary text-secondary-foreground";
	}
};