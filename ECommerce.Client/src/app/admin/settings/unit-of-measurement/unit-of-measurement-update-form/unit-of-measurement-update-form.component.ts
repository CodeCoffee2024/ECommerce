import { Component } from '@angular/core';
import { LoginService } from '../../../../login/login.service';
import { UpdateSuccess } from '../../../../models/message';
import { ToastType } from '../../../../models/toast';
import { InputTypes } from '../../../../shared/constants/input-type';
import { FormErrorService } from '../../../../shared/services/form-error/form-error.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { UnitOfMeasurementTypeListingOption } from '../../unit-of-measurement-type/unit-of-measurement-type-listing/unit-of-measurement-type-listing.option';
import { UnitOfMeasurementTypeService } from '../../unit-of-measurement-type/unit-of-measurement-type.service';
import { UnitOfMeasurementForm } from '../unit-of-measurement-form/unit-of-measurement.form';
import { UnitOfMeasurementService } from '../unit-of-measurement.service';

@Component({
  selector: 'app-unit-of-measurement-update-form',
  templateUrl: './unit-of-measurement-update-form.component.html',
  styleUrl: './unit-of-measurement-update-form.component.scss'
})
export class UnitOfMeasurementUpdateFormComponent {
  form: UnitOfMeasurementForm;
  InputTypes = InputTypes;
  hasMore = false;
  isUpdate = false;
  isDropdownLoading = false;
  isLoading = false;
  listingOptionUnitOfMeasurementType = new UnitOfMeasurementTypeListingOption();
  constructor(
    private unitOfMeasurementService: UnitOfMeasurementService,
    private toastService: ToastService,
    private loginService: LoginService,
    private formErrorService: FormErrorService,
    private unitOfMeasurementTypeService: UnitOfMeasurementTypeService,
  ) {
  }
  title = 'Unit of Measurement Update';
  submit() {
    this.unitOfMeasurementService.update(this.form.submitData, this.form.id).subscribe({
      next: () => {
        this.toastService.add("Success", UpdateSuccess("Unit of Measurement"), ToastType.SUCCESS);
        // this.activeModal.close(true);
      },
      error: (result) => {
        this.formErrorService.setServerErrors(this.form.form, result);
      }
    });
  }
  searchChanged(searchValue) {
    this.listingOptionUnitOfMeasurementType.search = searchValue;
    this.unitOfMeasurementTypeService.getDropdown(this.listingOptionUnitOfMeasurementType).subscribe(async it => {
      this.form.setUnitOfMeasurementTypesList(it.data.result);
    });
  }
  async onSearchChanged({ search, page, clear = false }: { search: string; page: number, clear: boolean }) {
    this.isDropdownLoading = true;
    this.listingOptionUnitOfMeasurementType.search = search;
    this.listingOptionUnitOfMeasurementType.page = page;
    if (clear) {
      this.form.setUnitOfMeasurementTypesList([]);
    }
    this.unitOfMeasurementTypeService.getDropdown(this.listingOptionUnitOfMeasurementType).subscribe(it => {
      this.hasMore = page * it.data.totalRecords < it.data.totalPages * it.data.totalRecords;
      const list = clear? it.data.result : [...this.form.unitOfMeasurementTypesList, ...it.data.result];
      this.form.setUnitOfMeasurementTypesList(list);
      this.isDropdownLoading = false;
    });
  }
  
  onSelectionChange(selected): void {
    console.log(selected)
    this.listingOptionUnitOfMeasurementType.exclude = selected;
    this.form.form.get('unitOfMeasurementType').setValue(selected);
    // this.refresh();
  }
}

