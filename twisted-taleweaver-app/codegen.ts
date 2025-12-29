import type { CodegenConfig } from '@graphql-codegen/cli'
import { use } from 'react'
 
const config: CodegenConfig = {
  schema: './schema.graphql',
  documents: ['./src/**/*.tsx'],
  generates: {
    "./src/__generated__/twisted-taleweaver-api/": {
      preset: 'client',
      presetConfig: {
        gqlTagName: "gql"
      }
    },
    "./src/__generated__/twisted-taleweaver-api/types.ts": {
      plugins: ["typescript", "typescript-operations", "typescript-graphql-request"]
    },
  },
  config: {
    useTypeImports: true,
    //enumsAsTypes: true,
  },
}
 
export default config