import { UserFragment } from "../user";

export abstract class AuditableResponse {
    createdBy: UserFragment;
    createdDate: Date;
    modifiedBy: UserFragment;
    modifiedDateL: Date;
}