import type { GraphQLClient, RequestOptions } from 'graphql-request';
import gql from 'graphql-tag';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type MakeEmpty<T extends { [key: string]: unknown }, K extends keyof T> = { [_ in K]?: never };
export type Incremental<T> = T | { [P in keyof T]?: P extends ' $fragmentName' | '__typename' ? T[P] : never };
type GraphQLClientRequestHeaders = RequestOptions['requestHeaders'];
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string; }
  String: { input: string; output: string; }
  Boolean: { input: boolean; output: boolean; }
  Int: { input: number; output: number; }
  Float: { input: number; output: number; }
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: { input: any; output: any; }
  /** The `Long` scalar type represents non-fractional signed whole 64-bit numeric values. Long can represent values between -(2^63) and 2^63 - 1. */
  Long: { input: any; output: any; }
  UUID: { input: any; output: any; }
};

/** Request to create a new character in the expedition. */
export type AddCharacterInput = {
  /** Narrative alignment of the character. */
  alignment: NarrativeAlignment;
  /** Attribute scores of the character. */
  attributeScores: AttributeScoresInput;
  /** Class of the character. */
  class: CharacterClass;
  /** Type of a creature, that the character is. */
  creatureType: CreatureType;
  /** Description of the expedition to drive the narrative - if empty a description will be generated. */
  description?: InputMaybe<Scalars['String']['input']>;
  /** Identifier of the expedition to create the character in. */
  expeditionId: Scalars['UUID']['input'];
  /** Gender of the character. */
  gender: CreatureGender;
  /** Name of the character - if empty a name will be generated. */
  name?: InputMaybe<Scalars['String']['input']>;
};

/** Request to create a new expedition. */
export type AddExpeditionInput = {
  /** Description of the expedition to drive the narrative - if empty a description will be generated. */
  description?: InputMaybe<Scalars['String']['input']>;
  /** Difficulty level of the expedition. */
  difficulty: ExpeditionDifficulty;
  /** Maximum number of participants allowed in the expedition. */
  maxParticipants: Scalars['Int']['input'];
  /** Minimum number of participants required to start the expedition. */
  minParticipants: Scalars['Int']['input'];
  /** Name of the expedition - if empty a name will be generated. */
  name?: InputMaybe<Scalars['String']['input']>;
};

/** Input for adding a new message to an expedition. */
export type AddMessageInput = {
  /** Content of the message to be added. */
  content: Scalars['String']['input'];
  /** Unique identifier of the expedition to which the message is being added. */
  expeditionId: Scalars['UUID']['input'];
};

/** Defines when a policy shall be executed. */
export enum ApplyPolicy {
  /** After the resolver was executed. */
  AfterResolver = 'AFTER_RESOLVER',
  /** Before the resolver was executed. */
  BeforeResolver = 'BEFORE_RESOLVER',
  /** The policy is applied in the validation step before the execution. */
  Validation = 'VALIDATION'
}

/** Breakdown of a character's attribute scores that represents their physical and mental capabilities. */
export type AttributeScores = {
  __typename?: 'AttributeScores';
  /** Charisma represents a character's charm, social skills, and ability to influence others. */
  charisma: Scalars['Int']['output'];
  /** Constitution represents a character's health, stamina, and resilience against physical challenges. */
  constitution: Scalars['Int']['output'];
  /** Dexterity represents a character's agility, reflexes, and balance. */
  dexterity: Scalars['Int']['output'];
  /** Intelligence represents a character's mental acuity, knowledge, and reasoning ability. */
  intelligence: Scalars['Int']['output'];
  /** Strength represents a character's physical power and ability to exert force. */
  strength: Scalars['Int']['output'];
  /** Wisdom represents a character's perception, insight, and intuition about the world around them. */
  wisdom: Scalars['Int']['output'];
};

/** Breakdown of a character's attribute scores that represents their physical and mental capabilities. */
export type AttributeScoresInput = {
  /** Charisma represents a character's charm, social skills, and ability to influence others. */
  charisma: Scalars['Int']['input'];
  /** Constitution represents a character's health, stamina, and resilience against physical challenges. */
  constitution: Scalars['Int']['input'];
  /** Dexterity represents a character's agility, reflexes, and balance. */
  dexterity: Scalars['Int']['input'];
  /** Intelligence represents a character's mental acuity, knowledge, and reasoning ability. */
  intelligence: Scalars['Int']['input'];
  /** Strength represents a character's physical power and ability to exert force. */
  strength: Scalars['Int']['input'];
  /** Wisdom represents a character's perception, insight, and intuition about the world around them. */
  wisdom: Scalars['Int']['input'];
};

