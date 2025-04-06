import { UserPermission } from "./user/user";
import { UserPermissionPermission } from "./user-permission/user-permission";
import { UnitOfMeasurementTypePermission } from "./settings/unit-of-measurement-type/unit-of-measurement-type";

export class SideBarNavigation {
    name: string;
    description: string;
    icon: string;
    route: string;
    module: string;
    enabled: boolean;
    constructor(name: string, description: string, enabled: boolean, icon: string, module: string, route: string) {
        this.name = name;
        this.module = module;
        this.icon = icon;
        this.description = description;
        this.route = route;
        this.enabled = enabled;
    }
}
export const Modules = [
    {
        name: "Dashboard",
        order: 1
    },
    {
        name: "User Management",
        order: 5
    },
    {
        name: "Settings",
        order: 6
    }
].sort((a, b) => a.order - b.order); // Sorting modules by 'order'
export const Navigations = [
    {
        module: Modules.find(it => it.name == "Dashboard").name,
        name: "Dashboard",
        icon: "bi bi-speedometer",
        route: '/admin/dashboard',
        description: "Dashboard",
    },
    {
        module: Modules.find(it => it.name == "User Management").name,
        name: "User",
        icon: "bi bi-people",
        route: '/admin/users',
        requiredPermission: [UserPermission.UserEnableToViewUser],
        description: "Users",
    },
    {
        module: Modules.find(it => it.name == "User Management").name,
        icon: "bi bi-lock",
        name: "UserPermission",
        route: '/admin/user-permissions',
        requiredPermission: [UserPermissionPermission.UserEnableToViewUserPermission],
        description: "User Permissions",
    },
    {
        module: Modules.find(it => it.name == "Settings").name,
        icon: "bi bi-list-check",
        name: "UnitOfMeasurementType",
        route: '/admin/settings/unit-of-measurement-types',
        requiredPermission: [UnitOfMeasurementTypePermission.UserEnableToViewUnitOfMeasurementType],
        description: "UOM Types",
    }
];