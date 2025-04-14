import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { FormatUnitOfMeasurementStatus, UnitOfMeasurementPermission } from '../../../../models/settings/unit-of-measurement/unit-of-measurement';
import { UnitOfMeasurementListingResponse } from '../../../../models/settings/unit-of-measurement/unit-of-measurement-listing-response';
import { LoginService } from '../../../../login/login.service';
import { TitleService } from '../../../../shared/services/title/title.service';
import { UnitOfMeasurementService } from '../unit-of-measurement.service';
import { LoadingService } from '../../../../shared/services/loading/loading.service';
import { UnitOfMeasurementListingOption } from './unit-of-measurement-listing.option';
import { GenericListingResult } from '../../../../models/generics/generic-listing-result';
import { takeUntil } from 'rxjs';
import { UnitOfMeasurementForm } from '../unit-of-measurement-form/unit-of-measurement.form';
import { ModalService } from '../../../../shared/services/modal/modal.service';
import { UnitOfMeasurementFormComponent } from '../unit-of-measurement-form/unit-of-measurement-form.component';
import { UnitOfMeasurementTypeService } from '../../unit-of-measurement-type/unit-of-measurement-type.service';
import { UnitOfMeasurementTypeListingOption } from '../../unit-of-measurement-type/unit-of-measurement-type-listing/unit-of-measurement-type-listing.option';
import { Router } from '@angular/router';

@Component({
  selector: 'app-unit-of-measurement-listing',
  templateUrl: './unit-of-measurement-listing.component.html',
  styleUrl: './unit-of-measurement-listing.component.scss'
})
export class UnitOfMeasurementListingComponent extends BaseComponent implements OnInit {
  title = 'Unit of Measurements';
  isDropdownOpen = false;
  UserEnableToModifyUnitOfMeasurement = UnitOfMeasurementPermission.UserEnableToModifyUnitOfMeasurement;
  listingFormat: UnitOfMeasurementListingResponse[];
  listingOptionUnitOfMeasurementType = new UnitOfMeasurementTypeListingOption();
  form: UnitOfMeasurementForm = new UnitOfMeasurementForm();
  FormatStatus = FormatUnitOfMeasurementStatus;
  statuses = [];
  constructor(
    private authService: LoginService,
    private titleService: TitleService,
    private router: Router,
    protected unitOfMeasurementService: UnitOfMeasurementService,
    private unitOfMeasurementTypeService: UnitOfMeasurementTypeService,
    private loadingService: LoadingService,
    private modalService: ModalService,
  ) {
    super(authService, titleService, loadingService);
    this.titleService.setTitle(this.title);
    this.setGenerics(
      new UnitOfMeasurementListingOption(),
      'UnitOfMeasurement',
      this.unitOfMeasurementService,
      GenericListingResult<UnitOfMeasurementListingResponse[]>,
      this.listingFormat
    )
  }
  ngOnInit(): void {
    this.loadingService.loading$.subscribe(status => {
      this.isLoading = status;
    });
    this.refresh();
    this.refreshBySort.subscribe(it => {
      if (it.value == true) {
        this.listingOption.sortBy = it.sortBy;
        this.listingOption.sortDirection = it.sortDirection;
        this.refresh();
        this.turnOffSortEvent();
      }
    })
    this.loadStatuses();
  }
  private loadStatuses(): void {
    this.unitOfMeasurementService.getStatuses()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.statuses = res.data;
        console.log(this.statuses);
      });
  }
  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
  goTo(page: number) {
    this.listingOption.page = page;
    this.refresh();
  }
  async new() {
    this.unitOfMeasurementTypeService.getDropdown(this.listingOptionUnitOfMeasurementType).subscribe(async it => {
      this.form.initializeForm();
      this.form.setUnitOfMeasurementTypesList(it.data.result);
      const result = await this.modalService.open(UnitOfMeasurementFormComponent, {form: this.form});
      if (result) {
        this.router.navigate([`/admin/settings/unit-of-measurements/view/${result.id}`])
      }
    });
  }

}
