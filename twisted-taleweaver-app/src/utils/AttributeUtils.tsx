export const formatModifier = (modifier: number) => {
  return modifier >= 0 ? `+${modifier}` : `${modifier}`;
};

export const getAttributeModifier = (score: number) => {
  return Math.floor((score - 10) / 2);
};