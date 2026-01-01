import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";

/**
 * Checks if the current user can act (if it's their character's turn)
 */
export const canParticipantAct = (participant: ExpeditionViewModel["participants"][number], activeCreature: ExpeditionViewModel["state"]["activeCreature"]): boolean => {
  return activeCreature !== null && participant?.character?.id === activeCreature?.id;
};

/**
 * Gets the participant whose turn it is
 */
export const getCurrentTurnParticipant = (participants: ExpeditionViewModel["participants"], activeCreature: ExpeditionViewModel["state"]["activeCreature"]): (ExpeditionViewModel["participants"][number] | undefined) => {
  if (activeCreature === null) {
    return undefined;
  }

  return participants.find(p => p.character?.id === activeCreature?.id);
};
