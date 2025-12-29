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
  /** User of the participant. */
  user: User;
};

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


export type AddExpeditionMutation = { __typename?: 'Mutation', addExpedition: { __typename?: 'Expedition', id: any, name: string, description: string, difficulty: ExpeditionDifficulty, minParticipants: number, maxParticipants: number, currentParticipants: number, outcome?: ExpeditionOutcome | null } };

export type JoinExpeditionMutationVariables = Exact<{
  input: JoinExpeditionInput;
}>;


export type JoinExpeditionMutation = { __typename?: 'Mutation', joinExpedition: { __typename?: 'Expedition', id: any, name: string, description: string, difficulty: ExpeditionDifficulty, minParticipants: number, maxParticipants: number, currentParticipants: number, outcome?: ExpeditionOutcome | null } };

export type GetExpeditionsQueryVariables = Exact<{
  page?: InputMaybe<Scalars['Int']['input']>;
  pageSize?: InputMaybe<Scalars['Int']['input']>;
}>;


export type GetExpeditionsQuery = { __typename?: 'Query', expeditions: { __typename?: 'OffsetPageOfExpedition', totalCount: number, page: number, pageSize: number, totalPages: number, items: Array<{ __typename?: 'Expedition', id: any, name: string, description: string, difficulty: ExpeditionDifficulty, minParticipants: number, maxParticipants: number, currentParticipants: number, outcome?: ExpeditionOutcome | null }> } };


export const AddExpeditionDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"mutation","name":{"kind":"Name","value":"AddExpedition"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"input"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"AddExpeditionInput"}}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"addExpedition"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"input"},"value":{"kind":"Variable","name":{"kind":"Name","value":"input"}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"name"}},{"kind":"Field","name":{"kind":"Name","value":"description"}},{"kind":"Field","name":{"kind":"Name","value":"difficulty"}},{"kind":"Field","name":{"kind":"Name","value":"minParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"maxParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"currentParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"outcome"}}]}}]}}]} as unknown as DocumentNode<AddExpeditionMutation, AddExpeditionMutationVariables>;
export const JoinExpeditionDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"mutation","name":{"kind":"Name","value":"JoinExpedition"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"input"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"JoinExpeditionInput"}}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"joinExpedition"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"input"},"value":{"kind":"Variable","name":{"kind":"Name","value":"input"}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"name"}},{"kind":"Field","name":{"kind":"Name","value":"description"}},{"kind":"Field","name":{"kind":"Name","value":"difficulty"}},{"kind":"Field","name":{"kind":"Name","value":"minParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"maxParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"currentParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"outcome"}}]}}]}}]} as unknown as DocumentNode<JoinExpeditionMutation, JoinExpeditionMutationVariables>;
export const GetExpeditionsDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"query","name":{"kind":"Name","value":"GetExpeditions"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"page"}},"type":{"kind":"NamedType","name":{"kind":"Name","value":"Int"}},"defaultValue":{"kind":"IntValue","value":"1"}},{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"pageSize"}},"type":{"kind":"NamedType","name":{"kind":"Name","value":"Int"}},"defaultValue":{"kind":"IntValue","value":"20"}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"expeditions"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"page"},"value":{"kind":"Variable","name":{"kind":"Name","value":"page"}}},{"kind":"Argument","name":{"kind":"Name","value":"pageSize"},"value":{"kind":"Variable","name":{"kind":"Name","value":"pageSize"}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"items"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"name"}},{"kind":"Field","name":{"kind":"Name","value":"description"}},{"kind":"Field","name":{"kind":"Name","value":"difficulty"}},{"kind":"Field","name":{"kind":"Name","value":"minParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"maxParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"currentParticipants"}},{"kind":"Field","name":{"kind":"Name","value":"outcome"}}]}},{"kind":"Field","name":{"kind":"Name","value":"totalCount"}},{"kind":"Field","name":{"kind":"Name","value":"page"}},{"kind":"Field","name":{"kind":"Name","value":"pageSize"}},{"kind":"Field","name":{"kind":"Name","value":"totalPages"}}]}}]}}]} as unknown as DocumentNode<GetExpeditionsQuery, GetExpeditionsQueryVariables>;