/** Represents an user's character within the system. */
export type Character = {
  __typename?: 'Character';
  /** Narrative alignment of the character. */
  alignment: NarrativeAlignment;
  /** Armor class of the character. */
  armorClass: Scalars['Int']['output'];
  /** Attribute scores of the character. */
  attributeScores: AttributeScores;
  /** Class of the character. */
  class: CharacterClass;
  /** Type of a creature, that the character is. */
  creatureType: CreatureType;
  /** Description of the character (e.g., backstory, lore). */
  description: Scalars['String']['output'];
  /** Gender of the character. */
  gender: CreatureGender;
  /** Unique identifier of the character. */
  id: Scalars['UUID']['output'];
  /** Maximum health points of the character. */
  maxHealth: Scalars['Int']['output'];
  /** The name of the character. */
  name: Scalars['String']['output'];
  /** Speed scores of the character. */
  speedScores: SpeedScores;
  /** The traits of the character. */
  traits: Array<CreatureTrait>;
};

export enum CharacterClass {
  Alchemist = 'ALCHEMIST',
  Artificer = 'ARTIFICER',
  Barbarian = 'BARBARIAN',
  Bard = 'BARD',
  Beastmaster = 'BEASTMASTER',
  Bloodmage = 'BLOODMAGE',
  Chronomancer = 'CHRONOMANCER',
  Cleric = 'CLERIC',
  Druid = 'DRUID',
  Elementalist = 'ELEMENTALIST',
  Fighter = 'FIGHTER',
  Hexblade = 'HEXBLADE',
  Monk = 'MONK',
  Necromancer = 'NECROMANCER',
  Paladin = 'PALADIN',
  Psion = 'PSION',
  Ranger = 'RANGER',
  Rogue = 'ROGUE',
  Shadowdancer = 'SHADOWDANCER',
  Sorcerer = 'SORCERER',
  Spellblade = 'SPELLBLADE',
  Summoner = 'SUMMONER',
  Warden = 'WARDEN',
  Warlock = 'WARLOCK',
  Wizard = 'WIZARD'
}

/** Represents a single chat message within the expedition. */
export type ChatMessage = {
  __typename?: 'ChatMessage';
  /** The string content of the message. */
  content: Scalars['String']['output'];
  /** Unique identifier of the message. */
  id: Scalars['Long']['output'];
  /** The sender of the message. */
  sender?: Maybe<MessageSender>;
  /** Timestamp of when the message was created within the system. */
  timestamp: Scalars['DateTime']['output'];
  /** The type of the message. */
  type: MessageType;
};

/** Represents current state of a combat creature. */
export type CombatCreatureState = {
  __typename?: 'CombatCreatureState';
  /** Identifier of the creature this state data is related to. */
  creatureId: Scalars['UUID']['output'];
  /** Remaining hit points. */
  currentHealth: Scalars['Int']['output'];
  /** Current status of the creature. */
  status: CreatureStatus;
};

/** Represents a creature. */
export type Creature = {
  __typename?: 'Creature';
  /** Unique identifier of the creature. */
  id: Scalars['UUID']['output'];
  /** The name of the creature. */
  name: Scalars['String']['output'];
};

export enum CreatureGender {
  Female = 'FEMALE',
  Male = 'MALE',
  NonBinary = 'NON_BINARY',
  Other = 'OTHER'
}

export enum CreatureStatus {
  Alive = 'ALIVE',
  Cursed = 'CURSED',
  Dead = 'DEAD',
  Empowered = 'EMPOWERED',
  Enraged = 'ENRAGED',
  Injured = 'INJURED',
  Stunned = 'STUNNED',
  Terrified = 'TERRIFIED',
  Unconscious = 'UNCONSCIOUS',
  Weakened = 'WEAKENED'
}

/** Passive ability or feature that is always active or trigger under certain conditions. */
export type CreatureTrait = {
  __typename?: 'CreatureTrait';
  /** Optional. Condition under which the trait is active or applies. */
  conditionForPerform?: Maybe<Scalars['String']['output']>;
  /** Description of the trait's effect or purpose. */
  effect: Scalars['String']['output'];
  /** Name of the trait. */
  name: Scalars['String']['output'];
};

export enum CreatureType {
  Aberration = 'ABERRATION',
  Beast = 'BEAST',
  Celestial = 'CELESTIAL',
  Chronowyrm = 'CHRONOWYRM',
  Construct = 'CONSTRUCT',
  Dragon = 'DRAGON',
  Dreamwalker = 'DREAMWALKER',
  Elemental = 'ELEMENTAL',
  Ethereal = 'ETHEREAL',
  Fey = 'FEY',
  Fiend = 'FIEND',
  Forgeborn = 'FORGEBORN',
  Giant = 'GIANT',
  Humanoid = 'HUMANOID',
  Luminarch = 'LUMINARCH',
  Monstrosity = 'MONSTROSITY',
  Ooze = 'OOZE',
  Plant = 'PLANT',
  Psychic = 'PSYCHIC',
  Shadowspawn = 'SHADOWSPAWN',
  Starforged = 'STARFORGED',
  Steamborn = 'STEAMBORN',
  Tidehunter = 'TIDEHUNTER',
  Undead = 'UNDEAD'
}

