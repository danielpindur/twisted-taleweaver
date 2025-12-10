import type { Action } from "./Action";
import type { Trait } from "./Trait";

export interface Monster {
  id: string;
  name: string;
  health: number;
  maxHealth: number;
  armor: number;
  status: "alive" | "injured" | "dead";
  creatureType: "undead" | "fiend" | "aberration" | "beast" | "fey" | "elemental" | "humanoid" | "dragon" | "celestial";
  size: "tiny" | "small" | "medium" | "large" | "huge" | "gargantuan";
  challengeRating: string;
  isBoss?: boolean;
  attributes: {
    strength: number;
    dexterity: number;
    constitution: number;
    intelligence: number;
    wisdom: number;
    charisma: number;
  };
  speedScores: {
    walking: number;
    flying?: number;
    swimming?: number;
    climbing?: number;
  };
  traits: Trait[];
  actions: Action[];
  legendaryActions?: Action[];
};