import type { PendingExpedition } from "@/interfaces/PendingExpedition";
import type { ChatMessage } from "@/interfaces/ChatMessage";

export const mockPendingExpedition: PendingExpedition = {
  id: "pending-1",
  name: "The Whispering Depths",
  description: "A treacherous descent into forgotten caverns where ancient evils stir. The Taleweaver has prepared something particularly sinister for this expedition...",
  ownerId: "char-1",
  ownerName: "test", // Match the default username from auth
  status: "ready", // All players have joined, ready to start
  maxPlayers: 4,
  currentPlayers: 4,
  difficulty: "medium",
  createdAt: new Date(Date.now() - 600000), // 10 minutes ago
  participants: [
    {
      characterId: "char-1",
      characterName: "test", // Match the default username
      joinedAt: new Date(Date.now() - 600000)
    },
    {
      characterId: "char-2", 
      characterName: "Lyra Shadowbane",
      joinedAt: new Date(Date.now() - 480000)
    },
    {
      characterId: "char-3",
      characterName: "Gareth Ironshield", 
      joinedAt: new Date(Date.now() - 360000)
    },
    {
      characterId: "char-4",
      characterName: "Elena Frostweaver",
      joinedAt: new Date(Date.now() - 240000)
    }
  ]
};

export const mockPreExpeditionMessages: ChatMessage[] = [
  {
    id: "pre-1",
    sender: "system",
    content: "Expedition 'The Whispering Depths' has been created by test.",
    timestamp: new Date(Date.now() - 600000),
    type: "system"
  },
  {
    id: "pre-2",
    sender: "system", 
    content: "Lyra Shadowbane has joined the expedition. Waiting for 2 more adventurers...",
    timestamp: new Date(Date.now() - 480000),
    type: "system"
  },
  {
    id: "pre-3",
    sender: "character",
    characterName: "Lyra Shadowbane",
    content: "Ready to delve into whatever horrors await us in those depths.",
    timestamp: new Date(Date.now() - 470000),
    type: "dialogue"
  },
  {
    id: "pre-4",
    sender: "system",
    content: "Gareth Ironshield has joined the expedition. Waiting for 1 more adventurer...",
    timestamp: new Date(Date.now() - 360000),
    type: "system"
  },
  {
    id: "pre-5",
    sender: "character",
    characterName: "Gareth Ironshield", 
    content: "My shield stands ready. Let's hope we all make it back alive.",
    timestamp: new Date(Date.now() - 350000),
    type: "dialogue"
  },
  {
    id: "pre-6",
    sender: "character",
    characterName: "test",
    content: "Welcome friends. I've prepared well for this journey - let's hope it's enough.",
    timestamp: new Date(Date.now() - 300000),
    type: "dialogue"
  },
  {
    id: "pre-7",
    sender: "system",
    content: "Elena Frostweaver has joined the expedition. All adventurers have joined - ready to begin!",
    timestamp: new Date(Date.now() - 240000),
    type: "system"
  },
  {
    id: "pre-8",
    sender: "character",
    characterName: "Elena Frostweaver",
    content: "My frost magic will serve us well in the darkness ahead. I'm ready when you are.",
    timestamp: new Date(Date.now() - 230000),
    type: "dialogue"
  },
  {
    id: "pre-9",
    sender: "character",
    characterName: "test",
    content: "Excellent! Now that everyone is here, we can begin our descent. May fortune favor us all.",
    timestamp: new Date(Date.now() - 120000),
    type: "dialogue"
  },
  {
    id: "pre-10",
    sender: "taleweaver",
    content: "How touching... such optimism, such camaraderie. It will make your inevitable doom all the more delicious to witness. Shall we begin this tale of woe?",
    timestamp: new Date(Date.now() - 60000),
    type: "narrative"
  }
];