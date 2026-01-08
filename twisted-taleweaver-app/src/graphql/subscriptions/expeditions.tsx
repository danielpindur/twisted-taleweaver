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
      activeCreature {
        id
        name
      }
      creatureStates {
        creatureId
        status
        currentHealth
      }
      monsters {
        id
        name
        description
        gender
        creatureType
        alignment
        challengeRating
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
`);
