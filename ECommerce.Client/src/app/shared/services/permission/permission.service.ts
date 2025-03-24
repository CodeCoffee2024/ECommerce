import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PermissionService {

  constructor() { }
  storePermission(permission: string) {
    localStorage.setItem('permission', permission);
  }
  private get permissions() {
    const userPermissions = localStorage.getItem("permission");
    return userPermissions.split(',');
  }
  hasPermissions(permissions: string[]): boolean {
    if (!permissions || permissions.length === 0) {
        return false;
    }

    // Allow access if the first permission is "authenticated" and user has an access token
    if (permissions.includes('authenticated')) {
        return localStorage.getItem("accessToken") !== null;
    }

    // Check if permissions exist in localStorage
    const storedPermissions = localStorage.getItem("permission");
    if (!storedPermissions) {
        return false;
    }

    // Convert stored permissions to an array
    const userPermissions = storedPermissions.split(',');
    
    // Check if the user has at least one required permission
    return permissions.some(permission => userPermissions.find(it => it == permission));
}
}
