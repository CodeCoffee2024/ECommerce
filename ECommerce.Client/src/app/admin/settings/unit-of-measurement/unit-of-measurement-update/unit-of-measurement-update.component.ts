import { Component, OnInit } from '@angular/core';
import { UnitOfMeasurementForm } from '../unit-of-measurement-form/unit-of-measurement.form';
import { UnitOfMeasurementResult } from '../../../../models/settings/unit-of-measurement/unit-of-measurement';
import { UnitOfMeasurementService } from '../unit-of-measurement.service';
import { FormErrorService } from '../../../../shared/services/form-error/form-error.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { LoadingService } from '../../../../shared/services/loading/loading.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Failed, NotFound } from '../../../../models/message';
import { ToastType } from '../../../../models/toast';
import { catchError, finalize, forkJoin } from 'rxjs';
import { UnitOfMeasurementListingOption } from '../unit-of-measurement-listing/unit-of-measurement-listing.option';

@Component({
  selector: 'app-unit-of-measurement-update',
  templateUrl: './unit-of-measurement-update.component.html',
  styleUrl: './unit-of-measurement-update.component.scss'
})
export class UnitOfMeasurementUpdateComponent implements OnInit {
  form: UnitOfMeasurementForm = new UnitOfMeasurementForm();
  Id: string;
  isLoading = true;
  unitOfMeasurementResult:UnitOfMeasurementResult = new UnitOfMeasurementResult();
  listingOption = new UnitOfMeasurementListingOption();
  constructor(
    private unitOfMeasurementService: UnitOfMeasurementService,
    private formErrorService: FormErrorService,
    private toastService: ToastService,
    private loadingService: LoadingService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {

  }
  ngOnInit(): void {
    this.loadingService.show();
    this.Id = this.activatedRoute.snapshot.paramMap.get('id') || null;

    if (!this.Id) {
      this.toastService.add("Error", NotFound("Unit of Measurement"), ToastType.ERROR);
      this.router.navigate(['../../']);
      return;
    }

    forkJoin({
      unitOfMeasurements: this.unitOfMeasurementService.getActiveList(this.listingOption),
      unitOfMeasurement: this.unitOfMeasurementService.show(this.Id),
    })
    .pipe(
      finalize(() => this.loadingService.hide()), // Ensure loading is hidden after execution
      catchError((error) => {
        this.toastService.add("Error", Failed("Unit of Measurement"), ToastType.ERROR);
        this.router.navigate(['../../']);
        throw error;
      })
    )
    .subscribe(({ unitOfMeasurements, unitOfMeasurement }) => {
      this.form.initializeForm();
      this.isLoading = false;
      this.unitOfMeasurementResult = unitOfMeasurement.data;
      this.form.fill(unitOfMeasurement.data, unitOfMeasurements.data.result);
    });
  }
  // submit(data) {
  //   this.userPermissionService.update(data, this.Id).subscribe({
  //     next: (result) => {
  //       this.toastService.add("Success", UpdateSuccess("User Permission"), ToastType.SUCCESS);
  //       this.router.navigate(['/admin/user-permissions/view/'+result.data.id]);

  //     }, error: (result) => {
  //       this.formErrorService.setServerErrors(this.form.form, result);
  //     }
  //   })
  // }
}
