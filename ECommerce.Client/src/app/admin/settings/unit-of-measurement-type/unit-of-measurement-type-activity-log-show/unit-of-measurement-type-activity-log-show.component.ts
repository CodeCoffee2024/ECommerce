import { Component, OnInit } from '@angular/core';
import { UnitOfMeasurementTypeResult } from '../../../../models/settings/unit-of-measurement-type/unit-of-measurement-type';
import { GenericActivityLogResult } from '../../../../models/generics/generic-activity-log-result';
import { LoadingService } from '../../../../shared/services/loading/loading.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { UnitOfMeasurementTypeService } from '../unit-of-measurement-type.service';
import { ActivityLogService } from '../../../../shared/services/ActivityLog/activity-log.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Failed, NotFound } from '../../../../models/message';
import { ToastType } from '../../../../models/toast';
import { finalize } from 'rxjs';
import { FormatStatus } from '../../../../models/status';

@Component({
  selector: 'app-unit-of-measurement-type-activity-log-show',
  templateUrl: './unit-of-measurement-type-activity-log-show.component.html',
  styleUrl: './unit-of-measurement-type-activity-log-show.component.scss'
})
export class UnitOfMeasurementTypeActivityLogShowComponent implements OnInit {
  Id: string;
  primaryKey: string;
  unitOfMeasurement: UnitOfMeasurementTypeResult;
  activityLog: GenericActivityLogResult;
  constructor(
    private loadingService: LoadingService,
    private toastService: ToastService,
    private unitOfMeasurementService: UnitOfMeasurementTypeService,
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
      this.toastService.add("Error", NotFound("Unit of Measurement Type Log"), ToastType.ERROR);
      this.router.navigate(['/admin/settings/unit-of-measurement-types']);
    } else {
      this.activityLogService.getLog("UnitOfMeasurementType",this.primaryKey)
      .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
      .subscribe({
        next: (result) => {
          this.activityLog = result.data;
          this.unitOfMeasurementService.show(this.Id)
          .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
          .subscribe({
            next: (result) => {
              this.unitOfMeasurement = result.data;
              this.loadingService.hide();
            },
            error: () => {
              this.toastService.add("Error", Failed("Unit of Measurement Type Log"), ToastType.ERROR);
              this.router.navigate(['/admin/settings/unit-of-measurement-types']);
            }
          });
        },
        error: () => {
          this.toastService.add("Error", Failed("Unit of Measurement Type Log"), ToastType.ERROR);
          this.router.navigate(['/admin/settings/unit-of-measurement-types']);
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
    let oldVal = this.oldValues?.[key];
    let newVal = this.newValues?.[key];
    if (key == 'Status') {
      if (oldVal) {
        oldVal = FormatStatus.format(oldVal);
      }
      newVal = FormatStatus.format(newVal);
    } 
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
  
}
