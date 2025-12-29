import { useAuth } from "@clerk/clerk-react";
import { useCallback } from "react";
import { GraphQLClient } from "graphql-request";
import { getSdk } from "@/__generated__/twisted-taleweaver-api/types";

export const useGraphQlClient = () => {
  const { getToken } = useAuth();

  const createClient = useCallback(
    async (): Promise<ReturnType<typeof getSdk>> => {
      const token = await getToken();
      if (!token) throw new Error("No authentication token available");

      const client = new GraphQLClient("http://localhost:8081/graphql", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      return getSdk(client);
    },
    [getToken]
  );

  const requestWithAuth = useCallback(
    async <T>(fn: (sdk: ReturnType<typeof getSdk>) => Promise<T>): Promise<T> => {
      const sdk = await createClient();
      return fn(sdk);
    },
    [createClient]
  );

  return { requestWithAuth };
};