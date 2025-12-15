/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ExpeditionDifficulty } from './ExpeditionDifficulty';
export type ExpeditionRequest = {
    name?: string | null;
    description?: string | null;
    difficulty: ExpeditionDifficulty;
    minParticipants: number;
    maxParticipants: number;
};

