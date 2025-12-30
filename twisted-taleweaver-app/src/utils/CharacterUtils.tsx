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
import type { LucideIcon } from "lucide-react";
import type { UserResource } from "@clerk/types";
import { CharacterClass, CreatureGender } from "@/__generated__/twisted-taleweaver-api/types";
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

/* export const getStatusColor = (status: CreatureStatus) => {
  switch (status) {
    case CreatureStatus.ALIVE: return "text-health";
    case CreatureStatus.INJURED: return "text-warning";
    case CreatureStatus.UNCONSCIOUS: return "text-accent";
    case CreatureStatus.DEAD: return "text-destructive";
    case CreatureStatus.ENRAGED: return "text-red-600";
    case CreatureStatus.TERRIFIED: return "text-purple-500";
    case CreatureStatus.EMPOWERED: return "text-blue-500";
    case CreatureStatus.WEAKENED: return "text-gray-500";
    case CreatureStatus.STUNNED: return "text-yellow-500";
    case CreatureStatus.CURSED: return "text-violet-600";
    default: return "text-foreground";
  }
}; */

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

/* export const getCreatureStatus = (character: Character, expeditionState: ExpeditionViewModel["state"]) => {
  const participantState = expeditionState.participants.find(p => p.creatureId === character.characterId)!;

  return participantState.status;
}; */

/* export const getCreatureCurrentHealth = (character: Character, expeditionState: ExpeditionState) => {
  const participantState = expeditionState.participants.find(p => p.creatureId === character.characterId)!;

  return participantState.currentHealth;
};
 */