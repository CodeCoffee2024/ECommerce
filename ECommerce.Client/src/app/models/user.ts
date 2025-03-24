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