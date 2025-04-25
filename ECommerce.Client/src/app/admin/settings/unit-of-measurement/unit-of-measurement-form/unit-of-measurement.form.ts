import { FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { BaseModel } from "../../../../models/base.model";
import { UnitOfMeasurementFragment, UnitOfMeasurementResult } from "../../../../models/settings/unit-of-measurement/unit-of-measurement";
import { UnitOfMeasurementTypeFragment } from "../../../../models/settings/unit-of-measurement-type/unit-of-measurement-type";
import { UnitOfMeasurementListingFragmentResponse } from "../../../../models/settings/unit-of-measurement/unit-of-measurement-listing-response";

export class UnitOfMeasurementForm extends BaseModel {
    unitOfMeasurementTypesList: UnitOfMeasurementTypeFragment[] = [];
    unitOfMeasurementList: UnitOfMeasurementFragment[] = [];
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
    get convertToValues() {
        if (this.conversions instanceof FormArray) {
            const controls = this.conversions.controls;
    
            return controls
                .filter((control, index) => {
                    const { value, unitOfMeasurementTo } = control.value;
    
                    const isEmpty = value == null || unitOfMeasurementTo?.id == null;
                    if (index === controls.length - 1 && control.dirty && isEmpty) {
                        return false;
                    }
    
                    // For all other items — only include if not empty
                    return !isEmpty;
                })
                .map(control => {
                    const { id, value, unitOfMeasurementTo } = control.value;
                    return {
                        id,
                        value,
                        unitOfMeasurementTo: unitOfMeasurementTo?.id ?? null
                    };
                });
        }
    
        return null;
    }
    conversion(name): FormGroup {
        return name as FormGroup;
    }
    newConversion(data): FormGroup {
        return this.fb.group({
            id: [data?.id || null],
            unitOfMeasurementTo: [data?.unitOfMeasurementTo || '', Validators.required],
            value: [data?.value || '', [Validators.required, Validators.pattern(/^\d+(\.\d+)?$/)]],
        });
    }
    removeConversion(index: number): void {
        if (this.conversions.length <= 1) return; // prevent removing the last one
        this.conversions.removeAt(index);
    }
    addConversion(data = null): void {
        this.conversions.push(this.newConversion(data));
    }
    setUnitOfMeasurementTypesList(unitOfMeasurementTypes: UnitOfMeasurementTypeFragment[]) {
        this.unitOfMeasurementTypesList = unitOfMeasurementTypes;
    }

    setUnitOfMeasurementsList(unitOfMeasurement: UnitOfMeasurementFragment[]) {
        this.unitOfMeasurementList = unitOfMeasurement;
    }

    canAddConversion(): boolean {
        const controls = this.conversions;
        if (controls.length === 0) {
            return true;
        }
        const lastGroup = controls.at(controls.length - 1);
        return lastGroup.dirty && lastGroup.valid;
    }
    get form() {
        return this.formGroup;
    }
    
    get submitData() {
        return {
            name: this.form.get('name')?.value,
            abbreviation: this.form.get('abbreviation')?.value,
            unitOfMeasurementTypeId: this.form.get('unitOfMeasurementType')?.value?.id,
            conversions: this.convertToValues
        };
    }
    
    fill(result: UnitOfMeasurementResult, conversions: UnitOfMeasurementListingFragmentResponse[]) {
        this.id = result.id;
        this.form.get('name').setValue(result.name);
        this.form.get('abbreviation').setValue(result.abbreviation);
        this.form.get('unitOfMeasurementType').setValue(result.unitOfMeasurementType);
        result.conversionsFrom.forEach(it => {
            this.conversions.push(this.newConversion(it));
        })
        this.conversions.push(this.newConversion(null));

    }
}