import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { GenericActivityLogListingResult } from '../../../models/generics/generc-activity-log-listing-result';
import { GenericListingResult } from '../../../models/generics/generic-listing-result';
import { Failed, NotFound } from '../../../models/message';
import { ToastType } from '../../../models/toast';
import { UserPermissionResult } from '../../../models/user-permission/user-permission';
import { ActivityLogService } from '../../../shared/services/ActivityLog/activity-log.service';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { ToastService } from '../../../shared/services/toast/toast.service';
import { UserPermissionService } from '../user-permission.service';

@Component({
  selector: 'app-user-permission-activity-log',
  templateUrl: './user-permission-activity-log.component.html',
  styleUrl: './user-permission-activity-log.component.scss'
})
export class UserPermissionActivityLogComponent implements OnInit {
  Id: string;
  userPermission: UserPermissionResult;
  activityLogs: GenericListingResult<GenericActivityLogListingResult[]>;
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
    if (!this.Id) {
      this.toastService.add("Error", NotFound("User Permission Logs"), ToastType.ERROR);
      this.router.navigate(['/admin/user-permissions']);
    } else {
      this.activityLogService.getLogs("UserPermission",this.Id)
      .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
      .subscribe({
        next: (result) => {
          this.activityLogs = result.data;
          this.userPermissionService.show(this.Id)
          .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
          .subscribe({
            next: (result) => {
              this.userPermission = result.data;
            },
            error: () => {
              this.toastService.add("Error", Failed("User Permission Logs"), ToastType.ERROR);
              this.router.navigate(['/admin/user-permissions']);
            }
          });
        },
        error: () => {
          this.toastService.add("Error", Failed("User Permission Logs"), ToastType.ERROR);
          this.router.navigate(['/admin/user-permissions']);
        }
      });
    }
  }
}
