import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";

/**
 * Checks if the current user can act (if it's their character's turn)
 */
export const canParticipantAct = (participant: ExpeditionViewModel["participants"][number], activeUser: ExpeditionViewModel["state"]["activeUser"]): boolean => {
  return activeUser !== null && participant?.user.id === activeUser?.id;
};

/**
 * Gets the participant whose turn it is
 */
export const getCurrentTurnParticipant = (participants: ExpeditionViewModel["participants"], activeUser: ExpeditionViewModel["state"]["activeUser"]): (ExpeditionViewModel["participants"][number] | undefined) => {
  if (activeUser === null) {
    return undefined;
  }

  return participants.find(p => p.user.id === activeUser?.id);
};
