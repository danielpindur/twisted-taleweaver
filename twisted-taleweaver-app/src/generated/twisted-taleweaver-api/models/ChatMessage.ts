/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { MessageSender } from './MessageSender';
import type { MessageType } from './MessageType';
export type ChatMessage = {
    expeditionMessageId: number;
    sender: (null | MessageSender);
    type: MessageType;
    content: string;
    timestamp: string;
};

