import { UserPermissionDTO } from "../user-permission/user-permission";

export class ModuleDTO {
    description: string;
    name: string;
    order: number;
    permissions: UserPermissionDTO[];
}