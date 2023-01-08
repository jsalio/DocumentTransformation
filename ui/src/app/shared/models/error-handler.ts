import { HttpErrorResponse } from "@angular/common/http";


export const buildErrorResponse = (error: HttpErrorResponse) => {
    return error.error as ErrorHandlerResponse;
}

export interface ErrorHandlerResponse {
    message?: string;
    source?: string;
    stacktrace?: string;
}