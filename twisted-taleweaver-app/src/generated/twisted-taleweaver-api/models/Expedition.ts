/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ExpeditionDifficulty } from './ExpeditionDifficulty';
import type { ExpeditionOutcome } from './ExpeditionOutcome';
import type { ExpeditionParticipant } from './ExpeditionParticipant';
import type { ExpeditionStatus } from './ExpeditionStatus';
export type Expedition = {
    expeditionId: string;
    name: string;
    description: string;
    status: ExpeditionStatus;
    difficulty: ExpeditionDifficulty;
    maxParticipants: number | string;
    outcome?: (null | ExpeditionOutcome);
    participants: Array<ExpeditionParticipant>;
    createdAt: string;
    updatedAt: string;
    startedAt?: string | null;
    completedAt?: string | null;
};

