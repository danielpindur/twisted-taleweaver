/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ChatMessage } from '../models/ChatMessage';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class MessagesService {
    /**
     * @param expeditionId
     * @param lastExpeditionMessageId
     * @param pageSize
     * @returns ChatMessage OK
     * @throws ApiError
     */
    public static getApiV1Messages(
        expeditionId?: string,
        lastExpeditionMessageId?: number,
        pageSize?: number,
    ): CancelablePromise<Array<ChatMessage>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/messages',
            query: {
                'ExpeditionId': expeditionId,
                'LastExpeditionMessageId': lastExpeditionMessageId,
                'PageSize': pageSize,
            },
            errors: {
                403: `Forbidden`,
            },
        });
    }
}
