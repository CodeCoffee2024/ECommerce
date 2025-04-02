import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { GenericActivityLogResult } from '../../../models/generics/generic-activity-log-result';
import { Failed, NotFound } from '../../../models/message';
import { ToastType } from '../../../models/toast';
import { UserPermissionResult } from '../../../models/user-permission/user-permission';
import { ActivityLogService } from '../../../shared/services/ActivityLog/activity-log.service';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { ToastService } from '../../../shared/services/toast/toast.service';
import { UserPermissionService } from '../user-permission.service';
import { ModuleDTO } from '../../../models/module/module';

@Component({
  selector: 'app-user-permission-activity-log-show',
  templateUrl: './user-permission-activity-log-show.component.html',
  styleUrl: './user-permission-activity-log-show.component.scss'
})
export class UserPermissionActivityLogShowComponent implements OnInit {
  Id: string;
  primaryKey: string;
  userPermission: UserPermissionResult;
  activityLog: GenericActivityLogResult;
  constructor(
    private loadingService: LoadingService,
    private toastService: ToastService,
    private userPermissionService: UserPermissionService,
    private activityLogService: ActivityLogService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
  }
  ngOnInit(): void {
    this.loadingService.show();
    this.Id = this.activatedRoute.snapshot.paramMap.get('id') || null;
    this.primaryKey = this.activatedRoute.snapshot.paramMap.get('primaryKey') || null;
    if (!this.Id) {
      this.toastService.add("Error", NotFound("User Permission Log"), ToastType.ERROR);
      this.router.navigate(['/admin/user-permissions']);
    } else {
      this.activityLogService.getLog("UserPermission",this.primaryKey)
      .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
      .subscribe({
        next: (result) => {
          this.activityLog = result.data;
          this.userPermissionService.show(this.Id)
          .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
          .subscribe({
            next: (result) => {
              this.userPermission = result.data;
              this.loadingService.hide();
            },
            error: () => {
              this.toastService.add("Error", Failed("User Permission Log"), ToastType.ERROR);
              this.router.navigate(['/admin/user-permissions']);
            }
          });
        },
        error: () => {
          this.toastService.add("Error", Failed("User Permission Log"), ToastType.ERROR);
          this.router.navigate(['/admin/user-permissions']);
        }
      });
    }
  }
  get oldValues() {
    return this.activityLog.oldValues;
  }
  get newValues() {
    return this.activityLog.newValues;
  }
  getClass(key: string): string {
    const oldVal = this.oldValues?.[key];
    const newVal = this.newValues?.[key];
  
    if (!oldVal && newVal) return 'bg-success text-white'; // New value
    if (oldVal && newVal && oldVal !== newVal) return 'bg-warning text-white'; // Changed value
    if (oldVal && !newVal) return 'bg-danger text-white'; // Removed value
    return ''; // No change
  }
  displayChange(key) {
    const oldVal = this.oldValues?.[key];
    const newVal = this.newValues?.[key];
    if (!oldVal && newVal) return `<span class="rounded text-white bg-success pl-2 pr-2">${newVal}</span>`; // New value
    if (oldVal && newVal && oldVal !== newVal) return `<span class="rounded text-white bg-warning mr-2 pl-2 pr-2">${oldVal}</span><span class="rounded text-white bg-success pl-2 pr-2">${newVal}</span>`; // Changed value
    if (oldVal && !newVal) return `<span class="rounded text-white bg-danger pl-2 pr-2">${oldVal}</span> --`; // Removed value
    return `${newVal}`; // No change
  }
  get keys() {
    const oldKeys = this.oldValues ? Object.keys(this.oldValues) : [];
    const newKeys = this.newValues ? Object.keys(this.newValues) : [];
    return Array.from(new Set([...oldKeys, ...newKeys]));
  }
  
  get modules() {
    return this.userPermission.modulePermissions;
  }
  getPermissions(module: ModuleDTO) {
    return module.permissions;
  }
  isChecked(permission) {
    return this.userPermissions.find(it=> it == permission) ? 'bi bi-check-square' : 'bi bi-square';
  }
  userPermissionClass(permission) {
    const existsInPrevious = this.oldUserPermissions.find(it=> it == permission.permission);
    const existsInCurrent = this.userPermissions.find(it=> it == permission.permission);
    if (existsInPrevious && !existsInCurrent) {
      return 'rounded text-white bg-danger';
    }
    if (!existsInPrevious && existsInCurrent) {
      return 'rounded text-white bg-success';
    }
    if (this.activityLog.eventType == "New" && existsInCurrent) {
      return 'rounded text-white bg-success';
    }
    return '';
  }
  get userPermissions() {
    return this.userPermission.permissions.split(",");
  }
  get oldUserPermissions() {
    if (this.activityLog.eventType == "New") {
      return this.newValues?.['Permissions'].split(",");
    }
    return this.oldValues?.['Permissions'].split(",");
  }
}
