/* eslint-disable */
import * as types from './graphql';
import type { TypedDocumentNode as DocumentNode } from '@graphql-typed-document-node/core';

/**
 * Map of all GraphQL operations in the project.
 *
 * This map has several performance disadvantages:
 * 1. It is not tree-shakeable, so it will include all operations in the project.
 * 2. It is not minifiable, so the string of a GraphQL query will be multiple times inside the bundle.
 * 3. It does not support dead code elimination, so it will add unused operations.
 *
 * Therefore it is highly recommended to use the babel or swc plugin for production.
 * Learn more about it here: https://the-guild.dev/graphql/codegen/plugins/presets/preset-client#reducing-bundle-size
 */
type Documents = {
    "\n  mutation AddCharacter($input: AddCharacterInput!) {\n    addCharacter(input: $input) {\n      id\n    }\n  }\n": typeof types.AddCharacterDocument,
    "\n  mutation AddExpedition($input: AddExpeditionInput!) {\n    addExpedition(input: $input) {\n      id\n    }\n  }\n": typeof types.AddExpeditionDocument,
    "\n  mutation JoinExpedition($input: JoinExpeditionInput!) {\n    joinExpedition(input: $input) {\n      id\n    }\n  }\n": typeof types.JoinExpeditionDocument,
    "\n  mutation StartExpedition($input: StartExpeditionInput!) {\n    startExpedition(input: $input) {\n      id\n    }\n  }\n": typeof types.StartExpeditionDocument,
    "\n  query GetExpeditions($input: OffsetInput!) {\n    expeditions(input: $input) {\n      items {\n        id\n        name\n        description\n        difficulty\n        minParticipants\n        maxParticipants\n        currentParticipants\n        outcome\n        state {\n          status\n        }\n      }\n      totalCount\n      page\n      pageSize\n      totalPages\n    }\n  }\n": typeof types.GetExpeditionsDocument,
    "\n  query GetExpeditionById($id: UUID!) {\n    expeditionById(id: $id) {\n      id\n      name\n      description\n      difficulty\n      minParticipants\n      maxParticipants\n      currentParticipants\n      outcome\n      userCreated {\n        id\n        externalId\n        name\n      }\n      state {\n        status\n        activeUser {\n          id\n          externalId\n        }\n        characterStates {\n          creatureId\n          status\n          currentHealth\n        }\n      }\n      participants {\n        user {\n          id\n          externalId\n          name\n        }\n        character {\n          id\n          name\n          gender\n          creatureType\n          alignment\n          class\n          maxHealth\n          armorClass\n          speedScores {\n            walking\n            swimming\n            flying\n            climbing\n            burrowing\n            teleportation\n          }\n          attributeScores {\n            strength\n            wisdom\n            intelligence\n            dexterity\n            constitution\n            charisma\n          }\n          traits {\n            name\n            effect\n            conditionForPerform\n          }\n        }\n      }\n    }\n  }\n": typeof types.GetExpeditionByIdDocument,
    "\n  subscription OnParticipantsChanged($expeditionId: UUID!) {\n    participantsUpdated(expeditionId: $expeditionId) {\n      id\n      participants {\n        user {\n            id\n        }\n        character {\n            id\n        }\n      }\n    }\n  }\n": typeof types.OnParticipantsChangedDocument,
    "\n  subscription OnExpeditionStateChanged($expeditionId: UUID!) {\n    expeditionStateChanged(expeditionId: $expeditionId) {\n      id,\n      state {\n        status\n        activeUser {\n          id\n          externalId\n          name\n        }\n        characterStates {\n          creatureId\n          status\n          currentHealth\n        }\n      }\n    }\n  }\n": typeof types.OnExpeditionStateChangedDocument,
};
const documents: Documents = {
    "\n  mutation AddCharacter($input: AddCharacterInput!) {\n    addCharacter(input: $input) {\n      id\n    }\n  }\n": types.AddCharacterDocument,
    "\n  mutation AddExpedition($input: AddExpeditionInput!) {\n    addExpedition(input: $input) {\n      id\n    }\n  }\n": types.AddExpeditionDocument,
    "\n  mutation JoinExpedition($input: JoinExpeditionInput!) {\n    joinExpedition(input: $input) {\n      id\n    }\n  }\n": types.JoinExpeditionDocument,
    "\n  mutation StartExpedition($input: StartExpeditionInput!) {\n    startExpedition(input: $input) {\n      id\n    }\n  }\n": types.StartExpeditionDocument,
    "\n  query GetExpeditions($input: OffsetInput!) {\n    expeditions(input: $input) {\n      items {\n        id\n        name\n        description\n        difficulty\n        minParticipants\n        maxParticipants\n        currentParticipants\n        outcome\n        state {\n          status\n        }\n      }\n      totalCount\n      page\n      pageSize\n      totalPages\n    }\n  }\n": types.GetExpeditionsDocument,
    "\n  query GetExpeditionById($id: UUID!) {\n    expeditionById(id: $id) {\n      id\n      name\n      description\n      difficulty\n      minParticipants\n      maxParticipants\n      currentParticipants\n      outcome\n      userCreated {\n        id\n        externalId\n        name\n      }\n      state {\n        status\n        activeUser {\n          id\n          externalId\n        }\n        characterStates {\n          creatureId\n          status\n          currentHealth\n        }\n      }\n      participants {\n        user {\n          id\n          externalId\n          name\n        }\n        character {\n          id\n          name\n          gender\n          creatureType\n          alignment\n          class\n          maxHealth\n          armorClass\n          speedScores {\n            walking\n            swimming\n            flying\n            climbing\n            burrowing\n            teleportation\n          }\n          attributeScores {\n            strength\n            wisdom\n            intelligence\n            dexterity\n            constitution\n            charisma\n          }\n          traits {\n            name\n            effect\n            conditionForPerform\n          }\n        }\n      }\n    }\n  }\n": types.GetExpeditionByIdDocument,
    "\n  subscription OnParticipantsChanged($expeditionId: UUID!) {\n    participantsUpdated(expeditionId: $expeditionId) {\n      id\n      participants {\n        user {\n            id\n        }\n        character {\n            id\n        }\n      }\n    }\n  }\n": types.OnParticipantsChangedDocument,
    "\n  subscription OnExpeditionStateChanged($expeditionId: UUID!) {\n    expeditionStateChanged(expeditionId: $expeditionId) {\n      id,\n      state {\n        status\n        activeUser {\n          id\n          externalId\n          name\n        }\n        characterStates {\n          creatureId\n          status\n          currentHealth\n        }\n      }\n    }\n  }\n": types.OnExpeditionStateChangedDocument,
};

