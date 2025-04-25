import { Component } from '@angular/core';
import { UnitOfMeasurementForm } from './unit-of-measurement.form';
import { InputTypes } from '../../../../shared/constants/input-type';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { UnitOfMeasurementService } from '../unit-of-measurement.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { LoginService } from '../../../../login/login.service';
import { FormErrorService } from '../../../../shared/services/form-error/form-error.service';
import { CreateSuccess, UpdateSuccess } from '../../../../models/message';
import { ToastType } from '../../../../models/toast';
import { UnitOfMeasurementTypeService } from '../../unit-of-measurement-type/unit-of-measurement-type.service';
import { UnitOfMeasurementTypeListingOption } from '../../unit-of-measurement-type/unit-of-measurement-type-listing/unit-of-measurement-type-listing.option';

@Component({
  selector: 'app-unit-of-measurement-form',
  templateUrl: './unit-of-measurement-form.component.html',
  styleUrl: './unit-of-measurement-form.component.scss'
})
export class UnitOfMeasurementFormComponent {
  form: UnitOfMeasurementForm;
  InputTypes = InputTypes;
  hasMore = false;
  isUpdate = false;
  isDropdownLoading = false;
  isLoading = false;
  listingOptionUnitOfMeasurementType = new UnitOfMeasurementTypeListingOption();
  constructor(
    public activeModal: NgbActiveModal,
    private unitOfMeasurementService: UnitOfMeasurementService,
    private toastService: ToastService,
    private loginService: LoginService,
    private formErrorService: FormErrorService,
    private unitOfMeasurementTypeService: UnitOfMeasurementTypeService,
  ) {
  }
  get title() {
    return this.isUpdate ? 'Unit of Measurement Update':'Unit of Measurement New';
  }
  submit() {
    if(!this.isUpdate) {
      this.unitOfMeasurementService.create(this.form.submitData).subscribe({
        next: (result) => {
          this.toastService.add("Success", CreateSuccess("Unit of Measurement"), ToastType.SUCCESS);
          this.activeModal.close(result.data);
        },
        error: (result) => {
          this.formErrorService.setServerErrors(this.form.form, result);
        }
      });
    } else {
      this.unitOfMeasurementService.update(this.form.submitData, this.form.id).subscribe({
        next: () => {
          this.toastService.add("Success", UpdateSuccess("Unit of Measurement"), ToastType.SUCCESS);
          this.activeModal.close(true);
        },
        error: (result) => {
          this.formErrorService.setServerErrors(this.form.form, result);
        }
      });
    }
  }
  cancel() {
    this.activeModal.dismiss(false);
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
    this.listingOptionUnitOfMeasurementType.exclude = selected.item;
    this.form.form.get('unitOfMeasurementType').setValue(selected.item);
    // this.refresh();
  }
}
