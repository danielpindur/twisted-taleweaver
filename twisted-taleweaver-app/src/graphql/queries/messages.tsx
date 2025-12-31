import { gql } from "@/__generated__/twisted-taleweaver-api/gql";

export const MessagesQuery = gql(`
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
`);