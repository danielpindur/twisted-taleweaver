import { gql } from "@/__generated__/twisted-taleweaver-api/gql";

export const AddExpedition = gql(`
  mutation AddExpedition($input: AddExpeditionInput!) {
    addExpedition(input: $input) {
      id
      name
      description
      difficulty
      minParticipants
      maxParticipants
      currentParticipants
      outcome
    }
  }
`);

export const JoinExpedition = gql(`
  mutation JoinExpedition($input: JoinExpeditionInput!) {
    joinExpedition(input: $input) {
      id
      name
      description
      difficulty
      minParticipants
      maxParticipants
      currentParticipants
      outcome
    }
  }
`);