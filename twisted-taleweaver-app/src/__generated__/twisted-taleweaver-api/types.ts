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
  UUID: { input: any; output: any; }
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

export enum CreatureGender {
  Female = 'FEMALE',
  Male = 'MALE',
  NonBinary = 'NON_BINARY',
  Other = 'OTHER'
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
  /** Creates a new expedition. */
  addExpedition: Expedition;
  /** Joins an existing expedition. */
  joinExpedition: Expedition;
};


export type MutationAddExpeditionArgs = {
  input: AddExpeditionInput;
};


export type MutationJoinExpeditionArgs = {
  input: JoinExpeditionInput;
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

export type AddExpeditionMutationVariables = Exact<{
  input: AddExpeditionInput;
}>;


export type AddExpeditionMutation = { __typename?: 'Mutation', addExpedition: { __typename?: 'Expedition', id: any } };

export type JoinExpeditionMutationVariables = Exact<{
  input: JoinExpeditionInput;
}>;


export type JoinExpeditionMutation = { __typename?: 'Mutation', joinExpedition: { __typename?: 'Expedition', id: any } };

export type GetExpeditionsQueryVariables = Exact<{
  page?: InputMaybe<Scalars['Int']['input']>;
  pageSize?: InputMaybe<Scalars['Int']['input']>;
}>;


export type GetExpeditionsQuery = { __typename?: 'Query', expeditions: { __typename?: 'OffsetPageOfExpedition', totalCount: number, page: number, pageSize: number, totalPages: number, items: Array<{ __typename?: 'Expedition', id: any, name: string, description: string, difficulty: ExpeditionDifficulty, minParticipants: number, maxParticipants: number, currentParticipants: number, outcome?: ExpeditionOutcome | null, state: { __typename?: 'ExpeditionState', status: ExpeditionStatus } }> } };

export type GetExpeditionByIdQueryVariables = Exact<{
  id: Scalars['UUID']['input'];
}>;


export type GetExpeditionByIdQuery = { __typename?: 'Query', expeditionById?: { __typename?: 'Expedition', id: any, name: string, description: string, difficulty: ExpeditionDifficulty, minParticipants: number, maxParticipants: number, currentParticipants: number, outcome?: ExpeditionOutcome | null, userCreated: { __typename?: 'User', id: any, externalId: string, name: string }, state: { __typename?: 'ExpeditionState', status: ExpeditionStatus, activeUser?: { __typename?: 'User', id: any, externalId: string } | null }, participants: Array<{ __typename?: 'ExpeditionParticipant', user: { __typename?: 'User', id: any, externalId: string, name: string }, character?: { __typename?: 'Character', id: any, name: string, gender: CreatureGender, creatureType: CreatureType, alignment: NarrativeAlignment, class: CharacterClass, maxHealth: number, armorClass: number, speedScores: { __typename?: 'SpeedScores', walking: number, swimming?: number | null, flying?: number | null, climbing?: number | null, burrowing?: number | null, teleportation?: number | null }, attributeScores: { __typename?: 'AttributeScores', strength: number, wisdom: number, intelligence: number, dexterity: number, constitution: number, charisma: number }, traits: Array<{ __typename?: 'CreatureTrait', name: string, effect: string, conditionForPerform?: string | null }> } | null }> } | null };


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
export const GetExpeditionsDocument = gql`
    query GetExpeditions($page: Int = 1, $pageSize: Int = 20) {
  expeditions(page: $page, pageSize: $pageSize) {
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
      activeUser {
        id
        externalId
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

export type SdkFunctionWrapper = <T>(action: (requestHeaders?:Record<string, string>) => Promise<T>, operationName: string, operationType?: string, variables?: any) => Promise<T>;


const defaultWrapper: SdkFunctionWrapper = (action, _operationName, _operationType, _variables) => action();

export function getSdk(client: GraphQLClient, withWrapper: SdkFunctionWrapper = defaultWrapper) {
  return {
    AddExpedition(variables: AddExpeditionMutationVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<AddExpeditionMutation> {
      return withWrapper((wrappedRequestHeaders) => client.request<AddExpeditionMutation>({ document: AddExpeditionDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'AddExpedition', 'mutation', variables);
    },
    JoinExpedition(variables: JoinExpeditionMutationVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<JoinExpeditionMutation> {
      return withWrapper((wrappedRequestHeaders) => client.request<JoinExpeditionMutation>({ document: JoinExpeditionDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'JoinExpedition', 'mutation', variables);
    },
    GetExpeditions(variables?: GetExpeditionsQueryVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<GetExpeditionsQuery> {
      return withWrapper((wrappedRequestHeaders) => client.request<GetExpeditionsQuery>({ document: GetExpeditionsDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'GetExpeditions', 'query', variables);
    },
    GetExpeditionById(variables: GetExpeditionByIdQueryVariables, requestHeaders?: GraphQLClientRequestHeaders, signal?: RequestInit['signal']): Promise<GetExpeditionByIdQuery> {
      return withWrapper((wrappedRequestHeaders) => client.request<GetExpeditionByIdQuery>({ document: GetExpeditionByIdDocument, variables, requestHeaders: { ...requestHeaders, ...wrappedRequestHeaders }, signal }), 'GetExpeditionById', 'query', variables);
    }
  };
}
export type Sdk = ReturnType<typeof getSdk>;