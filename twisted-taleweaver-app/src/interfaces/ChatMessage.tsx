export interface ChatMessage {
  id: string;
  sender: "taleweaver" | "character" | "system" | "monster";
  characterName?: string;
  monsterName?: string;
  content: string;
  timestamp: Date;
  type?: "narrative" | "action" | "dialogue" | "system";
};