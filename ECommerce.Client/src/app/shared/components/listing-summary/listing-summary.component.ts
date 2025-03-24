import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GenericListingResult } from '../../../models/generics/generic-listing-result';

@Component({
  selector: 'app-listing-summary',
  templateUrl: './listing-summary.component.html',
  styleUrl: './listing-summary.component.scss'
})
export class ListingSummaryComponent {
  @Input() listingData: GenericListingResult<unknown>;
  @Output() changePage = new EventEmitter<number>();
  onPageChange(page) {
    this.changePage.emit(page);
  }
}
