import { Component, Input, OnInit } from '@angular/core';
import { ProductCategoryResult } from '../../../../models/inventory/product-category/product-category';
import { ProductCategoryForm } from '../product-category-form/product-category.form';
import { ProductCategoryService } from '../../product-category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoadingService } from '../../../../shared/services/loading/loading.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { ModalService } from '../../../../shared/services/modal/modal.service';
import { DeleteSuccess, DisableSuccess, EnableSuccess, NotFound } from '../../../../models/message';
import { ToastType } from '../../../../models/toast';
import { finalize } from 'rxjs';
import { DeleteConfirmationDialog, DisableConfirmationDialog, EnableConfirmationDialog } from '../../../../models/confirmation-dialog';

@Component({
  selector: 'app-product-category-show',
  templateUrl: './product-category-show.component.html',
  styleUrl: './product-category-show.component.scss'
})
export class ProductCategoryShowComponent implements OnInit {
  @Input() productCategory:ProductCategoryResult = new ProductCategoryResult();
  form: ProductCategoryForm;
  Id: string;
  constructor(
    private productCategoryService: ProductCategoryService,
    private router: Router,
    private loadingService: LoadingService,
    private toastService: ToastService,
    private activatedRoute: ActivatedRoute,
    private modalService: ModalService
  ) {

  }
  ngOnInit(): void {
    if (this.productCategory && this.productCategory.id) {
      // Already passed in, skip loading
      return;
    }
    this.loadingService.show();
    this.Id = this.activatedRoute.snapshot.paramMap.get('id') || null;
    if (!this.Id) {
      this.toastService.add("Error", NotFound("Product Category"), ToastType.ERROR);
      this.router.navigate(['/admin/inventory/product-categories']);
    } else {
      this.productCategoryService.show(this.Id)
      .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
      .subscribe({
        next: (result) => {
          this.productCategory = result.data;
        },
        error: () => {
          this.toastService.add("Error", NotFound("Product Category"), ToastType.ERROR);
          this.router.navigate(['/admin/inventory/product-categories']);
        }
      });
    }
  }
  goToLog() {
    this.router.navigate(['/admin/inventory/product-categories/activity-log/'+this.productCategory.id]);
    // this.cancel();
  }
  async delete() {
    const dialog = DeleteConfirmationDialog("Product Category");
    const result = await this.modalService.confirm(dialog);
    if (result) {
      this.productCategoryService.deleteProductCategory(this.productCategory.id).subscribe({
        next:() => {
          this.toastService.add("Success", DeleteSuccess("Product Category"), ToastType.SUCCESS);
          this.router.navigate(['/admin/inventory/product-categories/view/' + this.Id])
      } })
    }
  }
  goTo(data) {
    console.log(data)
  }
  async disable() {
    const dialog = DisableConfirmationDialog("Product Category");
    const result = await this.modalService.confirm(dialog);
    if (result) {
      this.productCategoryService.disable(this.productCategory.id).subscribe({
        next:() => {
          window.location.reload();
          this.toastService.add("Success", DisableSuccess("Product Category"), ToastType.SUCCESS);
      } })
    }
  }
  async enable() {
    const dialog = EnableConfirmationDialog("Product Category");
    const result = await this.modalService.confirm(dialog);
    if (result) {
      this.productCategoryService.enable(this.productCategory.id).subscribe({
        next:() => {
          window.location.reload();
          this.toastService.add("Success", EnableSuccess("Product Category"), ToastType.SUCCESS);
      } })
    }
  }
}