import { gql } from "@/__generated__/twisted-taleweaver-api/gql";

export const OnParticipantsChangedSubscription = gql(`
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

export const OnExpeditionStateChangedSubscription = gql(`
  subscription OnExpeditionStateChanged($expeditionId: UUID!) {
    expeditionStateChanged(expeditionId: $expeditionId) {
      status
      activeUser {
        id
        externalId
        name
      }
      characterStates {
        creatureId
        status
        currentHealth
      }
    }
  }
`);