/**
 * The gql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 *
 *
 * @example
 * ```ts
 * const query = gql(`query GetUser($id: ID!) { user(id: $id) { name } }`);
 * ```
 *
 * The query argument is unknown!
 * Please regenerate the types.
 */
export function gql(source: string): unknown;

/**
 * The gql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function gql(source: "\n  mutation AddCharacter($input: AddCharacterInput!) {\n    addCharacter(input: $input) {\n      id\n    }\n  }\n"): (typeof documents)["\n  mutation AddCharacter($input: AddCharacterInput!) {\n    addCharacter(input: $input) {\n      id\n    }\n  }\n"];
/**
 * The gql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function gql(source: "\n  mutation AddExpedition($input: AddExpeditionInput!) {\n    addExpedition(input: $input) {\n      id\n    }\n  }\n"): (typeof documents)["\n  mutation AddExpedition($input: AddExpeditionInput!) {\n    addExpedition(input: $input) {\n      id\n    }\n  }\n"];
/**
 * The gql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function gql(source: "\n  mutation JoinExpedition($input: JoinExpeditionInput!) {\n    joinExpedition(input: $input) {\n      id\n    }\n  }\n"): (typeof documents)["\n  mutation JoinExpedition($input: JoinExpeditionInput!) {\n    joinExpedition(input: $input) {\n      id\n    }\n  }\n"];
/**
 * The gql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function gql(source: "\n  mutation StartExpedition($input: StartExpeditionInput!) {\n    startExpedition(input: $input) {\n      id\n    }\n  }\n"): (typeof documents)["\n  mutation StartExpedition($input: StartExpeditionInput!) {\n    startExpedition(input: $input) {\n      id\n    }\n  }\n"];
/**
 * The gql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function gql(source: "\n  query GetExpeditions($input: OffsetInput!) {\n    expeditions(input: $input) {\n      items {\n        id\n        name\n        description\n        difficulty\n        minParticipants\n        maxParticipants\n        currentParticipants\n        outcome\n        state {\n          status\n        }\n      }\n      totalCount\n      page\n      pageSize\n      totalPages\n    }\n  }\n"): (typeof documents)["\n  query GetExpeditions($input: OffsetInput!) {\n    expeditions(input: $input) {\n      items {\n        id\n        name\n        description\n        difficulty\n        minParticipants\n        maxParticipants\n        currentParticipants\n        outcome\n        state {\n          status\n        }\n      }\n      totalCount\n      page\n      pageSize\n      totalPages\n    }\n  }\n"];
/**
 * The gql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function gql(source: "\n  query GetExpeditionById($id: UUID!) {\n    expeditionById(id: $id) {\n      id\n      name\n      description\n      difficulty\n      minParticipants\n      maxParticipants\n      currentParticipants\n      outcome\n      userCreated {\n        id\n        externalId\n        name\n      }\n      state {\n        status\n        activeUser {\n          id\n          externalId\n        }\n        characterStates {\n          creatureId\n          status\n          currentHealth\n        }\n      }\n      participants {\n        user {\n          id\n          externalId\n          name\n        }\n        character {\n          id\n          name\n          gender\n          creatureType\n          alignment\n          class\n          maxHealth\n          armorClass\n          speedScores {\n            walking\n            swimming\n            flying\n            climbing\n            burrowing\n            teleportation\n          }\n          attributeScores {\n            strength\n            wisdom\n            intelligence\n            dexterity\n            constitution\n            charisma\n          }\n          traits {\n            name\n            effect\n            conditionForPerform\n          }\n        }\n      }\n    }\n  }\n"): (typeof documents)["\n  query GetExpeditionById($id: UUID!) {\n    expeditionById(id: $id) {\n      id\n      name\n      description\n      difficulty\n      minParticipants\n      maxParticipants\n      currentParticipants\n      outcome\n      userCreated {\n        id\n        externalId\n        name\n      }\n      state {\n        status\n        activeUser {\n          id\n          externalId\n        }\n        characterStates {\n          creatureId\n          status\n          currentHealth\n        }\n      }\n      participants {\n        user {\n          id\n          externalId\n          name\n        }\n        character {\n          id\n          name\n          gender\n          creatureType\n          alignment\n          class\n          maxHealth\n          armorClass\n          speedScores {\n            walking\n            swimming\n            flying\n            climbing\n            burrowing\n            teleportation\n          }\n          attributeScores {\n            strength\n            wisdom\n            intelligence\n            dexterity\n            constitution\n            charisma\n          }\n          traits {\n            name\n            effect\n            conditionForPerform\n          }\n        }\n      }\n    }\n  }\n"];
/**
 * The gql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function gql(source: "\n  subscription OnParticipantsChanged($expeditionId: UUID!) {\n    participantsUpdated(expeditionId: $expeditionId) {\n      id\n      participants {\n        user {\n            id\n        }\n        character {\n            id\n        }\n      }\n    }\n  }\n"): (typeof documents)["\n  subscription OnParticipantsChanged($expeditionId: UUID!) {\n    participantsUpdated(expeditionId: $expeditionId) {\n      id\n      participants {\n        user {\n            id\n        }\n        character {\n            id\n        }\n      }\n    }\n  }\n"];
/**
 * The gql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function gql(source: "\n  subscription OnExpeditionStateChanged($expeditionId: UUID!) {\n    expeditionStateChanged(expeditionId: $expeditionId) {\n      id,\n      state {\n        status\n        activeUser {\n          id\n          externalId\n          name\n        }\n        characterStates {\n          creatureId\n          status\n          currentHealth\n        }\n      }\n    }\n  }\n"): (typeof documents)["\n  subscription OnExpeditionStateChanged($expeditionId: UUID!) {\n    expeditionStateChanged(expeditionId: $expeditionId) {\n      id,\n      state {\n        status\n        activeUser {\n          id\n          externalId\n          name\n        }\n        characterStates {\n          creatureId\n          status\n          currentHealth\n        }\n      }\n    }\n  }\n"];

export function gql(source: string) {
  return (documents as any)[source] ?? {};
}

export type DocumentType<TDocumentNode extends DocumentNode<any, any>> = TDocumentNode extends DocumentNode<  infer TType,  any>  ? TType  : never;