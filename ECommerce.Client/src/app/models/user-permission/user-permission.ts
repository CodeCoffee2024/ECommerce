import { ModuleDTO } from "../module/module";

export enum UserPermissionPermission {
    UserEnableToModifyUserPermission = "UserEnableToModifyUserPermission",
    UserEnableToViewUserPermission = "UserEnableToViewUserPermission",
    UserEnableToDeleteUserPermission = "UserEnableToDeleteUserPermission",
}

export class UserPermissionDTO {
    dependencies: string;
    description: string;
    name: string;
    permission: string;
}
export class UserPermissionResult {
    id: string;
    name: string;
    permissions: string;
    modulePermissions: ModuleDTO[];
}