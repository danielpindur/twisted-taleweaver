import type { Character } from "@/interfaces/Character";

/**
 * Checks if the current user can act (if it's their character's turn)
 */
export const canUserAct = (characters: Character[], username: string): boolean => {
  const activeCharacter = characters.find(char => char.isCurrentTurn);
  return activeCharacter?.name === username;
};

/**
 * Gets the character whose turn it is
 */
export const getCurrentTurnCharacter = (characters: Character[]): Character | undefined => {
  return characters.find(char => char.isCurrentTurn);
};

/**
 * Gets the user's character by username
 */
export const getUserCharacter = (characters: Character[], username: string): Character | undefined => {
  return characters.find(char => char.name === username);
};