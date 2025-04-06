import { FormBuilder, Validators } from "@angular/forms";
import { BaseModel } from "../../../../models/base.model";
import { UnitOfMeasurementTypeResult } from "../../../../models/settings/unit-of-measurement-type/unit-of-measurement-type";

export class UnitOfMeasurementTypeForm extends BaseModel {
    constructor(private fb: FormBuilder = new FormBuilder()) {
        super();
    }
    initializeForm() {
        this.fb = new FormBuilder();
        const form =
        this.fb.group(
            {
                name: ['', [Validators.required, this.requiredNoWhitespace]],
                hasDecimal: [false]
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
            hasDecimal: this.form.get('hasDecimal')?.value
        };
    }
    
    fill(result: UnitOfMeasurementTypeResult) {
        this.id = result.id;
        this.form.get('name').setValue(result.name);
        this.form.get('hasDecimal').setValue(result.hasDecimal == "Yes" ? true : false);
    }
}