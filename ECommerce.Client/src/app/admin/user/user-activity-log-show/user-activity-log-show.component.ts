import { Component, OnInit } from '@angular/core';
import { UserResult } from '../../../models/user/user';
import { GenericActivityLogResult } from '../../../models/generics/generic-activity-log-result';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { ToastService } from '../../../shared/services/toast/toast.service';
import { UserService } from '../user.service';
import { ActivityLogService } from '../../../shared/services/ActivityLog/activity-log.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Failed, NotFound } from '../../../models/message';
import { ToastType } from '../../../models/toast';
import { finalize } from 'rxjs';
import { environment } from '../../../../../environment';

@Component({
  selector: 'app-user-activity-log-show',
  templateUrl: './user-activity-log-show.component.html',
  styleUrl: './user-activity-log-show.component.scss'
})
export class UserActivityLogShowComponent implements OnInit {
  Id: string;
  primaryKey: string;
  user: UserResult;
  activityLog: GenericActivityLogResult;
  constructor(
    private loadingService: LoadingService,
    private toastService: ToastService,
    private userService: UserService,
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
      this.toastService.add("Error", NotFound("User Log"), ToastType.ERROR);
      this.router.navigate(['/admin/users']);
    } else {
      this.activityLogService.getLog("User",this.primaryKey)
      .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
      .subscribe({
        next: (result) => {
          this.activityLog = result.data;
          this.userService.show(this.Id)
          .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
          .subscribe({
            next: (result) => {
              this.user = result.data;
              this.loadingService.hide();
            },
            error: () => {
              this.toastService.add("Error", Failed("User Log"), ToastType.ERROR);
              this.router.navigate(['/admin/users']);
            }
          });
        },
        error: () => {
          this.toastService.add("Error", Failed("User Log"), ToastType.ERROR);
          this.router.navigate(['/admin/users']);
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
  
    if (!oldVal && newVal) return 'bg-success'; // New value
    if (oldVal && newVal && oldVal !== newVal) return 'bg-warning'; // Changed value
    if (oldVal && !newVal) return 'bg-danger'; // Removed value
    return ''; // No change
  }
  userPermissionClass(permission) {
    const existsInPrevious = this.oldUserPermissions.find(it=> it == permission);
    const existsInCurrent = this.newUserPermissions.find(it=> it == permission);
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
  displayChange(key) {
    const oldVal = this.oldValues?.[key];
    const newVal = this.newValues?.[key];
    if (!oldVal && newVal) return `<span class="rounded bg-success text-white pl-2 pr-2">${newVal}</span>`; // New value
    if (oldVal && newVal && oldVal !== newVal) return `<span class="rounded bg-warning text-white mr-2 pl-2 pr-2">${oldVal}</span><span class="rounded text-white bg-success pl-2 pr-2">${newVal}</span>`; // Changed value
    if (oldVal && !newVal) return `<span class="rounded bg-danger text-white pl-2 pr-2">${oldVal}</span> --`; // Removed value
    console.log(newVal);
    return `${newVal}`; // No change
  }
  get oldUserPermissions() {
    if (this.activityLog.eventType == "New") {
      return this.newValues?.['User Permissions'].split(",");
    }
    return this.oldValues?.['User Permissions'].split(",");
  }
  get newUserPermissions() {
    return this.user.permissions.map(it => it.name);
  }
  get keys() {
    const oldKeys = this.oldValues ? Object.keys(this.oldValues) : [];
    const newKeys = this.newValues ? Object.keys(this.newValues) : [];
    return Array.from(new Set([...oldKeys, ...newKeys]));
  }
  get displayImg() {
    let img = '';
    if (this.activityLog.eventType == 'New') {
      const imgClass='p-1 text-white mr-2 d-flex justify-content-center bg-success activity-log-img align-items-center';
      img += `<div class="${imgClass}">N/A</div>`;
      return img;
    }
    const defaultClass='p-1 d-flex justify-content-center mr-2 align-items-center ';
    if (this.oldValues?.['Img'] != '--' && this.oldValues?.['Img'] != '') {
      let imgClass = defaultClass;
      if (this.oldValues?.['Img'] !== this.newValues?.['Img']) {
        imgClass += 'bg-warning activity-log-img';
      }
      img += `<div class="${imgClass}"><img width=100 height=100 alt="Profile Image" src="${environment.folderPath + this.oldValues?.['Img']}"/></div>`;
    } else {
      const imgClass='p-1 text-white mr-2 d-flex justify-content-center bg-danger activity-log-img align-items-center';
      img += `<div class="${imgClass}">N/A</div>`;
    }
    if (this.newValues?.['Img'] != '--' && this.newValues?.['Img'] != '') {
      let imgClass = defaultClass;
      if (this.oldValues?.['Img'] !== this.newValues?.['Img']) {
        imgClass += 'bg-success activity-log-img';
      }
      img += `<div class="${imgClass}"><img width=100 height=100 alt="Profile Image" src="${environment.folderPath + this.newValues?.['Img']}"/></div>`;
    }
    return img;
  }
  
}
