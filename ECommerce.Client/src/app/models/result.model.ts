export class ApiResult<T> {
    statusCode: number;
    isSuccess: boolean;
    data: T | null; // Ensures type safety
    errors: []; // Assuming errors are an array of strings
    error: [];  // If it's a duplicate, you might want to remove one
}
export class NullApiResult {
    statusCode: number;
    isSuccess: boolean;
    errors: []; // Assuming errors are an array of strings
    error: [];  // If it's a duplicate, you might want to remove one
}