export interface Message {
    platform: string;
    textToSpeech: string;
    type: any;
    speech: string;
}

export interface Fulfillment {
    messages: Message[];
    source: string;
    speech: string;
}

export interface Metadata {
    intentId: string;
    intentName: string;
    webhookForSlotFillingUsed: string;
    webhookUsed: string;
}

export interface Parameters {
    fruit: string[];
}

export interface Result {
    action: string;
    actionIncomplete: boolean;
    contexts: string[];
    fulfillment: Fulfillment;
    metadata: Metadata;
    parameters: Parameters;
    resolvedQuery: string;
    score: number;
    source: string;
}

export interface Status {
    code: number;
    errorType: string;
}

export interface DialogResponse {
    id: string;
    lang: string;
    result: Result;
    sessionId: string;
    status: Status;
    timestamp: Date;
}
