import { AuditableResponse } from "../abstractions/auditable-response";

export class UserPermissionListingReponse extends AuditableResponse {
    id: string;
    name: string;
}