/** Represents a page of items in cursor-based pagination. */
export type CursorPageOfChatMessageAndInt64 = {
  __typename?: 'CursorPageOfChatMessageAndInt64';
  /** The cursor pointing to the start of the page. */
  endCursor?: Maybe<Scalars['Long']['output']>;
  /** Indicates if there is a next page available. */
  hasNextPage: Scalars['Boolean']['output'];
  /** The items on the current page. */
  items: Array<ChatMessage>;
};

/** Represents an expedition. */
export type Expedition = {
  __typename?: 'Expedition';
  /** Date and time when the expedition was completed, if applicable. */
  completedAt?: Maybe<Scalars['DateTime']['output']>;
  /** Date and time when the expedition was created. */
  createdAt: Scalars['DateTime']['output'];
  /** Current number of participants in the expedition. */
  currentParticipants: Scalars['Int']['output'];
  /** Description of the expedition. */
  description: Scalars['String']['output'];
  /** Difficulty level of the expedition. */
  difficulty: ExpeditionDifficulty;
  /** Unique identifier of the expedition. */
  id: Scalars['UUID']['output'];
  /** Lesson learned from the expedition, if any. */
  lessonLearned?: Maybe<Scalars['String']['output']>;
  /** Maximum number of participants allowed for the expedition. */
  maxParticipants: Scalars['Int']['output'];
  /** Minimum number of participants required for the expedition. */
  minParticipants: Scalars['Int']['output'];
  /** Name of the expedition. */
  name: Scalars['String']['output'];
  /** The narrative messages of the expedition. */
  narrativeMessages: Array<ChatMessage>;
  /** Outcome of the expedition, if completed. */
  outcome?: Maybe<ExpeditionOutcome>;
  /** Participants of the expedition. */
  participants: Array<ExpeditionParticipant>;
  /** Date and time when the expedition was started, if applicable. */
  startedAt?: Maybe<Scalars['DateTime']['output']>;
  /** The state of the expedition. */
  state: ExpeditionState;
  /** User who created the expedition. */
  userCreated: User;
};

export enum ExpeditionDifficulty {
  Easy = 'EASY',
  Hard = 'HARD',
  Medium = 'MEDIUM',
  Nightmare = 'NIGHTMARE'
}

/** Offset paginated expeditions. */
export type ExpeditionOffsetPage = {
  __typename?: 'ExpeditionOffsetPage';
  /** The items on the current page. */
  items: Array<Expedition>;
  /** Current page number (1-based). */
  page: Scalars['Int']['output'];
  /** Number of items per page. */
  pageSize: Scalars['Int']['output'];
  /** Statistics about the expeditions. */
  statistics: ExpeditionStatistics;
  /** Total number of items available. */
  totalCount: Scalars['Int']['output'];
  /** Total number of pages available. */
  totalPages: Scalars['Int']['output'];
};

export enum ExpeditionOutcome {
  Failure = 'FAILURE',
  Success = 'SUCCESS'
}

/** Represents a participant in an expedition. */
export type ExpeditionParticipant = {
  __typename?: 'ExpeditionParticipant';
  /** Character of the participant. */
  character?: Maybe<Character>;
  /** User of the participant. */
  user: User;
};

/** Input for searching expeditions with pagination and status filters. */
export type ExpeditionSearchInput = {
  /** Filter expeditions by their difficulty level. */
  difficulty?: InputMaybe<ExpeditionDifficulty>;
  /** Filter expeditions by their outcome. */
  outcome?: InputMaybe<ExpeditionOutcome>;
  /** Current page number (1-based). */
  page: Scalars['Int']['input'];
  /** Number of items per page. */
  pageSize: Scalars['Int']['input'];
  /** Text to search for in expedition names or descriptions. */
  searchText?: InputMaybe<Scalars['String']['input']>;
  /** List of expedition statuses to filter the search results. */
  statuses: Array<ExpeditionStatus>;
};

/** Information regarding the state the expedition is in. */
export type ExpeditionState = {
  __typename?: 'ExpeditionState';
  /** Creature whose turn it is to act. */
  activeCreature?: Maybe<Creature>;
  /** States of creatures in the expedition. */
  creatureStates: Array<CombatCreatureState>;
  /** List of monsters currently present in the expedition. */
  monsters: Array<Monster>;
  /** Current status of the expedition. */
  status: ExpeditionStatus;
};

/** Represents statistics about expeditions. */
export type ExpeditionStatistics = {
  __typename?: 'ExpeditionStatistics';
  /** Number of failed expeditions. */
  failedExpeditions: Scalars['Int']['output'];
  /** Number of successful expeditions. */
  successfulExpeditions: Scalars['Int']['output'];
  /** Total number of expeditions. */
  totalExpeditions: Scalars['Int']['output'];
};

export enum ExpeditionStatus {
  Completed = 'COMPLETED',
  Created = 'CREATED',
  Started = 'STARTED'
}

/** Request to join an existing expedition. */
export type JoinExpeditionInput = {
  /** Unique identifier of the expedition to join. */
  expeditionId: Scalars['UUID']['input'];
};

