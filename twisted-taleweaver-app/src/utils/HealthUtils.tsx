export const getHealthPercentage = (health: number, maxHealth: number) => {
    return (health / maxHealth) * 100;
  };

export const getHealthColor = (percentage: number) => {
  if (percentage > 75) return "bg-health";
  if (percentage > 50) return "bg-warning";
  if (percentage > 25) return "bg-accent";
  return "bg-destructive";
};