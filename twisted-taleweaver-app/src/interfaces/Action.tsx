export interface Action {
  name: string;
  description: string;
  cost: number; // 1-3 (action economy)
  recharge?: number; // 0-6
  range?: number; // 0-120 feet
};