/** Input for searching messages within an expedition. */
export type MessageSearchInput = {
  /** The cursor after which to start fetching items. */
  after?: InputMaybe<Scalars['Long']['input']>;
  /** Unique identifier of the expedition whose messages are being searched. */
  expeditionId: Scalars['UUID']['input'];
  /** Number of items to fetch. */
  pageSize: Scalars['Int']['input'];
};

/** A sender of a action message. */
export type MessageSender = {
  __typename?: 'MessageSender';
  /** Unique identifier of the creature. */
  creatureId: Scalars['UUID']['output'];
  /** Type of the action message sender. */
  senderType: SenderType;
};

export enum MessageType {
  Action = 'ACTION',
  ActionAdjudication = 'ACTION_ADJUDICATION',
  Narrative = 'NARRATIVE',
  System = 'SYSTEM'
}

/** Represents a monster the players have to face. */
export type Monster = {
  __typename?: 'Monster';
  /** Narrative alignment of the monster. */
  alignment: NarrativeAlignment;
  /** Armor class of the monster. */
  armorClass: Scalars['Int']['output'];
  /** Attribute scores of the monster. */
  attributeScores: AttributeScores;
  /** Class of the monster. */
  challengeRating: Scalars['Int']['output'];
  /** Type of a creature, that the monster is. */
  creatureType: CreatureType;
  /** Description of the monster (e.g., backstory, lore). */
  description: Scalars['String']['output'];
  /** Gender of the monster. */
  gender: CreatureGender;
  /** Unique identifier of the monster. */
  id: Scalars['UUID']['output'];
  /** Maximum health points of the monster. */
  maxHealth: Scalars['Int']['output'];
  /** The name of the monster. */
  name: Scalars['String']['output'];
  /** Speed scores of the monster. */
  speedScores: SpeedScores;
  /** The traits of the monster. */
  traits: Array<CreatureTrait>;
};

export type Mutation = {
  __typename?: 'Mutation';
  /** Creates a new character. */
  addCharacter: Character;
  /** Creates a new expedition. */
  addExpedition: Expedition;
  /** Creates a new message in an expedition. */
  addMessage: ChatMessage;
  /** Joins an existing expedition. */
  joinExpedition: Expedition;
  /** Starts an existing expedition. */
  startExpedition: Expedition;
};


export type MutationAddCharacterArgs = {
  input: AddCharacterInput;
};


export type MutationAddExpeditionArgs = {
  input: AddExpeditionInput;
};


export type MutationAddMessageArgs = {
  input: AddMessageInput;
};


export type MutationJoinExpeditionArgs = {
  input: JoinExpeditionInput;
};


export type MutationStartExpeditionArgs = {
  input: StartExpeditionInput;
};

export enum NarrativeAlignment {
  ChaoticEvil = 'CHAOTIC_EVIL',
  ChaoticGood = 'CHAOTIC_GOOD',
  ChaoticNeutral = 'CHAOTIC_NEUTRAL',
  LawfulEvil = 'LAWFUL_EVIL',
  LawfulGood = 'LAWFUL_GOOD',
  LawfulNeutral = 'LAWFUL_NEUTRAL',
  NeutralEvil = 'NEUTRAL_EVIL',
  NeutralGood = 'NEUTRAL_GOOD',
  TrueNeutral = 'TRUE_NEUTRAL',
  Twisted = 'TWISTED',
  Unknowable = 'UNKNOWABLE',
  Unknown = 'UNKNOWN'
}

export type Query = {
  __typename?: 'Query';
  /** Retrieves an expedition by its unique identifier. */
  expeditionById?: Maybe<Expedition>;
  /** Retrieves a paginated list of expeditions. */
  expeditions: ExpeditionOffsetPage;
  /** Retrieves a paginated list of messages for a given expedition. */
  messages: CursorPageOfChatMessageAndInt64;
};


export type QueryExpeditionByIdArgs = {
  id: Scalars['UUID']['input'];
};


export type QueryExpeditionsArgs = {
  input: ExpeditionSearchInput;
};


export type QueryMessagesArgs = {
  input: MessageSearchInput;
};

export enum SenderType {
  Character = 'CHARACTER',
  Monster = 'MONSTER'
}

/** Represents how far a creature can move on its turn during combat. */
export type SpeedScores = {
  __typename?: 'SpeedScores';
  /** Represents the creature's speed while burrowing, if applicable. */
  burrowing?: Maybe<Scalars['Int']['output']>;
  /** Represents the creature's speed while climbing, if applicable. */
  climbing?: Maybe<Scalars['Int']['output']>;
  /** Represents the creature's speed while flying, if applicable. */
  flying?: Maybe<Scalars['Int']['output']>;
  /** Represents the creature's speed while swimming, if applicable. */
  swimming?: Maybe<Scalars['Int']['output']>;
  /** Represents the creature's speed while teleporting, if applicable. */
  teleportation?: Maybe<Scalars['Int']['output']>;
  /** Default walking speed in feet. */
  walking: Scalars['Int']['output'];
};

