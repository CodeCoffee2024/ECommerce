import { Component, OnInit } from '@angular/core';
import { UserResult } from '../../../models/user/user';
import { GenericListingResult } from '../../../models/generics/generic-listing-result';
import { GenericActivityLogListingResult } from '../../../models/generics/generc-activity-log-listing-result';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { ToastService } from '../../../shared/services/toast/toast.service';
import { UserService } from '../user.service';
import { ActivityLogService } from '../../../shared/services/ActivityLog/activity-log.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Failed, NotFound } from '../../../models/message';
import { ToastType } from '../../../models/toast';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-user-activity-log',
  templateUrl: './user-activity-log.component.html',
  styleUrl: './user-activity-log.component.scss'
})
export class UserActivityLogComponent implements OnInit {
  Id: string;
  user: UserResult;
  activityLogs: GenericListingResult<GenericActivityLogListingResult[]>;
  constructor(
    private loadingService: LoadingService,
    private toastService: ToastService,
    private userService: UserService,
    private activityLogService: ActivityLogService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
  }
  get entity() {
    return this.user.firstName + " " +this.user.lastName
  }
  ngOnInit(): void {    
    this.Id = this.activatedRoute.snapshot.paramMap.get('id') || null;
    if (!this.Id) {
      this.toastService.add("Error", NotFound("User Logs"), ToastType.ERROR);
      this.router.navigate(['/admin/users']);
    } else {
      this.activityLogService.getLogs('User',this.Id)
      .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
      .subscribe({
        next: (result) => {
          this.activityLogs = result.data;
          this.userService.show(this.Id)
          .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
          .subscribe({
            next: (result) => {
              this.user = result.data;
            },
            error: () => {
              this.toastService.add("Error", Failed("User Logs"), ToastType.ERROR);
              this.router.navigate(['/admin/users']);
            }
          });
        },
        error: () => {
          this.toastService.add("Error", Failed("User Logs"), ToastType.ERROR);
          this.router.navigate(['/admin/users']);
        }
      });
    }
  }
  goTo(page) {
    console.log(page);
  }
}
