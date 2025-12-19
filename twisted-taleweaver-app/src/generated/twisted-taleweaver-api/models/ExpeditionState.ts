/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CombatCreatureState } from './CombatCreatureState';
import type { ExpeditionStatus } from './ExpeditionStatus';
import type { User } from './User';
export type ExpeditionState = {
    status: ExpeditionStatus;
    activeUser?: (null | User);
    participants: Array<CombatCreatureState>;
};

