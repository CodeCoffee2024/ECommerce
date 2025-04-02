import { Component, Input } from '@angular/core';
import { GenericActivityLogResult } from '../../../models/generics/generic-activity-log-result';

@Component({
  selector: 'app-activity-log-entity',
  templateUrl: './activity-log-entity.component.html',
  styleUrl: './activity-log-entity.component.scss'
})
export class ActivityLogEntityComponent {
  @Input() data: GenericActivityLogResult;
  @Input() id: string;
  @Input() entity: string;
  @Input() route: string;
  constructor() {
  }
  get oldValues() {
    return this.data.oldValues;
  }
  get newValues() {
    return this.data.newValues;
  }
  getClass(key: string): string {
    const oldVal = this.oldValues?.[key];
    const newVal = this.newValues?.[key];
  
    if (!oldVal && newVal) return 'bg-success'; // New value
    if (oldVal && newVal && oldVal !== newVal) return 'bg-warning'; // Changed value
    if (oldVal && !newVal) return 'bg-danger'; // Removed value
    return ''; // No change
  }
  displayChange(key) {
    
    const oldVal = this.oldValues?.[key];
    const newVal = this.newValues?.[key];
  
    if (!oldVal && newVal) return `<span class="rounded bg-success pl-2 pr-2">${newVal}</span>`; // New value
    if (oldVal && newVal && oldVal !== newVal) return `<span class="rounded bg-warning mr-2 pl-2 pr-2">${oldVal}</span><span class="rounded bg-success pl-2 pr-2">${newVal}</span>`; // Changed value
    if (oldVal && !newVal) return `<span class="rounded bg-danger pl-2 pr-2">${oldVal}</span> --`; // Removed value
    return `${newVal}`; // No change
  }
  get keys() {
    const oldKeys = this.oldValues ? Object.keys(this.oldValues) : [];
    const newKeys = this.newValues ? Object.keys(this.newValues) : [];
    return Array.from(new Set([...oldKeys, ...newKeys]));
  }
}
