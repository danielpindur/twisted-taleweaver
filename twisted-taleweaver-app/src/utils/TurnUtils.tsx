import type { ExpeditionParticipant, User } from "@/generated/twisted-taleweaver-api";

/**
 * Checks if the current user can act (if it's their character's turn)
 */
export const canParticipantAct = (participant: ExpeditionParticipant, activeUser?: (User | null)): boolean => {
  return activeUser !== null && participant?.user.userId === activeUser?.userId;
};

/**
 * Gets the participant whose turn it is
 */
export const getCurrentTurnParticipant = (participants: ExpeditionParticipant[], activeUser?: (User | null)): (ExpeditionParticipant | undefined) => {
  if (activeUser === null) {
    return undefined;
  }

  return participants.find(p => p.user.userId === activeUser?.userId);
};
