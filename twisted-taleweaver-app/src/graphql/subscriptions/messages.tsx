import { gql } from "@/__generated__/twisted-taleweaver-api/gql";

export const OnMessageCreatedSubscription = gql(`
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
`);
