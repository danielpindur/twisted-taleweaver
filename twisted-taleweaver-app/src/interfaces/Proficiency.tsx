export interface Proficiency {
  type: 'Skill' | 'Weapon' | 'SavingThrow' | 'Tool' | 'Armor' | 'Other';
  target: string;
};