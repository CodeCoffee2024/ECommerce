import { Component, OnInit } from '@angular/core';
import { UnitOfMeasurementResult } from '../../../../models/settings/unit-of-measurement/unit-of-measurement';
import { GenericActivityLogResult } from '../../../../models/generics/generic-activity-log-result';
import { LoadingService } from '../../../../shared/services/loading/loading.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { UnitOfMeasurementService } from '../unit-of-measurement.service';
import { ActivityLogService } from '../../../../shared/services/ActivityLog/activity-log.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Failed, NotFound } from '../../../../models/message';
import { ToastType } from '../../../../models/toast';
import { finalize } from 'rxjs';
import { FormatStatus } from '../../../../models/status';

@Component({
  selector: 'app-unit-of-measurement-activity-log-show',
  templateUrl: './unit-of-measurement-activity-log-show.component.html',
  styleUrl: './unit-of-measurement-activity-log-show.component.scss'
})
export class UnitOfMeasurementActivityLogShowComponent  implements OnInit {
  Id: string;
  primaryKey: string;
  unitOfMeasurement: UnitOfMeasurementResult;
  activityLog: GenericActivityLogResult;
  constructor(
    private loadingService: LoadingService,
    private toastService: ToastService,
    private unitOfMeasurementService: UnitOfMeasurementService,
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
      this.toastService.add("Error", NotFound("Unit of Measurement Log"), ToastType.ERROR);
      this.router.navigate(['/admin/settings/unit-of-measurements']);
    } else {
      this.activityLogService.getLog("UnitOfMeasurement",this.primaryKey)
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
              this.toastService.add("Error", Failed("Unit of Measurement Log"), ToastType.ERROR);
              this.router.navigate(['/admin/settings/unit-of-measurements']);
            }
          });
        },
        error: () => {
          this.toastService.add("Error", Failed("Unit of Measurement Log"), ToastType.ERROR);
          this.router.navigate(['/admin/settings/unit-of-measurements']);
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
  displayChangeConversion(id, key) {
    let oldVal = this.oldValues?.['Conversions'];
    let newVal = this.newValues?.['Conversions'];
    if (!oldVal) {
      const newItem = JSON.parse(newVal)?.find(it => it.Id == id);
      return `<span class="rounded text-white bg-success pl-2 pr-2">${newItem[key]}</span>`;
    } 
    const newItem = JSON.parse(newVal)?.find(it => it.Id == id);
    const existing = JSON.parse(oldVal)?.find(it => it.Id == id);
    if (!existing && newItem) {
      return `<span class="rounded text-white bg-success pl-2 pr-2">${newItem[key]}</span>`;
    }
    if (existing && newItem && existing[key] !== newItem[key]) {
      return `<span class="rounded text-white bg-warning mr-2 pl-2 pr-2">${existing[key]}</span><span class="rounded text-white bg-success pl-2 pr-2">${newItem[key]}</span>`; // Changed value
    }
    if (existing && !newItem) {
      return `<span class="rounded text-white bg-danger pl-2 pr-2">${existing[key]}</span> --`;
    }
    return newItem[key];
  }
  get keys() {
    const oldKeys = this.oldValues ? Object.keys(this.oldValues) : [];
    const newKeys = this.newValues ? Object.keys(this.newValues) : [];
    return Array.from(new Set([...oldKeys, ...newKeys]));
  }
  get conversions() {
    const oldConversions = JSON.parse(this.oldValues['Conversions'] || '[]');
    const newConversions = JSON.parse(this.newValues['Conversions'] || '[]');
  
    const combined = [...oldConversions, ...newConversions];
  
    const uniqueById = Array.from(
      new Map(combined.reverse().map(item => [item.Id, item])).values()
    ).reverse(); // optional, to preserve original order
    return uniqueById;
  }
  
}