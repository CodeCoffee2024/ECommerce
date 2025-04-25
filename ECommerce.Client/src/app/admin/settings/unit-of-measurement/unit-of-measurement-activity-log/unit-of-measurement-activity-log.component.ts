import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { GenericActivityLogListingResult } from '../../../../models/generics/generc-activity-log-listing-result';
import { GenericListingResult } from '../../../../models/generics/generic-listing-result';
import { Failed, NotFound } from '../../../../models/message';
import { UnitOfMeasurementResult } from '../../../../models/settings/unit-of-measurement/unit-of-measurement';
import { ToastType } from '../../../../models/toast';
import { ActivityLogService } from '../../../../shared/services/ActivityLog/activity-log.service';
import { LoadingService } from '../../../../shared/services/loading/loading.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { UnitOfMeasurementService } from '../unit-of-measurement.service';

@Component({
  selector: 'app-unit-of-measurement-activity-log',
  templateUrl: './unit-of-measurement-activity-log.component.html',
  styleUrl: './unit-of-measurement-activity-log.component.scss'
})
export class UnitOfMeasurementActivityLogComponent implements OnInit {
  Id: string;
  unitOfMeasurement: UnitOfMeasurementResult;
  activityLogs: GenericListingResult<GenericActivityLogListingResult[]>;
  constructor(
    private loadingService: LoadingService,
    private toastService: ToastService,
    private unitOfMeasurementService: UnitOfMeasurementService,
    private activityLogService: ActivityLogService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
  }
  get entity() {
    return this.unitOfMeasurement.name;
  }
  ngOnInit(): void {    
    this.Id = this.activatedRoute.snapshot.paramMap.get('id') || null;
    if (!this.Id) {
      this.toastService.add("Error", NotFound("Unit of Measurements"), ToastType.ERROR);
      this.router.navigate(['/admin/settings/unit-of-measurements']);
    } else {
      this.activityLogService.getLogs('User',this.Id)
      .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
      .subscribe({
        next: (result) => {
          this.activityLogs = result.data;
          this.unitOfMeasurementService.show(this.Id)
          .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
          .subscribe({
            next: (result) => {
              this.unitOfMeasurement = result.data;
            },
            error: () => {
              this.toastService.add("Error", Failed("Unit of Measurement Logs"), ToastType.ERROR);
              this.router.navigate(['/admin/settings/unit-of-measurements']);
            }
          });
        },
        error: () => {
          this.toastService.add("Error", Failed("Unit of Measurement Logs"), ToastType.ERROR);
          this.router.navigate(['/admin/settings/unit-of-measurements']);
        }
      });
    }
  }
}
