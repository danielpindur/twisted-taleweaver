/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ChatMessage } from './ChatMessage';
import type { ExpeditionDifficulty } from './ExpeditionDifficulty';
import type { ExpeditionOutcome } from './ExpeditionOutcome';
import type { ExpeditionParticipant } from './ExpeditionParticipant';
import type { ExpeditionState } from './ExpeditionState';
import type { User } from './User';
export type Expedition = {
    expeditionId: string;
    name: string;
    description: string;
    difficulty: ExpeditionDifficulty;
    minParticipants: number;
    maxParticipants: number;
    currentParticipants: number;
    outcome?: (null | ExpeditionOutcome);
    createdByUser: User;
    participants: Array<ExpeditionParticipant>;
    state: ExpeditionState;
    messages: Array<ChatMessage>;
};

