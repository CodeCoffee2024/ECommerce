import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime, Subject } from 'rxjs';

@Component({
  selector: 'app-search-mono-select',
  templateUrl: './search-mono-select.component.html',
  styleUrl: './search-mono-select.component.scss'
})
export class SearchMonoSelectComponent {
  @Input() label = 'Search';
  @Input() placeholder = 'Type to search...';
  @Input() isLoading = false;
  @Input() field: string;
  @Input() hasSearchIcon = false;
  @Input() controlName;
  @Input() searchOptions = []; // Options must be passed from parent
  @Output() searchChanged = new EventEmitter<{ search: string; page: number, clear: boolean }>(); // Emits search event
  @Output() selectedItemChange = new EventEmitter<string>(); // Emits selected items

  @ViewChild('dropdownMenu', { static: false }) dropdownMenu!: ElementRef;
  @ViewChild('searchInput', { static: false }) searchInput!: ElementRef;

  searchQuery = '';
  @Input() selectedItem;
  @Input() formGroup;
  @Input() hasMore = false;
  clear = false;
  page = 1;
  focused = false;
  searchSubject = new Subject<string>();

  constructor() {
    this.selectedItem = this.selectedItem ?? null;
    this.searchSubject.pipe(debounceTime(300)).subscribe(() => {
      this.page = 1; // Reset page
      this.clear = true;
      this.emitSearch();
    });
  }
  get formControl(): FormControl {
    return this.formGroup?.get(this.controlName) as FormControl;
  }
  isFocused(focus) {
    this.focused = focus;
  }
  /** Detect scroll to bottom */
  onScroll(): void {
    if (!this.dropdownMenu) return;

    const { scrollTop, scrollHeight, clientHeight } = this.dropdownMenu.nativeElement;
    if (scrollTop + clientHeight >= scrollHeight - 10) {
      this.loadMore();
    }
  }

  /** Called when user types */
  onSearch(): void {
    this.isLoading = true;
    this.searchSubject.next(this.searchQuery);
  }

  /** Emits search event */
  emitSearch(): void {
    this.searchChanged.emit({ search: this.searchQuery, page: this.page, clear: this.clear });
  }

  /** Loads more results (infinite scroll) */
  loadMore(): void {
    if (!this.hasMore || this.isLoading) return;
    this.clear = false;
    this.page++;
    this.emitSearch();
  }

  /** Selects an item */
  selectItem(item): void {
    this.selectedItemChange.emit(item);
    this.searchQuery = '';
  }

  /** Removes a selected item */
  clearSelection(): void {
    this.selectedItemChange.emit(null);
  }
  get errors(): string[] {
    if (!this.formControl) return [];
    const controlErrors = this.formControl.errors || {};
  
    const errorMessages: string[] = [];
    Object.keys(controlErrors).forEach(errorKey => {
      if (errorKey === 'required') {
        errorMessages.push(`${this.label} is required`);
      } else if (errorKey === 'serverError') {
        errorMessages.push(controlErrors['serverError']);
      } else {
        errorMessages.push(`${this.label} ${errorKey.replace(/([A-Z])/g, ' $1').toLowerCase()}`);
      }
    });
    return errorMessages;
  }
  get isRequired(): boolean {
    return !!this.formControl?.errors?.['required'];
  }
  
}
