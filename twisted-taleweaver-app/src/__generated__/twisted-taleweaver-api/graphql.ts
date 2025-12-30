/* eslint-disable */
import type { TypedDocumentNode as DocumentNode } from '@graphql-typed-document-node/core';
export type Maybe<T> = T | null;
export type InputMaybe<T> = T | null | undefined;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type MakeEmpty<T extends { [key: string]: unknown }, K extends keyof T> = { [_ in K]?: never };
export type Incremental<T> = T | { [P in keyof T]?: P extends ' $fragmentName' | '__typename' ? T[P] : never };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string; }
  String: { input: string; output: string; }
  Boolean: { input: boolean; output: boolean; }
  Int: { input: number; output: number; }
  Float: { input: number; output: number; }
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

/** Represents an expedition. */
export type Expedition = {
  __typename?: 'Expedition';
  /** Current number of participants in the expedition. */
  currentParticipants: Scalars['Int']['output'];
  /** Description of the expedition. */
  description: Scalars['String']['output'];
  /** Difficulty level of the expedition. */
  difficulty: ExpeditionDifficulty;
  /** Unique identifier of the expedition. */
  id: Scalars['UUID']['output'];
  /** Maximum number of participants allowed for the expedition. */
  maxParticipants: Scalars['Int']['output'];
  /** Minimum number of participants required for the expedition. */
  minParticipants: Scalars['Int']['output'];
  /** Name of the expedition. */
  name: Scalars['String']['output'];
  /** Outcome of the expedition, if completed. */
  outcome?: Maybe<ExpeditionOutcome>;
  /** Participants of the expedition. */
  participants: Array<ExpeditionParticipant>;
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

/** Information regarding the state the expedition is in. */
export type ExpeditionState = {
  __typename?: 'ExpeditionState';
  /** User whose turn it is to act. */
  activeUser?: Maybe<User>;
  /** States of characters in the expedition. */
  characterStates: Array<CombatCreatureState>;
  /** Current status of the expedition. */
  status: ExpeditionStatus;
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

export type Mutation = {
  __typename?: 'Mutation';
  /** Creates a new character. */
  addCharacter: Character;
  /** Creates a new expedition. */
  addExpedition: Expedition;
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

/** Represents a page of items using offset pagination. */
export type OffsetPageOfExpedition = {
  __typename?: 'OffsetPageOfExpedition';
  /** The items on the current page. */
  items: Array<Expedition>;
  /** Current page number (1-based). */
  page: Scalars['Int']['output'];
  /** Number of items per page. */
  pageSize: Scalars['Int']['output'];
  /** Total number of items available. */
  totalCount: Scalars['Int']['output'];
  /** Total number of pages available. */
  totalPages: Scalars['Int']['output'];
};

export type Query = {
  __typename?: 'Query';
  /** Retrieves an expedition by its unique identifier. */
  expeditionById?: Maybe<Expedition>;
  /** Retrieves a paginated list of expeditions. */
  expeditions: OffsetPageOfExpedition;
};


export type QueryExpeditionByIdArgs = {
  id: Scalars['UUID']['input'];
};


export type QueryExpeditionsArgs = {
  page?: Scalars['Int']['input'];
  pageSize?: Scalars['Int']['input'];
};

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

export type GetExpeditionsQueryVariables = Exact<{
  page?: InputMaybe<Scalars['Int']['input']>;
  pageSize?: InputMaybe<Scalars['Int']['input']>;
}>;


export type GetExpeditionsQuery = { __typename?: 'Query', expeditions: { __typename?: 'OffsetPageOfExpedition', totalCount: number, page: number, pageSize: number, totalPages: number, items: Array<{ __typename?: 'Expedition', id: any, name: string, description: string, difficulty: ExpeditionDifficulty, minParticipants: number, maxParticipants: number, currentParticipants: number, outcome?: ExpeditionOutcome | null, state: { __typename?: 'ExpeditionState', status: ExpeditionStatus } }> } };

export type GetExpeditionByIdQueryVariables = Exact<{
  id: Scalars['UUID']['input'];
}>;


export type GetExpeditionByIdQuery = { __typename?: 'Query', expeditionById?: { __typename?: 'Expedition', id: any, name: string, description: string, difficulty: ExpeditionDifficulty, minParticipants: number, maxParticipants: number, currentParticipants: number, outcome?: ExpeditionOutcome | null, userCreated: { __typename?: 'User', id: any, externalId: string, name: string }, state: { __typename?: 'ExpeditionState', status: ExpeditionStatus, activeUser?: { __typename?: 'User', id: any, externalId: string } | null, characterStates: Array<{ __typename?: 'CombatCreatureState', creatureId: any, status: CreatureStatus, currentHealth: number }> }, participants: Array<{ __typename?: 'ExpeditionParticipant', user: { __typename?: 'User', id: any, externalId: string, name: string }, character?: { __typename?: 'Character', id: any, name: string, gender: CreatureGender, creatureType: CreatureType, alignment: NarrativeAlignment, class: CharacterClass, maxHealth: number, armorClass: number, speedScores: { __typename?: 'SpeedScores', walking: number, swimming?: number | null, flying?: number | null, climbing?: number | null, burrowing?: number | null, teleportation?: number | null }, attributeScores: { __typename?: 'AttributeScores', strength: number, wisdom: number, intelligence: number, dexterity: number, constitution: number, charisma: number }, traits: Array<{ __typename?: 'CreatureTrait', name: string, effect: string, conditionForPerform?: string | null }> } | null }> } | null };


export const AddCharacterDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"mutation","name":{"kind":"Name","value":"AddCharacter"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"input"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"AddCharacterInput"}}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"addCharacter"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"input"},"value":{"kind":"Variable","name":{"kind":"Name","value":"input"}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}}]}}]}}]} as unknown as DocumentNode<AddCharacterMutation, AddCharacterMutationVariables>;
export const AddExpeditionDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"mutation","name":{"kind":"Name","value":"AddExpedition"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"input"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"AddExpeditionInput"}}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"addExpedition"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"input"},"value":{"kind":"Variable","name":{"kind":"Name","value":"input"}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}}]}}]}}]} as unknown as DocumentNode<AddExpeditionMutation, AddExpeditionMutationVariables>;
export const JoinExpeditionDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"mutation","name":{"kind":"Name","value":"JoinExpedition"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"input"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"JoinExpeditionInput"}}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"joinExpedition"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"input"},"value":{"kind":"Variable","name":{"kind":"Name","value":"input"}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}}]}}]}}]} as unknown as DocumentNode<JoinExpeditionMutation, JoinExpeditionMutationVariables>;
export const StartExpeditionDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"mutation","name":{"kind":"Name","value":"StartExpedition"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"input"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"StartExpeditionInput"}}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"startExpedition"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"input"},"value":{"kind":"Variable","name":{"kind":"Name","value":"input"}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}}]}}]}}]} as unknown as DocumentNode<StartExpeditionMutation, StartExpeditionMutationVariables>;
export const GetExpeditionsDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"query","name":{"kind":"Name","value":"GetExpeditions"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"page"}},"type":{"kind":"NamedType","name":{"kind":"Name","value":"Int"}},"defaultValue":{"kind":"IntValue","value":"1"}},{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"pageSize"}},"type":{"kind":"NamedType","name":{"kind":"Name","value":"Int"}},"defaultValue":{"kind":"IntValue","value":"20"}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"expeditions"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"page"},"value":{"kind":"Variable","name":{"kind":"Name","value":"page"}}},{"kind":"Argument","name":{"kind":"Name","value":"pageSize"},"value":{"kind":"Variable","name":{"kind":"Name","value":"pageSize"}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"items"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"name"}},{"kind":"Field","name":{"kind":"Name","value":"description"}},{"kind":"Field","name":{"kind":"Name","value":"difficulty"}},{"kind":"Field","name":{"kind":"Name","value":"minParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"maxParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"currentParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"outcome"}},{"kind":"Field","name":{"kind":"Name","value":"state"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"status"}}]}}]}},{"kind":"Field","name":{"kind":"Name","value":"totalCount"}},{"kind":"Field","name":{"kind":"Name","value":"page"}},{"kind":"Field","name":{"kind":"Name","value":"pageSize"}},{"kind":"Field","name":{"kind":"Name","value":"totalPages"}}]}}]}}]} as unknown as DocumentNode<GetExpeditionsQuery, GetExpeditionsQueryVariables>;
export const GetExpeditionByIdDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"query","name":{"kind":"Name","value":"GetExpeditionById"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"id"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"UUID"}}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"expeditionById"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"id"},"value":{"kind":"Variable","name":{"kind":"Name","value":"id"}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"name"}},{"kind":"Field","name":{"kind":"Name","value":"description"}},{"kind":"Field","name":{"kind":"Name","value":"difficulty"}},{"kind":"Field","name":{"kind":"Name","value":"minParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"maxParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"currentParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"outcome"}},{"kind":"Field","name":{"kind":"Name","value":"userCreated"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"externalId"}},{"kind":"Field","name":{"kind":"Name","value":"name"}}]}},{"kind":"Field","name":{"kind":"Name","value":"state"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"status"}},{"kind":"Field","name":{"kind":"Name","value":"activeUser"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"externalId"}}]}},{"kind":"Field","name":{"kind":"Name","value":"characterStates"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"creatureId"}},{"kind":"Field","name":{"kind":"Name","value":"status"}},{"kind":"Field","name":{"kind":"Name","value":"currentHealth"}}]}}]}},{"kind":"Field","name":{"kind":"Name","value":"participants"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"user"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"externalId"}},{"kind":"Field","name":{"kind":"Name","value":"name"}}]}},{"kind":"Field","name":{"kind":"Name","value":"character"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"name"}},{"kind":"Field","name":{"kind":"Name","value":"gender"}},{"kind":"Field","name":{"kind":"Name","value":"creatureType"}},{"kind":"Field","name":{"kind":"Name","value":"alignment"}},{"kind":"Field","name":{"kind":"Name","value":"class"}},{"kind":"Field","name":{"kind":"Name","value":"maxHealth"}},{"kind":"Field","name":{"kind":"Name","value":"armorClass"}},{"kind":"Field","name":{"kind":"Name","value":"speedScores"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"walking"}},{"kind":"Field","name":{"kind":"Name","value":"swimming"}},{"kind":"Field","name":{"kind":"Name","value":"flying"}},{"kind":"Field","name":{"kind":"Name","value":"climbing"}},{"kind":"Field","name":{"kind":"Name","value":"burrowing"}},{"kind":"Field","name":{"kind":"Name","value":"teleportation"}}]}},{"kind":"Field","name":{"kind":"Name","value":"attributeScores"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"strength"}},{"kind":"Field","name":{"kind":"Name","value":"wisdom"}},{"kind":"Field","name":{"kind":"Name","value":"intelligence"}},{"kind":"Field","name":{"kind":"Name","value":"dexterity"}},{"kind":"Field","name":{"kind":"Name","value":"constitution"}},{"kind":"Field","name":{"kind":"Name","value":"charisma"}}]}},{"kind":"Field","name":{"kind":"Name","value":"traits"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"name"}},{"kind":"Field","name":{"kind":"Name","value":"effect"}},{"kind":"Field","name":{"kind":"Name","value":"conditionForPerform"}}]}}]}}]}}]}}]}}]} as unknown as DocumentNode<GetExpeditionByIdQuery, GetExpeditionByIdQueryVariables>;