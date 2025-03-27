import { Component, Input } from '@angular/core';
import { dateToString, formatNullable } from '../../../utils/string.util';

@Component({
  selector: 'app-audit',
  templateUrl: './audit.component.html',
  styleUrl: './audit.component.scss'
})
export class AuditComponent {
  @Input() entity;

  get createdBy() {
    return formatNullable(this.entity.createdBy.name);
  }
  get createdDate() {
    return this.entity ? dateToString(this.entity.createdDate, true) : "--";
  }
  get modifiedBy() {
    return formatNullable(this.entity.modifiedBy.name);
  }
  get modifiedDate() {
    return this.entity ? dateToString(this.entity.modifiedDate, true) : "--";
  }
}
