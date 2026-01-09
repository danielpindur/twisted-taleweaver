import { gql } from "@/__generated__/twisted-taleweaver-api/gql";

export const ExpeditionsQuery = gql(`
  query GetExpeditions($input: ExpeditionSearchInput!) {
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

export const ArchivedExpeditionsQuery = gql(`
  query GetArchivedExpeditions($input: ExpeditionSearchInput!) {
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
        createdAt
        startedAt
        completedAt
        state {
          status
          creatureStates {
            creatureId
            currentHealth
          }
        }
        participants {
          character {
            id
          }
        }
      }
      statistics {
        totalExpeditions
        successfulExpeditions
        failedExpeditions
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

export const ArchivedExpeditionByIdQuery = gql(`
  query GetArchivedExpeditionById($id: UUID!) {
    expeditionById(id: $id) {
      id
      name
      description
      difficulty
      maxParticipants
      currentParticipants
      outcome
      lessonLearned
      startedAt
      completedAt
      state {
        creatureStates {
          creatureId
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
        }
      }
      participants {
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
        }
      }
      narrativeMessages {
        id
        content
        timestamp
      }
    }
  }
`);
