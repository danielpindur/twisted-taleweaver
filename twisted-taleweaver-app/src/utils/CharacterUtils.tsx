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
import { CreatureStatus } from "@/generated/twisted-taleweaver-api/models/CreatureStatus";
import { ClassType } from "@/generated/twisted-taleweaver-api/models/ClassType";
import { Gender } from "@/generated/twisted-taleweaver-api/models/Gender";
import type { Character, Expedition, ExpeditionParticipant, ExpeditionState } from "@/generated/twisted-taleweaver-api";
import type { UserResource } from "@clerk/types";

export const getGenderSymbol = (gender: Gender) => {
  switch (gender) {
    case Gender.MALE: return "♂";
    case Gender.FEMALE: return "♀";
    case Gender.NON_BINARY: return "⚥";
    case Gender.OTHER: return "○";
    default: return "○";
    }
};

export const getStatusColor = (status: CreatureStatus) => {
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
};

export const getClassIcon = (className: ClassType) => {
  const classIconMap: Record<ClassType, LucideIcon> = {
    [ClassType.BARBARIAN]: Axe,
    [ClassType.BARD]: Music,
    [ClassType.CLERIC]: Cross,
    [ClassType.DRUID]: Leaf,
    [ClassType.FIGHTER]: Swords,
    [ClassType.MONK]: Hand,
    [ClassType.PALADIN]: Shield,
    [ClassType.RANGER]: Crosshair,
    [ClassType.ROGUE]: Eye,
    [ClassType.SORCERER]: Zap,
    [ClassType.WARLOCK]: Flame,
    [ClassType.WIZARD]: Wand2,
    [ClassType.ARTIFICER]: Wrench,
    [ClassType.ALCHEMIST]: FlaskConical,
    [ClassType.BEASTMASTER]: Dog,
    [ClassType.BLOODMAGE]: Droplets,
    [ClassType.CHRONOMANCER]: Timer,
    [ClassType.ELEMENTALIST]: Waves,
    [ClassType.HEXBLADE]: Sword,
    [ClassType.NECROMANCER]: Skull,
    [ClassType.PSION]: Brain,
    [ClassType.SHADOWDANCER]: Moon,
    [ClassType.SPELLBLADE]: Sparkles,
    [ClassType.SUMMONER]: Users2,
    [ClassType.WARDEN]: TreePine,
  };
  
  return classIconMap[className] ?? Swords;
};

/**
 * Gets the user's participant
 */
export const getUserParticipant = (participants: ExpeditionParticipant[], user: UserResource): ExpeditionParticipant | undefined => {
  return participants.find(p => p.user.externalUserId === user.id);
};

export const getCreatureStatus = (character: Character, expeditionState: ExpeditionState) => {
  const participantState = expeditionState.participants.find(p => p.creatureId === character.characterId)!;

  return participantState.status;
};

export const getCreatureCurrentHealth = (character: Character, expeditionState: ExpeditionState) => {
  const participantState = expeditionState.participants.find(p => p.creatureId === character.characterId)!;

  return participantState.currentHealth;
};
