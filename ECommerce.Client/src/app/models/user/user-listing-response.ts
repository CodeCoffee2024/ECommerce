import { AuditableResponse } from "../abstractions/auditable-response";

export class UserListingReponse extends AuditableResponse {
    id: string;
    lastName: string;
    firstName: string;
    middleName: string;
    permissions: string;
}