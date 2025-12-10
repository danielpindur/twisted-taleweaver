import type { Action } from "./Action";
import type { Proficiency } from "./Proficiency";
import type { Trait } from "./Trait";

export interface Character {
  id: string;
  name: string;
  health: number;
  maxHealth: number;
  armor: number;
  status: "alive" | "injured" | "unconscious" | "dead";
  isCurrentTurn: boolean;
  attributes: {
    strength: number;
    dexterity: number;
    constitution: number;
    intelligence: number;
    wisdom: number;
    charisma: number;
  };
  level: number;
  class: string;
  background?: string;
  creatureType: "humanoid" | "undead" | "fiend" | "aberration" | "beast" | "fey" | "elemental";
  gender: "male" | "female" | "non-binary" | "other";
  traits: Trait[];
  proficiencies: Proficiency[];
  speedScores: {
    walking: number;
    flying?: number;
    swimming?: number;
    climbing?: number;
  };
  combatActions: Action[];
  nonCombatActions: Action[];
};