/** Request to start an existing expedition. */
export type StartExpeditionInput = {
  /** Unique identifier of the expedition to start. */
  expeditionId: Scalars['UUID']['input'];
};

export type Subscription = {
  __typename?: 'Subscription';
  /** Notifies when the state of an expedition has changed. */
  expeditionStateChanged: ExpeditionState;
  /** Notifies when a new message has been created in an expedition. */
  messageCreated: ChatMessage;
  /** Notifies when the participants of an expedition have been updated. */
  participantsUpdated: Expedition;
};


export type SubscriptionExpeditionStateChangedArgs = {
  expeditionId: Scalars['UUID']['input'];
};


export type SubscriptionMessageCreatedArgs = {
  expeditionId: Scalars['UUID']['input'];
};


export type SubscriptionParticipantsUpdatedArgs = {
  expeditionId: Scalars['UUID']['input'];
};

/** Represents an user. */
export type User = {
  __typename?: 'User';
  /** External identifier of the user. */
  externalId: Scalars['String']['output'];
  /** Unique identifier of the user. */
  id: Scalars['UUID']['output'];
  /** Name of the user. */
  name: Scalars['String']['output'];
};

export type AddCharacterMutationVariables = Exact<{
  input: AddCharacterInput;
}>;


export type AddCharacterMutation = { __typename?: 'Mutation', addCharacter: { __typename?: 'Character', id: any } };

export type AddExpeditionMutationVariables = Exact<{
  input: AddExpeditionInput;
}>;


export type AddExpeditionMutation = { __typename?: 'Mutation', addExpedition: { __typename?: 'Expedition', id: any } };

export type JoinExpeditionMutationVariables = Exact<{
  input: JoinExpeditionInput;
}>;


export type JoinExpeditionMutation = { __typename?: 'Mutation', joinExpedition: { __typename?: 'Expedition', id: any } };

export type StartExpeditionMutationVariables = Exact<{
  input: StartExpeditionInput;
}>;


export type StartExpeditionMutation = { __typename?: 'Mutation', startExpedition: { __typename?: 'Expedition', id: any } };

export type AddMessageMutationVariables = Exact<{
  input: AddMessageInput;
}>;


export type AddMessageMutation = { __typename?: 'Mutation', addMessage: { __typename?: 'ChatMessage', id: any } };

export type GetExpeditionsQueryVariables = Exact<{
  input: ExpeditionSearchInput;
}>;


export type GetExpeditionsQuery = { __typename?: 'Query', expeditions: { __typename?: 'ExpeditionOffsetPage', totalCount: number, page: number, pageSize: number, totalPages: number, items: Array<{ __typename?: 'Expedition', id: any, name: string, description: string, difficulty: ExpeditionDifficulty, minParticipants: number, maxParticipants: number, currentParticipants: number, outcome?: ExpeditionOutcome | null, state: { __typename?: 'ExpeditionState', status: ExpeditionStatus } }> } };

export type GetArchivedExpeditionsQueryVariables = Exact<{
  input: ExpeditionSearchInput;
}>;


export type GetArchivedExpeditionsQuery = { __typename?: 'Query', expeditions: { __typename?: 'ExpeditionOffsetPage', totalCount: number, page: number, pageSize: number, totalPages: number, items: Array<{ __typename?: 'Expedition', id: any, name: string, description: string, difficulty: ExpeditionDifficulty, minParticipants: number, maxParticipants: number, currentParticipants: number, outcome?: ExpeditionOutcome | null, createdAt: any, startedAt?: any | null, completedAt?: any | null, state: { __typename?: 'ExpeditionState', status: ExpeditionStatus, creatureStates: Array<{ __typename?: 'CombatCreatureState', creatureId: any, currentHealth: number }> }, participants: Array<{ __typename?: 'ExpeditionParticipant', character?: { __typename?: 'Character', id: any } | null }> }>, statistics: { __typename?: 'ExpeditionStatistics', totalExpeditions: number, successfulExpeditions: number, failedExpeditions: number } } };

export type GetExpeditionByIdQueryVariables = Exact<{
  id: Scalars['UUID']['input'];
}>;


