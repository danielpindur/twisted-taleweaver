import type { Monster } from "@/interfaces/Monster";

export const mockMonsters: Monster[] = [
  {
    id: "m1",
    name: "Skeletal Warrior",
    health: 45,
    maxHealth: 45,
    armor: 13,
    status: "alive",
    creatureType: "undead",
    size: "medium",
    challengeRating: "1/2",
    attributes: {
      strength: 15,
      dexterity: 14,
      constitution: 15,
      intelligence: 6,
      wisdom: 8,
      charisma: 5
    },
    speedScores: {
      walking: 30
    },
    traits: [
      { name: "Damage Vulnerabilities", effect: "Bludgeoning" },
      { name: "Damage Immunities", effect: "Poison" },
      { name: "Condition Immunities", effect: "Exhaustion, poisoned" }
    ],
    actions: [
      { name: "Scimitar", description: "Melee Weapon Attack: +4 to hit, reach 5 ft., one target. Hit: 5 (1d6 + 2) slashing damage.", cost: 1, range: 5 },
      { name: "Shortbow", description: "Ranged Weapon Attack: +4 to hit, range 80/320 ft., one target. Hit: 5 (1d6 + 2) piercing damage.", cost: 1, range: 80 }
    ]
  },
  {
    id: "m2", 
    name: "Shadow Wraith",
    health: 20,
    maxHealth: 67,
    armor: 12,
    status: "injured",
    creatureType: "undead",
    size: "medium",
    challengeRating: "5",
    attributes: {
      strength: 6,
      dexterity: 16,
      constitution: 16,
      intelligence: 12,
      wisdom: 14,
      charisma: 15
    },
    speedScores: {
      walking: 0,
      flying: 60
    },
    traits: [
      { name: "Incorporeal Movement", effect: "Can move through other creatures and objects as if they were difficult terrain. Takes 5 force damage if it ends its turn inside an object." },
      { name: "Sunlight Sensitivity", effect: "While in sunlight, has disadvantage on attack rolls and Wisdom (Perception) checks that rely on sight." }
    ],
    actions: [
      { name: "Life Drain", description: "Melee Weapon Attack: +6 to hit, reach 5 ft., one humanoid. Hit: 21 (4d8 + 3) necrotic damage. Target must make a DC 14 Constitution saving throw or its hit point maximum is reduced by an amount equal to the damage taken.", cost: 1, range: 5 },
      { name: "Create Specter", description: "The wraith targets a humanoid within 10 feet of it that has been dead for no longer than 1 minute and died violently. The target's spirit rises as a specter under the wraith's control.", cost: 1, range: 10 }
    ]
  },
  {
    id: "m3",
    name: "Lich Lord Valeth",
    health: 135,
    maxHealth: 135,
    armor: 17,
    status: "alive",
    creatureType: "undead",
    size: "medium",
    challengeRating: "21",
    isBoss: true,
    attributes: {
      strength: 11,
      dexterity: 16,
      constitution: 16,
      intelligence: 20,
      wisdom: 14,
      charisma: 16
    },
    speedScores: {
      walking: 30,
      flying: 60
    },
    traits: [
      { name: "Legendary Resistance", effect: "If the lich fails a saving throw, it can choose to succeed instead (3/Day)." },
      { name: "Turn Resistance", effect: "The lich has advantage on saving throws against any effect that turns undead." },
      { name: "Rejuvenation", effect: "If it has a phylactery, a destroyed lich gains a new body in 1d10 days, regaining all its hit points." }
    ],
    actions: [
      { name: "Paralyzing Touch", description: "Melee Spell Attack: +12 to hit, reach 5 ft., one creature. Hit: 10 (3d6) cold damage. The target must succeed on a DC 18 Constitution saving throw or be paralyzed for 1 minute.", cost: 1, range: 5 },
      { name: "Frightening Gaze", description: "The lich fixes its gaze on one creature it can see within 10 feet of it. The target must succeed on a DC 18 Wisdom saving throw or become frightened for 1 minute.", cost: 1, range: 10 },
      { name: "Disintegrate", description: "The lich casts disintegrate. DC 19 Dexterity saving throw. 75 (10d6 + 40) force damage on failure, half on success.", cost: 1, range: 60 }
    ],
    legendaryActions: [
      { name: "Cantrip", description: "The lich casts a cantrip.", cost: 1 },
      { name: "Paralyzing Touch", description: "The lich uses its Paralyzing Touch.", cost: 2 },
      { name: "Frightening Gaze", description: "The lich uses its Frightening Gaze (Costs 2 Actions).", cost: 2 },
      { name: "Disintegrate", description: "The lich casts disintegrate (Costs 3 Actions).", cost: 3 }
    ]
  }
];