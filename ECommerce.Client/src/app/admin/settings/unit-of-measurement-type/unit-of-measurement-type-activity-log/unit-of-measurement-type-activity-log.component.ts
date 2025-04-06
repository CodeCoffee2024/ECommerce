import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { GenericActivityLogListingResult } from '../../../../models/generics/generc-activity-log-listing-result';
import { GenericListingResult } from '../../../../models/generics/generic-listing-result';
import { Failed, NotFound } from '../../../../models/message';
import { UnitOfMeasurementTypeResult } from '../../../../models/settings/unit-of-measurement-type/unit-of-measurement-type';
import { ToastType } from '../../../../models/toast';
import { ActivityLogService } from '../../../../shared/services/ActivityLog/activity-log.service';
import { LoadingService } from '../../../../shared/services/loading/loading.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { UnitOfMeasurementTypeService } from '../unit-of-measurement-type.service';

@Component({
  selector: 'app-unit-of-measurement-type-activity-log',
  templateUrl: './unit-of-measurement-type-activity-log.component.html',
  styleUrl: './unit-of-measurement-type-activity-log.component.scss'
})
export class UnitOfMeasurementTypeActivityLogComponent implements OnInit {
  Id: string;
  unitOfMeasurementType: UnitOfMeasurementTypeResult;
  activityLogs: GenericListingResult<GenericActivityLogListingResult[]>;
  constructor(
    private loadingService: LoadingService,
    private toastService: ToastService,
    private unitOfMeasurementTypeService: UnitOfMeasurementTypeService,
    private activityLogService: ActivityLogService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
  }
  get entity() {
    return this.unitOfMeasurementType.name;
  }
  ngOnInit(): void {    
    this.Id = this.activatedRoute.snapshot.paramMap.get('id') || null;
    if (!this.Id) {
      this.toastService.add("Error", NotFound("Unit of Measurement Types"), ToastType.ERROR);
      this.router.navigate(['/admin/settings/unit-of-measurement-types']);
    } else {
      this.activityLogService.getLogs('User',this.Id)
      .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
      .subscribe({
        next: (result) => {
          this.activityLogs = result.data;
          this.unitOfMeasurementTypeService.show(this.Id)
          .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
          .subscribe({
            next: (result) => {
              this.unitOfMeasurementType = result.data;
            },
            error: () => {
              this.toastService.add("Error", Failed("Unit of Measurement Type Logs"), ToastType.ERROR);
              this.router.navigate(['/admin/settings/unit-of-measurement-types']);
            }
          });
        },
        error: () => {
          this.toastService.add("Error", Failed("Unit of Measurement Types Logs"), ToastType.ERROR);
          this.router.navigate(['/admin/settings/unit-of-measurement-types']);
        }
      });
    }
  }
}
