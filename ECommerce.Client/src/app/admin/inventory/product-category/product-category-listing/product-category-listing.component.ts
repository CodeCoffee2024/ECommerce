import { Component, OnInit } from '@angular/core';
import { finalize, takeUntil } from 'rxjs';
import { LoginService } from '../../../../login/login.service';
import { GenericListingResult } from '../../../../models/generics/generic-listing-result';
import { FormatProductCategoryStatus, ProductCategoryPermission } from '../../../../models/inventory/product-category/product-category';
import { ProductCategoryListingResponse } from '../../../../models/inventory/product-category/product-category-listing-response';
import { Failed, NotFound } from '../../../../models/message';
import { ToastType } from '../../../../models/toast';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { LoadingService } from '../../../../shared/services/loading/loading.service';
import { ModalService } from '../../../../shared/services/modal/modal.service';
import { TitleService } from '../../../../shared/services/title/title.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { ProductCategoryService } from '../../product-category.service';
import { ProductCategoryForm } from '../product-category-form/product-category.form';
import { ProductCategoryShowComponent } from '../product-category-show/product-category-show.component';
import { ProductCategoryListingOption } from './product-category-listing.option';
import { ProductCategoryFormComponent } from '../product-category-form/product-category-form.component';

@Component({
  selector: 'app-product-category-listing',
  templateUrl: './product-category-listing.component.html',
  styleUrl: './product-category-listing.component.scss'
})
export class ProductCategoryListingComponent extends BaseComponent implements OnInit {
  title = 'Product Categories';
  isDropdownOpen = false;
  isDropdownLoading = false;
  userPermissions = [];
  statuses = [];
  hasMore = false;
  UserEnableToModifyProductCategory = ProductCategoryPermission.UserEnableToModifyProductCategory; 
  form: ProductCategoryForm = new ProductCategoryForm();
  listingFormat: ProductCategoryListingOption[];
  FormatStatus = FormatProductCategoryStatus;
  constructor(
    private authService: LoginService,
    private titleService: TitleService,
    protected productCategoryService: ProductCategoryService,
    private loadingService: LoadingService,
    private modalService: ModalService,
    private toastService: ToastService,
  ) {
    super(authService, titleService, loadingService);
    this.titleService.setTitle(this.title);
    this.setGenerics(
      new ProductCategoryListingOption(),
    'ProductCategory',
      this.productCategoryService,
      GenericListingResult<ProductCategoryListingResponse[]>,
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
    this.productCategoryService.getStatuses()
      .pipe(takeUntil(this.destroy$))
      .subscribe(res => {
        console.log(res);
        this.statuses = res.data;
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
    const result = await this.modalService.open(ProductCategoryFormComponent, {form: this.form});
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
      this.toastService.add("Error", NotFound("Product Category"), ToastType.ERROR);
    } else {
      this.productCategoryService.show(id)
      .pipe(finalize(() => this.loadingService.hide()))
      .subscribe({
        next: async (result) => {
          this.loadingService.hide();
          const isSuccess = await this.modalService.open(ProductCategoryShowComponent, {productCategory: result.data});
          if (isSuccess) {
            this.refresh();
          }
        },
        error: () => {
          this.toastService.add("Error", Failed("Product Category"), ToastType.ERROR);
        }
      });
    }
  }
}
