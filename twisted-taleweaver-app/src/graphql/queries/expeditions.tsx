import { gql } from "@/__generated__/twisted-taleweaver-api/gql";

export const ExpeditionsQuery = gql(`
  query GetExpeditions($input: OffsetInput!) {
    expeditions(input: $input) {
      items {
        id
        name
        description
        difficulty
        minParticipants
        maxParticipants
        currentParticipants
        outcome
        state {
          status
        }
      }
      totalCount
      page
      pageSize
      totalPages
    }
  }
`);

export const ExpeditionByIdQuery = gql(`
  query GetExpeditionById($id: UUID!) {
    expeditionById(id: $id) {
      id
      name
      description
      difficulty
      minParticipants
      maxParticipants
      currentParticipants
      outcome
      userCreated {
        id
        externalId
        name
      }
      state {
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
      participants {
        user {
          id
          externalId
          name
        }
        character {
          id
          name
          description
          gender
          creatureType
          alignment
          class
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
  }
`);