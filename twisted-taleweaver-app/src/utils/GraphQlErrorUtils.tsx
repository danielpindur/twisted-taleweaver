import { ErrorCode } from "@/graphql/errors/graphQlError";
import type { GraphQLErrorDto, ValidationGraphQLErrorExtensions } from "@/graphql/errors/graphQlError";

export function isValidationGraphQLError(
  error: GraphQLErrorDto
): error is GraphQLErrorDto<ValidationGraphQLErrorExtensions> {
  return (
    error.extensions?.code === ErrorCode.ValidationError &&
    Array.isArray((error.extensions as any).errors)
  );
}

export function extractGraphQLErrors(error: unknown): GraphQLErrorDto[] {
  if (
    typeof error === "object" &&
    error !== null &&
    "response" in error &&
    typeof (error as any).response === "object" &&
    Array.isArray((error as any).response.errors)
  ) {
    return (error as any).response.errors;
  }

  return [];
}

export const normalizeValidationErrors = (validationErrors: ValidationGraphQLErrorExtensions): string => {
  if (!validationErrors || !validationErrors.errors) {
    return "Unexpected validation error.";
  }

  let errorText = "";

  for (const [_, error] of Object.entries(validationErrors.errors)) {
    if (errorText.length > 0){
        errorText += " ";
    } 

    errorText += error.message;
  }

  return errorText;
}