export type GetExpeditionByIdQuery = { __typename?: 'Query', expeditionById?: { __typename?: 'Expedition', id: any, name: string, description: string, difficulty: ExpeditionDifficulty, minParticipants: number, maxParticipants: number, currentParticipants: number, outcome?: ExpeditionOutcome | null, userCreated: { __typename?: 'User', id: any, externalId: string, name: string }, state: { __typename?: 'ExpeditionState', status: ExpeditionStatus, activeCreature?: { __typename?: 'Creature', id: any, name: string } | null, creatureStates: Array<{ __typename?: 'CombatCreatureState', creatureId: any, status: CreatureStatus, currentHealth: number }>, monsters: Array<{ __typename?: 'Monster', id: any, name: string, description: string, gender: CreatureGender, creatureType: CreatureType, alignment: NarrativeAlignment, challengeRating: number, maxHealth: number, armorClass: number, speedScores: { __typename?: 'SpeedScores', walking: number, swimming?: number | null, flying?: number | null, climbing?: number | null, burrowing?: number | null, teleportation?: number | null }, attributeScores: { __typename?: 'AttributeScores', strength: number, wisdom: number, intelligence: number, dexterity: number, constitution: number, charisma: number }, traits: Array<{ __typename?: 'CreatureTrait', name: string, effect: string, conditionForPerform?: string | null }> }> }, participants: Array<{ __typename?: 'ExpeditionParticipant', user: { __typename?: 'User', id: any, externalId: string, name: string }, character?: { __typename?: 'Character', id: any, name: string, description: string, gender: CreatureGender, creatureType: CreatureType, alignment: NarrativeAlignment, class: CharacterClass, maxHealth: number, armorClass: number, speedScores: { __typename?: 'SpeedScores', walking: number, swimming?: number | null, flying?: number | null, climbing?: number | null, burrowing?: number | null, teleportation?: number | null }, attributeScores: { __typename?: 'AttributeScores', strength: number, wisdom: number, intelligence: number, dexterity: number, constitution: number, charisma: number }, traits: Array<{ __typename?: 'CreatureTrait', name: string, effect: string, conditionForPerform?: string | null }> } | null }> } | null };

export type GetArchivedExpeditionByIdQueryVariables = Exact<{
  id: Scalars['UUID']['input'];
}>;


export type GetArchivedExpeditionByIdQuery = { __typename?: 'Query', expeditionById?: { __typename?: 'Expedition', id: any, name: string, description: string, difficulty: ExpeditionDifficulty, maxParticipants: number, currentParticipants: number, outcome?: ExpeditionOutcome | null, lessonLearned?: string | null, startedAt?: any | null, completedAt?: any | null, state: { __typename?: 'ExpeditionState', creatureStates: Array<{ __typename?: 'CombatCreatureState', creatureId: any, currentHealth: number }>, monsters: Array<{ __typename?: 'Monster', id: any, name: string, description: string, gender: CreatureGender, creatureType: CreatureType, alignment: NarrativeAlignment, challengeRating: number, maxHealth: number, armorClass: number }> }, participants: Array<{ __typename?: 'ExpeditionParticipant', character?: { __typename?: 'Character', id: any, name: string, description: string, gender: CreatureGender, creatureType: CreatureType, alignment: NarrativeAlignment, class: CharacterClass, maxHealth: number, armorClass: number } | null }>, narrativeMessages: Array<{ __typename?: 'ChatMessage', id: any, content: string, timestamp: any }> } | null };

export type GetMessagesQueryVariables = Exact<{
  input: MessageSearchInput;
}>;


export type GetMessagesQuery = { __typename?: 'Query', messages: { __typename?: 'CursorPageOfChatMessageAndInt64', hasNextPage: boolean, endCursor?: any | null, items: Array<{ __typename?: 'ChatMessage', id: any, type: MessageType, content: string, timestamp: any, sender?: { __typename?: 'MessageSender', creatureId: any, senderType: SenderType } | null }> } };

export type OnParticipantsChangedSubscriptionVariables = Exact<{
  expeditionId: Scalars['UUID']['input'];
}>;


export type OnParticipantsChangedSubscription = { __typename?: 'Subscription', participantsUpdated: { __typename?: 'Expedition', id: any, participants: Array<{ __typename?: 'ExpeditionParticipant', user: { __typename?: 'User', id: any }, character?: { __typename?: 'Character', id: any } | null }> } };

export type OnExpeditionStateChangedSubscriptionVariables = Exact<{
  expeditionId: Scalars['UUID']['input'];
}>;


export type OnExpeditionStateChangedSubscription = { __typename?: 'Subscription', expeditionStateChanged: { __typename?: 'ExpeditionState', status: ExpeditionStatus, activeCreature?: { __typename?: 'Creature', id: any, name: string } | null, creatureStates: Array<{ __typename?: 'CombatCreatureState', creatureId: any, status: CreatureStatus, currentHealth: number }>, monsters: Array<{ __typename?: 'Monster', id: any, name: string, description: string, gender: CreatureGender, creatureType: CreatureType, alignment: NarrativeAlignment, challengeRating: number, maxHealth: number, armorClass: number, speedScores: { __typename?: 'SpeedScores', walking: number, swimming?: number | null, flying?: number | null, climbing?: number | null, burrowing?: number | null, teleportation?: number | null }, attributeScores: { __typename?: 'AttributeScores', strength: number, wisdom: number, intelligence: number, dexterity: number, constitution: number, charisma: number }, traits: Array<{ __typename?: 'CreatureTrait', name: string, effect: string, conditionForPerform?: string | null }> }> } };

export type OnMessageCreatedSubscriptionVariables = Exact<{
  expeditionId: Scalars['UUID']['input'];
}>;


