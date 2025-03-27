import { UserPermissionFragment } from "../user-permission/user-permission";

export enum UserPermission {
    UserEnableToModifyUser = "UserEnableToModifyUser",
    UserEnableToViewUser = "UserEnableToViewUser",
    UserEnableToDeleteUser = "UserEnableToDeleteUser",
}
export class UserFragment {
    firstName: string;
    middleName: string;
    lastName: string;
    id: string;
    get fullName() {
        if (this.middleName) {
            return this.firstName+" "+this.middleName+" "+this.lastName;
        }
        return this.firstName+" "+this.lastName;
    }
}
export class UserResult {
    id: string;
    lastName: string;
    firstName: string;
    middleName: string;
    userName: string;
    email: string;
    canUpdate: boolean;
    canDelete: boolean;
    birthDate: Date;
    permissions: UserPermissionFragment[];
}