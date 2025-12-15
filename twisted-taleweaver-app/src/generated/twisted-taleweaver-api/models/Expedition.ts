/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ExpeditionDifficulty } from './ExpeditionDifficulty';
import type { ExpeditionOutcome } from './ExpeditionOutcome';
import type { ExpeditionParticipant } from './ExpeditionParticipant';
import type { ExpeditionStatus } from './ExpeditionStatus';
import type { User } from './User';
export type Expedition = {
    createdByUser: User;
    participants: Array<ExpeditionParticipant>;
    expeditionId: string;
    name: string;
    description: string;
    status: ExpeditionStatus;
    difficulty: ExpeditionDifficulty;
    minParticipants: number;
    maxParticipants: number;
    currentParticipants: number;
    outcome?: (null | ExpeditionOutcome);
};

