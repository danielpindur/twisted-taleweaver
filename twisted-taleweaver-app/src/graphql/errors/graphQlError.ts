export enum ErrorCode {
  ValidationError = "VALIDATION_ERROR",
}

export interface GraphQLErrorExtensions {
  code?: string;
  [key: string]: any;
}

export interface ValidationGraphQLErrorExtensions extends GraphQLErrorExtensions {
  code: ErrorCode.ValidationError;
  errors: ValidationErrorDetail[];
}

export interface ValidationErrorDetail {
  property: string;
  message: string;
  errorCode?: string;
}

export interface GraphQLErrorDto<TExtensions = GraphQLErrorExtensions> {
  message: string;
  locations?: Array<{ line: number; column: number }>;
  path?: Array<string | number>;
  extensions?: TExtensions;
}