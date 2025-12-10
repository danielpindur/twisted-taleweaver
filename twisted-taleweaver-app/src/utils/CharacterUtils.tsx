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
import type { Character } from "@/interfaces/Character";

export const getGenderSymbol = (gender: Character["gender"]) => {
  switch (gender) {
    case "male": return "♂";
    case "female": return "♀";
    case "non-binary": return "⚥";
    case "other": return "○";
    default: return "○";
    }
};

export const getStatusColor = (status: Character["status"]) => {
  switch (status) {
    case "alive": return "text-health";
    case "injured": return "text-warning";
    case "unconscious": return "text-accent";
    case "dead": return "text-destructive";
  }
};

export const getClassIcon = (className: Character["class"]) => {
  const classIconMap: Record<string, any> = {
    'Barbarian': Axe,
    'Bard': Music,
    'Cleric': Cross,
    'Druid': Leaf,
    'Fighter': Swords,
    'Monk': Hand,
    'Paladin': Shield,
    'Ranger': Crosshair,
    'Rogue': Eye,
    'Sorcerer': Zap,
    'Warlock': Flame,
    'Wizard': Wand2,
    'Artificer': Wrench,
    'Alchemist': FlaskConical,
    'Beastmaster': Dog,
    'Bloodmage': Droplets,
    'Chronomancer': Timer,
    'Elementalist': Waves,
    'Hexblade': Sword,
    'Necromancer': Skull,
    'Psion': Brain,
    'Shadowdancer': Moon,
    'Spellblade': Sparkles,
    'Summoner': Users2,
    'Warden': TreePine
  };
  
  return classIconMap[className] || Swords; // Fallback to Swords if class not found
};