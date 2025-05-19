import { FormBuilder, Validators } from "@angular/forms";
import { BaseModel } from "../../../../models/base.model";
import { ProductCategoryFragment, ProductCategoryResult } from "../../../../models/inventory/product-category/product-category";

export class ProductCategoryForm extends BaseModel {
    productCategoriesList: ProductCategoryFragment[] = [];
    constructor(private fb: FormBuilder = new FormBuilder()) {
        super();
    }
    initializeForm() {
        this.fb = new FormBuilder();
        const form =
        this.fb.group(
            {
                name: ['', [Validators.required, this.requiredNoWhitespace]],
                isSubCategory: [true],
                parentProductCategoryId: [''],
            },
        )
        this.setForm(form);
    }
    
    get form() {
        return this.formGroup;
    }
    
    get submitData() {
        return {
            name: this.form.get('name')?.value,
            isSubCategory: this.form.get('isSubCategory')?.value,
            parentProductCategoryId: this.form.get('parentProductCategoryId')?.value == "" ? null : this.form.get('parentProductCategoryId')?.value,
        };
    }
    
    setProductCategoriesList(productCategoriesList: ProductCategoryFragment[]) {
        this.productCategoriesList = productCategoriesList;
    }
    
    fill(result: ProductCategoryResult) {
        this.id = result.id;
        this.form.get('name').setValue(result.name);
        this.form.get('isSubCategory').setValue(result.isSubCategory);
        this.form.get('parentProductCategoryId').setValue(result.parentProductCategoryId);
    }
}