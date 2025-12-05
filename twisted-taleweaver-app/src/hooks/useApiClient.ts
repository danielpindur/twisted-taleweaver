import { useAuth } from "@clerk/clerk-react";
import { useCallback } from "react";
import { OpenAPI } from "@/generated/twisted-taleweaver-api/core/OpenAPI";

export const useApiClient = () => {
  const { getToken } = useAuth();

  const requestWithAuth = useCallback(
    async <T>(fn: () => Promise<T>): Promise<T> => {
      const token = await getToken();
      
      if (!token){
        throw new Error("No authentication token available");
      }
      
      OpenAPI.TOKEN = token;

      return fn();
    },
    [getToken]
  );

  return { requestWithAuth };
};