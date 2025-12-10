/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ExpeditionDifficulty } from './ExpeditionDifficulty';
import type { ExpeditionOutcome } from './ExpeditionOutcome';
import type { ExpeditionStatus } from './ExpeditionStatus';
export type ExpeditionSlim = {
    expeditionId: string;
    name: string;
    description: string;
    status: ExpeditionStatus;
    difficulty: ExpeditionDifficulty;
    maxParticipants: number;
    currentParticipants: number;
    outcome?: (null | ExpeditionOutcome);
};

