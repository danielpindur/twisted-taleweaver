//import { ExpeditionStatus } from "@/__generated__/twisted-taleweaver-api/types";

export const getStatusColor = (status: ExpeditionStatus) => {
	switch (status) {
		case ExpeditionStatus.CREATED: return "bg-warning text-background";
		case ExpeditionStatus.STARTED: return "bg-player text-background";
		case ExpeditionStatus.COMPLETED: return "bg-secondary text-secondary-foreground";
	}
};