import { Component, OnInit } from '@angular/core';
import { finalize, takeUntil } from 'rxjs';
import { LoginService } from '../../../../login/login.service';
import { GenericListingResult } from '../../../../models/generics/generic-listing-result';
import { Failed, NotFound } from '../../../../models/message';
import { FormatUnitOfMeasurementTypeStatus, UnitOfMeasurementTypePermission } from '../../../../models/settings/unit-of-measurement-type/unit-of-measurement-type';
import { UnitOfMeasurementTypeListingResponse } from '../../../../models/settings/unit-of-measurement-type/unit-of-measurement-type-listing-response';
import { ToastType } from '../../../../models/toast';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { LoadingService } from '../../../../shared/services/loading/loading.service';
import { ModalService } from '../../../../shared/services/modal/modal.service';
import { TitleService } from '../../../../shared/services/title/title.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { UnitOfMeasurementTypeFormComponent } from '../unit-of-measurement-type-form/unit-of-measurement-type-form.component';
import { UnitOfMeasurementTypeForm } from '../unit-of-measurement-type-form/unit-of-measurement-type.form';
import { UnitOfMeasurementTypeShowComponent } from '../unit-of-measurement-type-show/unit-of-measurement-type-show.component';
import { UnitOfMeasurementTypeService } from '../unit-of-measurement-type.service';
import { UnitOfMeasurementTypeListingOption } from './unit-of-measurement-type-listing.option';

@Component({
  selector: 'app-unit-of-measurement-type-listing',
  templateUrl: './unit-of-measurement-type-listing.component.html',
  styleUrl: './unit-of-measurement-type-listing.component.scss'
})
export class UnitOfMeasurementTypeListingComponent extends BaseComponent implements OnInit {
  title = 'Unit of Measurement Types';
  isDropdownOpen = false;
  isDropdownLoading = false;
  userPermissions = [];
  statuses = [];
  hasMore = false;
  UserEnableToModifyUnitOfMeasurementType = UnitOfMeasurementTypePermission.UserEnableToModifyUnitOfMeasurementType; 
  form: UnitOfMeasurementTypeForm = new UnitOfMeasurementTypeForm();
  listingFormat: UnitOfMeasurementTypeListingResponse[];
  FormatStatus = FormatUnitOfMeasurementTypeStatus;
  listingOptionDecimal = [
    {
      id: 'hasDecimalAll',
      value: '',
      description: 'All'
    },
    {
      id: 'hasDecimalYes',
      value: 'Yes',
      description: 'Yes'
    },
    {
      id: 'hasDecimalNo',
      value: 'No',
      description: 'No'
    },
  ]
  constructor(
    private authService: LoginService,
    private titleService: TitleService,
    protected unitOfMeasurementTypeService: UnitOfMeasurementTypeService,
    private loadingService: LoadingService,
    private modalService: ModalService,
    private toastService: ToastService,
  ) {
    super(authService, titleService, loadingService);
    this.titleService.setTitle(this.title);
    this.setGenerics(
      new UnitOfMeasurementTypeListingOption(),
    'UnitOfMeasurementType',
      this.unitOfMeasurementTypeService,
      GenericListingResult<UnitOfMeasurementTypeListingResponse[]>,
      this.listingFormat
    )
  }
  ngOnInit(): void {
    this.loadStatuses();
    this.subscribeToLoading();
    this.subscribeToSortEvents();
    this.refresh();
  }
  
  private loadStatuses(): void {
    this.unitOfMeasurementTypeService.getStatuses()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        this.statuses = res.data;
        console.log(this.statuses);
      });
  }
  

  private subscribeToLoading(): void {
    this.loadingService.loading$
      .pipe(takeUntil(this.destroy$))
      .subscribe(status => {
        this.isLoading = status;
      });
  }
  
  private subscribeToSortEvents(): void {
    this.refreshBySort
      .pipe(takeUntil(this.destroy$))
      .subscribe(it => {
        if (it.value) {
          this.listingOption.sortBy = it.sortBy;
          this.listingOption.sortDirection = it.sortDirection;
          this.refresh();
          this.turnOffSortEvent();
        }
      });
  }
  
  async new() {
    this.form.initializeForm();
    const result = await this.modalService.open(UnitOfMeasurementTypeFormComponent, {form: this.form});
    if (result) {
      this.refresh();
    }
  }
  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
  goTo(page: number) {
    this.listingOption.page = page;
    this.refresh();
  }
  async show(id) {
    this.loadingService.show();
    if (!id) {
      this.toastService.add("Error", NotFound("Unit of Measurement Type"), ToastType.ERROR);
    } else {
      this.unitOfMeasurementTypeService.show(id)
      .pipe(finalize(() => this.loadingService.hide()))
      .subscribe({
        next: async (result) => {
          this.loadingService.hide();
          const isSuccess = await this.modalService.open(UnitOfMeasurementTypeShowComponent, {unitOfMeasurement: result.data});
          if (isSuccess) {
            this.refresh();
          }
        },
        error: () => {
          this.toastService.add("Error", Failed("Unit of Measurement Type"), ToastType.ERROR);
        }
      });
    }
  }
}
