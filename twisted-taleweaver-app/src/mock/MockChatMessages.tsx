import type { ChatMessage } from "@/interfaces/ChatMessage";

export const mockMessages: ChatMessage[] = [
  {
    id: "1",
    sender: "taleweaver",
    content: "The ancient door creaks open with the sound of grinding bone, revealing a chamber steeped in shadow and malice. Three paths stretch before you - each promising a unique flavor of doom. How delightfully predictable...",
    timestamp: new Date(Date.now() - 300000),
    type: "narrative"
  },
  {
    id: "2",
    sender: "character",
    characterName: "Theron the Bold",
    content: "I examine the left path carefully, looking for traps.",
    timestamp: new Date(Date.now() - 240000),
    type: "action"
  },
  {
    id: "3",
    sender: "taleweaver", 
    content: "Ah, caution from our would-be hero! How... refreshing. You peer into the left passage and notice the stones are discolored with what appears to be centuries of dried blood. The air whispers with the echoes of those who thought themselves clever. Roll for perception, dear Theron.",
    timestamp: new Date(Date.now() - 180000),
    type: "narrative"
  },
  {
    id: "4",
    sender: "system",
    content: "Theron the Bold rolled 16 for Perception (Success!)",
    timestamp: new Date(Date.now() - 120000),
    type: "system"
  },
  {
    id: "5",
    sender: "taleweaver",
    content: "Your keen eyes spot pressure plates hidden beneath a thin layer of dust. How marvelous! You've found the trap... but knowing it exists and surviving it are two entirely different tales, aren't they? What will you do now, oh 'bold' one?",
    timestamp: new Date(Date.now() - 60000),
    type: "narrative"
  },
  {
    id: "6",
    sender: "monster",
    monsterName: "Skeletal Warrior",
    content: "The skeletal warrior raises its rusty scimitar and strikes at Theron with supernatural fury!",
    timestamp: new Date(Date.now() - 30000),
    type: "action"
  },
  {
    id: "7",
    sender: "system",
    content: "Skeletal Warrior rolled 18 for attack (Hit!) - Deals 7 slashing damage to Theron",
    timestamp: new Date(Date.now() - 25000),
    type: "system"
  },
  {
    id: "8",
    sender: "character",
    characterName: "Lyra Shadowbane",
    content: "I use my Cunning Action to Hide behind the broken pillar, then attempt a Sneak Attack on the skeleton!",
    timestamp: new Date(Date.now() - 20000),
    type: "action"
  },
  {
    id: "9",
    sender: "system",
    content: "Lyra Shadowbane rolled 19 for Stealth (Success!) - Hidden from enemies",
    timestamp: new Date(Date.now() - 18000),
    type: "system"
  },
  {
    id: "10",
    sender: "taleweaver",
    content: "Ah, how delightfully clever! Lyra melts into the shadows like smoke, becoming one with the darkness. The skeletal warrior's hollow sockets search frantically, but see only empty air. Sometimes, dear players, discretion truly is the better part of valor...",
    timestamp: new Date(Date.now() - 15000),
    type: "narrative"
  },
  {
    id: "11",
    sender: "character",
    characterName: "Gareth Ironshield",
    content: "I cast Lay on Hands on myself to recover some health, then move to flank the skeleton!",
    timestamp: new Date(Date.now() - 12000),
    type: "action"
  },
  {
    id: "12",
    sender: "system",
    content: "Gareth Ironshield recovers 15 hit points from Lay on Hands (15/30 uses remaining)",
    timestamp: new Date(Date.now() - 10000),
    type: "system"
  },
  {
    id: "13",
    sender: "monster",
    monsterName: "Shadow Wraith",
    content: "A chilling wail echoes through the chamber as the wraith phases through the wall, its ghostly form reaching toward Gareth's soul!",
    timestamp: new Date(Date.now() - 8000),
    type: "action"
  },
  {
    id: "14",
    sender: "system",
    content: "Shadow Wraith uses Life Drain on Gareth Ironshield - Constitution saving throw required!",
    timestamp: new Date(Date.now() - 7000),
    type: "system"
  },
  {
    id: "15",
    sender: "system",
    content: "Gareth Ironshield rolled 8 for Constitution save (Failure!) - Takes 16 necrotic damage",
    timestamp: new Date(Date.now() - 5000),
    type: "system"
  },
  {
    id: "16",
    sender: "taleweaver",
    content: "The wraith's icy touch finds its mark! Gareth staggers as his very life essence is drained away, his face growing pale as winter frost. The undead creatures circle like vultures now, sensing weakness. How wonderfully dire your situation has become!",
    timestamp: new Date(Date.now() - 3000),
    type: "narrative"
  }
];