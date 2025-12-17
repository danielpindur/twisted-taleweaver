/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AttributeScores } from './AttributeScores';
import type { ClassType } from './ClassType';
import type { CreatureType } from './CreatureType';
import type { Gender } from './Gender';
import type { NarrativeAlignment } from './NarrativeAlignment';
export type CharacterRequest = {
    expeditionId: string;
    name?: string | null;
    description?: string | null;
    class: ClassType;
    gender: Gender;
    creatureType: CreatureType;
    alignment: NarrativeAlignment;
    attributeScores: AttributeScores;
};

