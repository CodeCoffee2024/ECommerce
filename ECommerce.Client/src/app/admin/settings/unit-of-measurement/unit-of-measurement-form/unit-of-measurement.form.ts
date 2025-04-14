import { FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { BaseModel } from "../../../../models/base.model";
import { UnitOfMeasurementResult } from "../../../../models/settings/unit-of-measurement/unit-of-measurement";
import { UnitOfMeasurementTypeFragment } from "../../../../models/settings/unit-of-measurement-type/unit-of-measurement-type";
import { UnitOfMeasurementListingFragmentResponse } from "../../../../models/settings/unit-of-measurement/unit-of-measurement-listing-response";

export class UnitOfMeasurementForm extends BaseModel {
    unitOfMeasurementTypesList: UnitOfMeasurementTypeFragment[] = [];
    constructor(private fb: FormBuilder = new FormBuilder()) {
        super();
    }
    initializeForm() {
        this.fb = new FormBuilder();
        const form =
        this.fb.group(
            {
                name: ['', [Validators.required, this.requiredNoWhitespace]],
                abbreviation: ['', [Validators.required]],
                unitOfMeasurementType: ['', [Validators.required]],
                conversions: this.fb.array([]) // Empty array to start with
            },
        )
        this.setForm(form);
    }
    get conversions(): FormArray {
        return this.form.get('conversions') as FormArray;
    }
    newConversion(data): FormGroup {
        return this.fb.group({
            from: [data?.from || '', Validators.required],
            to: [data?.to || '', Validators.required],
            value: [data?.factor || '', [Validators.required, Validators.pattern(/^\d+(\.\d+)?$/)]],
        });
    }
    removeConversion(index: number): void {
        this.conversions.removeAt(index);
    }
    addConversion(data = null): void {
        this.conversions.push(this.newConversion(data));
    }
    setUnitOfMeasurementTypesList(unitOfMeasurementTypes: UnitOfMeasurementTypeFragment[]) {
        this.unitOfMeasurementTypesList = unitOfMeasurementTypes;
    }
    get form() {
        return this.formGroup;
    }
    
    get submitData() {
        return {
            name: this.form.get('name')?.value,
            abbreviation: this.form.get('abbreviation')?.value,
            unitOfMeasurementTypeId: this.form.get('unitOfMeasurementType')?.value?.id
        };
    }
    
    fill(result: UnitOfMeasurementResult, conversions: UnitOfMeasurementListingFragmentResponse[]) {
        this.id = result.id;
        this.form.get('name').setValue(result.name);
        console.log(conversions);
        this.form.get('abbreviation').setValue(result.abbreviation);
        this.form.get('unitOfMeasurementType').setValue(result.unitOfMeasurementType);
    }
}