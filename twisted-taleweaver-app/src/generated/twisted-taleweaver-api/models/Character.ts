/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AttributeScores } from './AttributeScores';
import type { ClassType } from './ClassType';
import type { CreatureStatus } from './CreatureStatus';
import type { CreatureType } from './CreatureType';
import type { Gender } from './Gender';
import type { NarrativeAlignment } from './NarrativeAlignment';
import type { SpeedScores } from './SpeedScores';
import type { Trait } from './Trait';
export type Character = {
    characterId?: string;
    class: ClassType;
    maxHealth: number;
    currentHealth: number;
    armorClass: number;
    speedScores: SpeedScores;
    name: string;
    status: CreatureStatus;
    attributeScores: AttributeScores;
    contextKeywords: string;
    gender: Gender;
    creatureType: CreatureType;
    alignment: NarrativeAlignment;
    traits: Array<Trait>;
};

