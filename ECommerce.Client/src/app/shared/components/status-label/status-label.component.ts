import { Component, Input } from '@angular/core';
import { EntityStatuses, FormatStatus } from '../../../models/status';

@Component({
  selector: 'app-status-label',
  template: '<div [innerHTML]="statusLabel"></div>',
})
export class StatusLabelComponent {
  @Input() status;
  get statusLabel() {
    return FormatStatus.format(this.status, true);
  }
}
