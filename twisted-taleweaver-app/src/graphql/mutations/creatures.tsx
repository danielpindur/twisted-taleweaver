import { gql } from "@/__generated__/twisted-taleweaver-api/gql";

export const AddCharacter = gql(`
  mutation AddCharacter($input: AddCharacterInput!) {
    addCharacter(input: $input) {
      id
    }
  }
`);