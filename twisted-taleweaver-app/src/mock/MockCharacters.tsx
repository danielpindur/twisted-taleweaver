import type { Character } from "@/interfaces/Character";

export const mockCharacters: Character[] = [
  {
    id: "1",
    name: "test",
    health: 85,
    maxHealth: 100,
    armor: 12,
    status: "alive",
    isCurrentTurn: true,
    level: 5,
    class: "Fighter",
    background: "Noble",
    creatureType: "humanoid",
    gender: "male",
    attributes: {
      strength: 18,
      dexterity: 14,
      constitution: 16,
      intelligence: 12,
      wisdom: 13,
      charisma: 15
    },
    traits: [
      { name: "Second Wind", effect: "Regain 1d10+5 hit points as a bonus action", conditionForPerform: "Once per short rest" },
      { name: "Fighting Style", effect: "+2 to damage rolls with melee weapons" },
      { name: "Action Surge", effect: "Take one additional action on your turn", conditionForPerform: "Once per short rest" }
    ],
    proficiencies: [
      { type: 'Weapon', target: 'All Simple Weapons' },
      { type: 'Weapon', target: 'All Martial Weapons' },
      { type: 'Armor', target: 'All Armor' },
      { type: 'Skill', target: 'Intimidation' },
      { type: 'Skill', target: 'Athletics' },
      { type: 'SavingThrow', target: 'Strength' },
      { type: 'SavingThrow', target: 'Constitution' }
    ],
    speedScores: {
      walking: 30,
      climbing: 15
    },
    combatActions: [
      { name: "Ice Sword Slash", description: "Frost-enchanted melee attack +7 to hit, 2d6+4 cold damage", cost: 1, range: 5 },
      { name: "Fire Shield Bash", description: "Flaming shield push target 5 feet and deal 1d4 fire damage", cost: 1, range: 5 },
      { name: "Lightning Strike", description: "Electrified attack that chains to nearby enemies", cost: 2, recharge: 5 },
      { name: "Shadow Whirlwind", description: "Dark energy attack hitting all enemies within reach", cost: 2, recharge: 4 }
    ],
    nonCombatActions: [
      { name: "Inspire Allies", description: "Grant advantage on next ability check", cost: 1 },
      { name: "Assess Situation", description: "Gain insight into tactical options", cost: 1 },
      { name: "Divine Light", description: "Create holy illumination in darkness", cost: 1 }
    ]
  },
  {
    id: "2", 
    name: "Lyra Shadowbane",
    health: 45,
    maxHealth: 80,
    armor: 8,
    status: "injured",
    isCurrentTurn: false,
    level: 4,
    class: "Rogue",
    background: "Criminal",
    creatureType: "humanoid",
    gender: "female",
    attributes: {
      strength: 10,
      dexterity: 18,
      constitution: 14,
      intelligence: 16,
      wisdom: 12,
      charisma: 13
    },
    traits: [
      { name: "Sneak Attack", effect: "Deal extra 2d6 damage when you have advantage" },
      { name: "Thieves' Cant", effect: "Communicate secretly with other rogues" },
      { name: "Cunning Action", effect: "Dash, Disengage, or Hide as a bonus action" }
    ],
    proficiencies: [
      { type: 'Weapon', target: 'Simple Weapons' },
      { type: 'Weapon', target: 'Rapiers' },
      { type: 'Weapon', target: 'Shortswords' },
      { type: 'Skill', target: 'Stealth' },
      { type: 'Skill', target: 'Sleight of Hand' },
      { type: 'Skill', target: 'Investigation' },
      { type: 'Tool', target: "Thieves' Tools" }
    ],
    speedScores: {
      walking: 30,
      climbing: 30
    },
    combatActions: [
      { name: "Rapier Strike", description: "Finesse weapon attack +6 to hit, 1d8+4 damage", cost: 1, range: 5 },
      { name: "Sneak Attack", description: "Extra 2d6 damage with advantage", cost: 1 },
      { name: "Dash", description: "Move up to your speed again", cost: 1 }
    ],
    nonCombatActions: [
      { name: "Pick Lock", description: "Attempt to unlock a door or chest", cost: 2 },
      { name: "Search", description: "Look for hidden objects or clues", cost: 1 },
      { name: "Hide", description: "Attempt to become unseen", cost: 1 }
    ]
  },
  {
    id: "3",
    name: "Gareth Ironshield", 
    health: 0,
    maxHealth: 120,
    armor: 15,
    status: "unconscious",
    isCurrentTurn: false,
    level: 6,
    class: "Paladin",
    background: "Soldier",
    creatureType: "undead",
    gender: "male",
    attributes: {
      strength: 16,
      dexterity: 10,
      constitution: 18,
      intelligence: 11,
      wisdom: 14,
      charisma: 17
    },
    traits: [
      { name: "Divine Sense", effect: "Detect celestials, fiends, and undead within 60 feet", conditionForPerform: "3 times per long rest" },
      { name: "Lay on Hands", effect: "Heal 30 hit points per day" },
      { name: "Undead Fortitude", effect: "When reduced to 0 HP, make a Constitution save to drop to 1 HP instead" }
    ],
    proficiencies: [
      { type: 'Weapon', target: 'All Simple Weapons' },
      { type: 'Weapon', target: 'All Martial Weapons' },
      { type: 'Armor', target: 'All Armor' },
      { type: 'Skill', target: 'Religion' },
      { type: 'Skill', target: 'Medicine' },
      { type: 'SavingThrow', target: 'Wisdom' },
      { type: 'SavingThrow', target: 'Charisma' }
    ],
    speedScores: {
      walking: 25,
      flying: 30
    },
    combatActions: [
      { name: "Warhammer Strike", description: "Melee weapon attack +6 to hit, 1d8+3 damage", cost: 1, range: 5 },
      { name: "Divine Smite", description: "Expend spell slot for extra radiant damage", cost: 1 },
      { name: "Protection", description: "Impose disadvantage on attack against ally", cost: 1 }
    ],
    nonCombatActions: [
      { name: "Detect Evil", description: "Sense hostile supernatural presence", cost: 1 },
      { name: "Bless", description: "Grant bonus to ally's next roll", cost: 1 }
    ]
  }
];