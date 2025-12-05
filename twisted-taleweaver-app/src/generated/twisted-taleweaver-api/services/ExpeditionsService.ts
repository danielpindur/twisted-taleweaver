/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Expedition } from '../models/Expedition';
import type { ExpeditionRequest } from '../models/ExpeditionRequest';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class ExpeditionsService {
    /**
     * @param requestBody
     * @returns Expedition OK
     * @throws ApiError
     */
    public static postApiExpeditions(
        requestBody: ExpeditionRequest,
    ): CancelablePromise<Expedition> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/expeditions',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param expeditionId
     * @returns Expedition OK
     * @throws ApiError
     */
    public static getApiExpeditions(
        expeditionId: string,
    ): CancelablePromise<Expedition> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/expeditions/{expeditionId}',
            path: {
                'expeditionId': expeditionId,
            },
        });
    }
}
