import type { Character } from "./Character";

export interface PendingExpedition {
  id: string;
  name: string;
  description: string;
  ownerId: string;
  ownerName: string;
  status: "gathering" | "ready" | "started";
  maxPlayers: number;
  currentPlayers: number;
  difficulty: "easy" | "medium" | "hard" | "nightmare";
  createdAt: Date;
  participants: {
    characterId: string;
    characterName: string;
    joinedAt: Date;
  }[];
}

export interface ExpeditionSystemMessage {
  type: "expedition_created" | "character_joined" | "character_left" | "expedition_ready" | "expedition_started";
  expeditionName: string;
  ownerName?: string;
  characterName?: string;
  waitingCount?: number;
}