export type OnMessageCreatedSubscription = { __typename?: 'Subscription', messageCreated: { __typename?: 'ChatMessage', id: any, type: MessageType, content: string, timestamp: any, sender?: { __typename?: 'MessageSender', creatureId: any, senderType: SenderType } | null } };


export const AddCharacterDocument = gql`
    mutation AddCharacter($input: AddCharacterInput!) {
  addCharacter(input: $input) {
    id
  }
}
    `;
export const AddExpeditionDocument = gql`
    mutation AddExpedition($input: AddExpeditionInput!) {
  addExpedition(input: $input) {
    id
  }
}
    `;
export const JoinExpeditionDocument = gql`
    mutation JoinExpedition($input: JoinExpeditionInput!) {
  joinExpedition(input: $input) {
    id
  }
}
    `;
export const StartExpeditionDocument = gql`
    mutation StartExpedition($input: StartExpeditionInput!) {
  startExpedition(input: $input) {
    id
  }
}
    `;
export const AddMessageDocument = gql`
    mutation AddMessage($input: AddMessageInput!) {
  addMessage(input: $input) {
    id
  }
}
    `;
export const GetExpeditionsDocument = gql`
    query GetExpeditions($input: ExpeditionSearchInput!) {
  expeditions(input: $input) {
    items {
      id
      name
      description
      difficulty
      minParticipants
      maxParticipants
      currentParticipants
      outcome
      state {
        status
      }
    }
    totalCount
    page
    pageSize
    totalPages
  }
}
    `;
export const GetArchivedExpeditionsDocument = gql`
    query GetArchivedExpeditions($input: ExpeditionSearchInput!) {
  expeditions(input: $input) {
    items {
      id
      name
      description
      difficulty
      minParticipants
      maxParticipants
      currentParticipants
      outcome
      createdAt
      startedAt
      completedAt
      state {
        status
        creatureStates {
          creatureId
          currentHealth
        }
      }
      participants {
        character {
          id
        }
      }
    }
    statistics {
      totalExpeditions
      successfulExpeditions
      failedExpeditions
    }
    totalCount
    page
    pageSize
    totalPages
  }
}
    `;
export const GetExpeditionByIdDocument = gql`
    query GetExpeditionById($id: UUID!) {
  expeditionById(id: $id) {
    id
    name
    description
    difficulty
    minParticipants
    maxParticipants
    currentParticipants
    outcome
    userCreated {
      id
      externalId
      name
    }
    state {
      status
      activeCreature {
        id
        name
      }
      creatureStates {
        creatureId
        status
        currentHealth
      }
      monsters {
        id
        name
        description
        gender
        creatureType
        alignment
        challengeRating
        maxHealth
        armorClass
        speedScores {
          walking
          swimming
          flying
          climbing
          burrowing
          teleportation
        }
        attributeScores {
          strength
          wisdom
          intelligence
          dexterity
          constitution
          charisma
        }
        traits {
          name
          effect
          conditionForPerform
        }
      }
    }
    participants {
      user {
        id
        externalId
        name
      }
      character {
        id
        name
        description
        gender
        creatureType
        alignment
        class
        maxHealth
        armorClass
        speedScores {
          walking
          swimming
          flying
          climbing
          burrowing
          teleportation
        }
        attributeScores {
          strength
          wisdom
          intelligence
          dexterity
          constitution
          charisma
        }
        traits {
          name
          effect
          conditionForPerform
        }
      }
    }
  }
}
    `;
export const GetArchivedExpeditionByIdDocument = gql`
    query GetArchivedExpeditionById($id: UUID!) {
  expeditionById(id: $id) {
    id
    name
    description
    difficulty
    maxParticipants
    currentParticipants
    outcome
    lessonLearned
    startedAt
    completedAt
    state {
      creatureStates {
        creatureId
        currentHealth
      }
      monsters {
        id
        name
        description
        gender
        creatureType
        alignment
        challengeRating
        maxHealth
        armorClass
      }
    }
    participants {
      character {
        id
        name
        description
        gender
        creatureType
        alignment
        class
        maxHealth
        armorClass
      }
    }
    narrativeMessages {
      id
      content
      timestamp
    }
  }
}
    `;
export const GetMessagesDocument = gql`
    query GetMessages($input: MessageSearchInput!) {
  messages(input: $input) {
    items {
      id
      sender {
        creatureId
        senderType
      }
      type
      content
      timestamp
    }
    hasNextPage
    endCursor
  }
}
    `;
export const OnParticipantsChangedDocument = gql`
    subscription OnParticipantsChanged($expeditionId: UUID!) {
  participantsUpdated(expeditionId: $expeditionId) {
    id
    participants {
      user {
        id
      }
      character {
        id
      }
    }
  }
}
    `;
