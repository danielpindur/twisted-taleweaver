import { gql } from "@/__generated__/twisted-taleweaver-api/gql";

export const ExpeditionsQuery = gql(`
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
`);
