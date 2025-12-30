import { 
  Shield, 
  Swords, 
  Eye, 
  Skull,
  Flame,
  Brain,
  Zap,
  TreePine,
  Sparkles,
  Wrench,
  Axe,
  Music,
  Cross,
  Leaf,
  Hand,
  Crosshair,
  Wand2,
  FlaskConical,
  Dog,
  Droplets,
  Timer,
  Waves,
  Sword,
  Moon,
  Users2} from "lucide-react";
import { CharacterClass, CreatureGender, CreatureStatus } from "@/__generated__/twisted-taleweaver-api/types";
import type { LucideIcon } from "lucide-react";
import type { UserResource } from "@clerk/types";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";

export const getGenderSymbol = (gender: CreatureGender) => {
  switch (gender) {
    case CreatureGender.Male: return "♂";
    case CreatureGender.Female: return "♀";
    case CreatureGender.NonBinary: return "⚥";
    case CreatureGender.Other: return "○";
    default: return "○";
    }
};

export const getStatusColor = (status: CreatureStatus) => {
  switch (status) {
    case CreatureStatus.Alive: return "text-health";
    case CreatureStatus.Injured: return "text-warning";
    case CreatureStatus.Unconscious: return "text-accent";
    case CreatureStatus.Dead: return "text-destructive";
    case CreatureStatus.Enraged: return "text-red-600";
    case CreatureStatus.Terrified: return "text-purple-500";
    case CreatureStatus.Empowered: return "text-blue-500";
    case CreatureStatus.Weakened: return "text-gray-500";
    case CreatureStatus.Stunned: return "text-yellow-500";
    case CreatureStatus.Cursed: return "text-violet-600";
    default: return "text-foreground";
  }
};

export const getClassIcon = (className: CharacterClass) => {
  const classIconMap: Record<CharacterClass, LucideIcon> = {
    [CharacterClass.Barbarian]: Axe,
    [CharacterClass.Bard]: Music,
    [CharacterClass.Cleric]: Cross,
    [CharacterClass.Druid]: Leaf,
    [CharacterClass.Fighter]: Swords,
    [CharacterClass.Monk]: Hand,
    [CharacterClass.Paladin]: Shield,
    [CharacterClass.Ranger]: Crosshair,
    [CharacterClass.Rogue]: Eye,
    [CharacterClass.Sorcerer]: Zap,
    [CharacterClass.Warlock]: Flame,
    [CharacterClass.Wizard]: Wand2,
    [CharacterClass.Artificer]: Wrench,
    [CharacterClass.Alchemist]: FlaskConical,
    [CharacterClass.Beastmaster]: Dog,
    [CharacterClass.Bloodmage]: Droplets,
    [CharacterClass.Chronomancer]: Timer,
    [CharacterClass.Elementalist]: Waves,
    [CharacterClass.Hexblade]: Sword,
    [CharacterClass.Necromancer]: Skull,
    [CharacterClass.Psion]: Brain,
    [CharacterClass.Shadowdancer]: Moon,
    [CharacterClass.Spellblade]: Sparkles,
    [CharacterClass.Summoner]: Users2,
    [CharacterClass.Warden]: TreePine,
  };
  
  return classIconMap[className] ?? Swords;
};

/**
 * Gets the user's participant
 */
export const getUserParticipant = (participants: ExpeditionViewModel["participants"], user: UserResource): ExpeditionViewModel["participants"][number] | undefined => {
  return participants.find(p => p.user.externalId === user.id);
};

export const getCreatureStatus = (character: NonNullable<ExpeditionViewModel["participants"][number]["character"]>, expeditionState: ExpeditionViewModel["state"]) => {
  const participantState = expeditionState.characterStates.find(p => p.creatureId === character.id)!;

  return participantState.status;
};

export const getCreatureCurrentHealth = (character: NonNullable<ExpeditionViewModel["participants"][number]["character"]>, expeditionState: ExpeditionViewModel["state"]) => {
  const participantState = expeditionState.characterStates.find(p => p.creatureId === character.id)!;

  return participantState.currentHealth;
};
