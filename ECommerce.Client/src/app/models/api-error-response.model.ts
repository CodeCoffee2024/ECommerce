import { ApiError } from "./api-error.model";

export interface ApiErrorResponse {
    data: unknown; // Can be replaced with a more specific type if needed
    isSuccess: boolean;
    error?: ApiError;
    errors?: ApiError[];
}