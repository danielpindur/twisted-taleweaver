/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Character } from '../models/Character';
import type { CharacterRequest } from '../models/CharacterRequest';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class CharactersService {
    /**
     * @param requestBody
     * @returns Character Created
     * @throws ApiError
     */
    public static postApiV1Characters(
        requestBody: CharacterRequest,
    ): CancelablePromise<Character> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/characters',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `Bad Request`,
                403: `Forbidden`,
            },
        });
    }
}
