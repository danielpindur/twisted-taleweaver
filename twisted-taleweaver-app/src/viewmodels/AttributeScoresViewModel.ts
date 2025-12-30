import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";

export type AttributeScoresViewModel = Omit<
  NonNullable<
    NonNullable<
      ExpeditionViewModel["participants"][number]["character"]
    >["attributeScores"]
  >,
  "__typename"
>;

export const ATTRIBUTE_KEYS = [
  "strength",
  "dexterity",
  "constitution",
  "intelligence",
  "wisdom",
  "charisma",
] as const satisfies readonly (keyof AttributeScoresViewModel)[];