export const OnExpeditionStateChangedDocument = gql`
    subscription OnExpeditionStateChanged($expeditionId: UUID!) {
  expeditionStateChanged(expeditionId: $expeditionId) {
    status
    activeCreature {
      id
      name
    }
    creatureStates {
      creatureId
      status
      currentHealth
    }
    monsters {
      id
      name
      description
      gender
      creatureType
      alignment
      challengeRating
      maxHealth
      armorClass
      speedScores {
        walking
        swimming
        flying
        climbing
        burrowing
        teleportation
      }
      attributeScores {
        strength
        wisdom
        intelligence
        dexterity
        constitution
        charisma
      }
      traits {
        name
        effect
        conditionForPerform
      }
    }
  }
}
    `;
export const OnMessageCreatedDocument = gql`
    subscription OnMessageCreated($expeditionId: UUID!) {
  messageCreated(expeditionId: $expeditionId) {
    id
    sender {
      creatureId
      senderType
    }
    type
    content
    timestamp
  }
}
    `;

export type SdkFunctionWrapper = <T>(action: (requestHeaders?:Record<string, string>) => Promise<T>, operationName: string, operationType?: string, variables?: any) => Promise<T>;


const defaultWrapper: SdkFunctionWrapper = (action, _operationName, _operationType, _variables) => action();

export function getSdk(client: GraphQLClient, withWrapper: SdkFunctionWrapper = defaultWrapper) {
  return {
    AddCharacter(variables: AddCharacterMutationVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<AddCharacterMutation> {
      return withWrapper((wrappedRequestHeaders) => client.request<AddCharacterMutation>({ document: AddCharacterDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'AddCharacter', 'mutation', variables);
    },
    AddExpedition(variables: AddExpeditionMutationVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<AddExpeditionMutation> {
      return withWrapper((wrappedRequestHeaders) => client.request<AddExpeditionMutation>({ document: AddExpeditionDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'AddExpedition', 'mutation', variables);
    },
    JoinExpedition(variables: JoinExpeditionMutationVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<JoinExpeditionMutation> {
      return withWrapper((wrappedRequestHeaders) => client.request<JoinExpeditionMutation>({ document: JoinExpeditionDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'JoinExpedition', 'mutation', variables);
    },
    StartExpedition(variables: StartExpeditionMutationVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<StartExpeditionMutation> {
      return withWrapper((wrappedRequestHeaders) => client.request<StartExpeditionMutation>({ document: StartExpeditionDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'StartExpedition', 'mutation', variables);
    },
    AddMessage(variables: AddMessageMutationVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<AddMessageMutation> {
      return withWrapper((wrappedRequestHeaders) => client.request<AddMessageMutation>({ document: AddMessageDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'AddMessage', 'mutation', variables);
    },
    GetExpeditions(variables: GetExpeditionsQueryVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<GetExpeditionsQuery> {
      return withWrapper((wrappedRequestHeaders) => client.request<GetExpeditionsQuery>({ document: GetExpeditionsDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'GetExpeditions', 'query', variables);
    },
    GetArchivedExpeditions(variables: GetArchivedExpeditionsQueryVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<GetArchivedExpeditionsQuery> {
      return withWrapper((wrappedRequestHeaders) => client.request<GetArchivedExpeditionsQuery>({ document: GetArchivedExpeditionsDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'GetArchivedExpeditions', 'query', variables);
    },
    GetExpeditionById(variables: GetExpeditionByIdQueryVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<GetExpeditionByIdQuery> {
      return withWrapper((wrappedRequestHeaders) => client.request<GetExpeditionByIdQuery>({ document: GetExpeditionByIdDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'GetExpeditionById', 'query', variables);
    },
    GetArchivedExpeditionById(variables: GetArchivedExpeditionByIdQueryVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<GetArchivedExpeditionByIdQuery> {
      return withWrapper((wrappedRequestHeaders) => client.request<GetArchivedExpeditionByIdQuery>({ document: GetArchivedExpeditionByIdDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'GetArchivedExpeditionById', 'query', variables);
    },
    GetMessages(variables: GetMessagesQueryVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<GetMessagesQuery> {
      return withWrapper((wrappedRequestHeaders) => client.request<GetMessagesQuery>({ document: GetMessagesDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'GetMessages', 'query', variables);
    },
    OnParticipantsChanged(variables: OnParticipantsChangedSubscriptionVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<OnParticipantsChangedSubscription> {
      return withWrapper((wrappedRequestHeaders) => client.request<OnParticipantsChangedSubscription>({ document: OnParticipantsChangedDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'OnParticipantsChanged', 'subscription', variables);
    },
    OnExpeditionStateChanged(variables: OnExpeditionStateChangedSubscriptionVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<OnExpeditionStateChangedSubscription> {
      return withWrapper((wrappedRequestHeaders) => client.request<OnExpeditionStateChangedSubscription>({ document: OnExpeditionStateChangedDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'OnExpeditionStateChanged', 'subscription', variables);
    },
    OnMessageCreated(variables: OnMessageCreatedSubscriptionVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<OnMessageCreatedSubscription> {
      return withWrapper((wrappedRequestHeaders) => client.request<OnMessageCreatedSubscription>({ document: OnMessageCreatedDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'OnMessageCreated', 'subscription', variables);
    }
  };
}
export type Sdk = ReturnType<typeof getSdk>;