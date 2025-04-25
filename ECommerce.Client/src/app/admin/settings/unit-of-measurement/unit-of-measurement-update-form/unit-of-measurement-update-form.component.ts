import { Component, Input, OnInit } from '@angular/core';
import { LoginService } from '../../../../login/login.service';
import { UpdateSuccess } from '../../../../models/message';
import { ToastType } from '../../../../models/toast';
import { InputTypes } from '../../../../shared/constants/input-type';
import { FormErrorService } from '../../../../shared/services/form-error/form-error.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { UnitOfMeasurementForm } from '../unit-of-measurement-form/unit-of-measurement.form';
import { UnitOfMeasurementListingOption } from '../unit-of-measurement-listing/unit-of-measurement-listing.option';
import { UnitOfMeasurementService } from '../unit-of-measurement.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-unit-of-measurement-update-form',
  templateUrl: './unit-of-measurement-update-form.component.html',
  styleUrl: './unit-of-measurement-update-form.component.scss'
})
export class UnitOfMeasurementUpdateFormComponent implements OnInit {
  @Input() form: UnitOfMeasurementForm;
  InputTypes = InputTypes;
  hasMore = false;
  isUpdate = false;
  isDropdownLoading = false;
  isLoading = false;
  title = 'Unit of Measurement Update';
  listingOptionUnitOfMeasurement = new UnitOfMeasurementListingOption();
  constructor(
    private unitOfMeasurementService: UnitOfMeasurementService,
    private toastService: ToastService,
    private loginService: LoginService,
    private formErrorService: FormErrorService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.listingOptionUnitOfMeasurement.unitOfMeasurementType = this.form.form.get('unitOfMeasurementType').value?.id;
    this.listingOptionUnitOfMeasurement.exclude = [this.form.id, this.form.convertToValues.map(it => it?.unitOfMeasurementTo).join(",")].join(",");
    
  }

  submit() {
    this.unitOfMeasurementService.update(this.form.submitData, this.form.id).subscribe({
      next: () => {
        this.toastService.add("Success", UpdateSuccess("Unit of Measurement"), ToastType.SUCCESS);
        this.router.navigate(['/admin/settings/unit-of-measurements/view/'+this.form.id]);
      },
      error: (result) => {
        this.formErrorService.setServerErrors(this.form.form, result);
      }
    });
  }

  searchChanged(searchValue) {
    this.listingOptionUnitOfMeasurement.search = searchValue;
    this.unitOfMeasurementService.getDropdown(this.listingOptionUnitOfMeasurement).subscribe(async it => {
      this.form.setUnitOfMeasurementsList(it.data.result);
    });
  }

  async onSearchChanged({ search, page, clear = false }: { search: string; page: number, clear: boolean }) {
    this.isDropdownLoading = true;
    this.listingOptionUnitOfMeasurement.search = search;
    this.listingOptionUnitOfMeasurement.page = page;
    
    if (clear) {
      this.form.setUnitOfMeasurementsList([]);
    }
    console.log(this.listingOptionUnitOfMeasurement);
    this.unitOfMeasurementService.getDropdown(this.listingOptionUnitOfMeasurement).subscribe(it => {
      this.hasMore = page * it.data.totalRecords < it.data.totalPages * it.data.totalRecords;
      const list = clear? it.data.result : [...this.form.unitOfMeasurementList, ...it.data.result];
      this.form.setUnitOfMeasurementsList(list);
      this.isDropdownLoading = false;
    });
  }

  removeConversion(index: number): void {
    this.listingOptionUnitOfMeasurement.exclude = [this.form.id, this.form.convertToValues.map(it => it?.unitOfMeasurementTo).join(",")].join(",");
    if (this.form.conversions.length <= 1) return;
    this.form.conversions.removeAt(index);
  }

  onSelectionChange(selected): void {
    const fg = selected?.formGroup;
    fg.get('unitOfMeasurementTo').setValue(selected?.item);
    fg.get('unitOfMeasurementTo').markAsDirty();
    fg.get('value').markAsDirty();
    this.listingOptionUnitOfMeasurement.exclude = [this.form.id, this.form.convertToValues.map(it => it?.unitOfMeasurementTo).join(",")].join(",");
  }
}