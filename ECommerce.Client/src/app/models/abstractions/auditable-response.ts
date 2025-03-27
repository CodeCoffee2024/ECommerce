import { UserFragment } from "../user/user";

export abstract class AuditableResponse {
    createdBy: UserFragment;
    createdDate: Date;
    modifiedBy: UserFragment;
    modifiedDate: Date;
}