/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Expedition } from '../models/Expedition';
import type { ExpeditionRequest } from '../models/ExpeditionRequest';
import type { ExpeditionSlim } from '../models/ExpeditionSlim';
import type { PaginatedResponseOfExpeditionSlim } from '../models/PaginatedResponseOfExpeditionSlim';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class ExpeditionsService {
    /**
     * @param requestBody
     * @returns ExpeditionSlim Created
     * @throws ApiError
     */
    public static postApiExpeditions(
        requestBody: ExpeditionRequest,
    ): CancelablePromise<ExpeditionSlim> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/expeditions',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `Bad Request`,
                403: `Forbidden`,
            },
        });
    }
    /**
     * @param pageNumber
     * @param pageSize
     * @returns PaginatedResponseOfExpeditionSlim OK
     * @throws ApiError
     */
    public static getApiExpeditions(
        pageNumber?: number,
        pageSize?: number,
    ): CancelablePromise<PaginatedResponseOfExpeditionSlim> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/expeditions',
            query: {
                'PageNumber': pageNumber,
                'PageSize': pageSize,
            },
            errors: {
                403: `Forbidden`,
            },
        });
    }
    /**
     * @param expeditionId
     * @returns Expedition OK
     * @throws ApiError
     */
    public static getApiExpeditions1(
        expeditionId: string,
    ): CancelablePromise<Expedition> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/expeditions/{expeditionId}',
            path: {
                'expeditionId': expeditionId,
            },
            errors: {
                403: `Forbidden`,
                404: `Not Found`,
            },
        });
    }
    /**
     * @param expeditionId
     * @returns void
     * @throws ApiError
     */
    public static postApiExpeditionsJoin(
        expeditionId: string,
    ): CancelablePromise<void> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/expeditions/{expeditionId}/join',
            path: {
                'expeditionId': expeditionId,
            },
            errors: {
                400: `Bad Request`,
                403: `Forbidden`,
            },
        });
    }
    /**
     * @param expeditionId
     * @returns void
     * @throws ApiError
     */
    public static postApiExpeditionsBegin(
        expeditionId: string,
    ): CancelablePromise<void> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/expeditions/{expeditionId}/begin',
            path: {
                'expeditionId': expeditionId,
            },
            errors: {
                400: `Bad Request`,
                403: `Forbidden`,
            },
        });
    }
}
