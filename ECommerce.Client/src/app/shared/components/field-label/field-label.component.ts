import { Component, Input } from '@angular/core';
import { dateToString } from '../../../utils/string.util';

@Component({
  selector: 'app-field-label',
  templateUrl: './field-label.component.html',
  styleUrl: './field-label.component.scss'
})
export class FieldLabelComponent {
  @Input() description = '';
  @Input() value; 
  @Input() type = 'string'; 
  @Input() isNullable = false; 

  get labelValue() {
    if (this.type == 'date') {
      return dateToString(this.value);
    }
    return this.value;
  }
}
