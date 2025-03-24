export class GenericListingResult<T> {
    result: T | null;
    page: number;
    pageSize: number;
    totalEntries: number;
    totalPages: number;
    totalRecords: number;
}