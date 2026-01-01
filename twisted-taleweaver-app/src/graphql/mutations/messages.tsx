import { gql } from "@/__generated__/twisted-taleweaver-api/gql";

export const AddMessage = gql(`
  mutation AddMessage($input: AddMessageInput!) {
    addMessage(input: $input) {
      id
    }
  }
`);