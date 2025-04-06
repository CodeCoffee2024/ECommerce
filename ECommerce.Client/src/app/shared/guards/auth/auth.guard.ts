import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { PermissionService } from '../../services/permission/permission.service';
import { ToastService } from '../../services/toast/toast.service';
import { ToastType } from '../../../models/toast';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
  constructor(private permissionService: PermissionService, private router: Router, private toastService: ToastService) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const optionalPermission = route.data['optionalPermission']?.split(',');
    let exists = false;
    if (optionalPermission && optionalPermission.length > 0) {
      optionalPermission.forEach(it => {
        console.log(it);
        if (this.permissionService.hasPermissions([it])) {
          exists = true;
        }
      });
    }
    const requiredPermission = route.data['permission']?.split(',');
    if (!requiredPermission && exists) {
      return exists;
    }
    if (!requiredPermission || this.permissionService.hasPermissions(requiredPermission)) {
      return true;
    }

    if (!this.permissionService.hasPermissions(requiredPermission) && requiredPermission == 'authenticated') {
      this.toastService.add("Error 401", "Unauthorized Access", ToastType.ERROR);
      this.router.navigate(['/']);
      return false;
    }
    this.router.navigate(['/admin/401']);
    return false;
  }
}
