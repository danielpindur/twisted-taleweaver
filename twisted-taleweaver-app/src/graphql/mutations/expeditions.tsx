import { gql } from "@/__generated__/twisted-taleweaver-api/gql";

export const AddExpedition = gql(`
  mutation AddExpedition($input: AddExpeditionInput!) {
    addExpedition(input: $input) {
      id
    }
  }
`);

export const JoinExpedition = gql(`
  mutation JoinExpedition($input: JoinExpeditionInput!) {
    joinExpedition(input: $input) {
      id
    }
  }
`);