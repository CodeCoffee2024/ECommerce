import { Component } from '@angular/core';
import { ProductCategoryForm } from './product-category.form';
import { InputTypes } from '../../../../shared/constants/input-type';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductCategoryService } from '../../product-category.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { LoginService } from '../../../../login/login.service';
import { FormErrorService } from '../../../../shared/services/form-error/form-error.service';
import { CreateSuccess, UpdateSuccess } from '../../../../models/message';
import { ToastType } from '../../../../models/toast';
import { ProductCategoryListingOption } from '../product-category-listing/product-category-listing.option';

@Component({
  selector: 'app-product-category-form',
  templateUrl: './product-category-form.component.html',
  styleUrl: './product-category-form.component.scss'
})
export class ProductCategoryFormComponent {
  form: ProductCategoryForm;
  InputTypes = InputTypes;
  isUpdate = false;
  isDropdownLoading = false;
  isLoading = false;
  hasMore = false;
  listingOptionProductCategory = new ProductCategoryListingOption();
  constructor(
    public activeModal: NgbActiveModal,
    private productCategoryService: ProductCategoryService,
    private toastService: ToastService,
    private loginService: LoginService,
    private formErrorService: FormErrorService
  ) {
  }
  get title() {
    return this.isUpdate ? 'Product Category Update':'Product Category New';
  }
  submit() {
    if(!this.isUpdate) {
      this.productCategoryService.create(this.form.submitData).subscribe({
        next: () => {
          this.toastService.add("Success", CreateSuccess("Product Category"), ToastType.SUCCESS);
          this.activeModal.close(true);
        },
        error: (result) => {
          this.formErrorService.setServerErrors(this.form.form, result);
        }
      });
    } else {
      this.productCategoryService.update(this.form.submitData, this.form.id).subscribe({
        next: () => {
          this.toastService.add("Success", UpdateSuccess("Product Category"), ToastType.SUCCESS);
          this.activeModal.close(true);
        },
        error: (result) => {
          this.formErrorService.setServerErrors(this.form.form, result);
        }
      });
    }
  }
  searchChanged(searchValue) {
    this.listingOptionProductCategory.search = searchValue;
    this.productCategoryService.getDropdown(this.listingOptionProductCategory).subscribe(async it => {
      this.form.setProductCategoriesList(it.data.result);
    });
  }
  async onSearchChanged({ search, page, clear = false }: { search: string; page: number, clear: boolean }) {
    this.isDropdownLoading = true;
    this.listingOptionProductCategory.search = search;
    this.listingOptionProductCategory.page = page;
    if (clear) {
      this.form.setProductCategoriesList([]);
    }
    this.productCategoryService.getDropdown(this.listingOptionProductCategory).subscribe(it => {
      this.hasMore = page * it.data.totalRecords < it.data.totalPages * it.data.totalRecords;
      const list = clear? it.data.result : [...this.form.productCategoriesList, ...it.data.result];
      this.form.setProductCategoriesList(list);
      this.isDropdownLoading = false;
    });
  }
  
  onSelectionChange(selected): void {
    this.listingOptionProductCategory.exclude = selected.item;
    this.form.form.get('parentProductCategoryId').setValue(selected.item);
    // this.refresh();
  }
  cancel() {
    this.activeModal.dismiss(false);
  }
}
