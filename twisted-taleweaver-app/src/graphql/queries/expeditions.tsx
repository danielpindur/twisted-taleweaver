import { gql } from "@/__generated__/twisted-taleweaver-api/gql";

export const ExpeditionsQuery = gql(`
  query GetExpeditions($page: Int = 1, $pageSize: Int = 20) {
    expeditions(page: $page, pageSize: $pageSize) {
      items {
        id
        name
        description
        difficulty
        minParticipants
        maxParticipants
        currentParticipants
        outcome
      }
      totalCount
      page
      pageSize
      totalPages
    }
  }
`);