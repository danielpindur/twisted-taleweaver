import type { ValidationProblemDetails } from "@/generated/twisted-taleweaver-api/models/ValidationProblemDetails";

export const normalizeValidationErrors = (details: ValidationProblemDetails): string => {
  if (!details || !details.errors) {
    return "Unexpected validation error.";
  }

  let errorText = "";

  for (const [_, messages] of Object.entries(details.errors)) {
    messages.forEach(message => {
      if (errorText.length > 0){
          errorText += " ";
      } 

      errorText += message;
    });
  }

  return errorText;
}
