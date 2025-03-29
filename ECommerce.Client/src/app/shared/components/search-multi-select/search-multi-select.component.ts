import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { debounceTime, Subject } from 'rxjs';

@Component({
  selector: 'app-search-multi-select',
  templateUrl: './search-multi-select.component.html',
  styleUrls: ['./search-multi-select.component.scss'],
})
export class SearchMultiSelectComponent {
  @Input() label = 'Search';
  @Input() placeholder = 'Type to search...';
  @Input() isLoading = false;
  @Input() field: string;
  @Input() hasSearchIcon = false;
  @Input() searchOptions: string[] = []; // Options must be passed from parent
  @Output() searchChanged = new EventEmitter<{ search: string; page: number, clear: boolean }>(); // Emits search event
  @Output() selectedItemsChange = new EventEmitter<[]>(); // Emits selected items

  @ViewChild('dropdownMenu', { static: false }) dropdownMenu!: ElementRef;
  @ViewChild('searchInput', { static: false }) searchInput!: ElementRef;

  searchQuery = '';
  @Input() selectedItems;
  @Input() hasMore = false;
  clear = false;
  page = 1;
  focused = false;
  searchSubject = new Subject<string>();

  constructor() {
    this.selectedItems = this.selectedItems ?? [];
    this.searchSubject.pipe(debounceTime(300)).subscribe(query => {
      console.log(query)
      this.page = 1; // Reset page
      this.clear = true;
      this.emitSearch();
    });
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
    console.log(this.selectedItems);
    if (!this.selectedItems.includes(item)) {
      this.selectedItems.push(item);
      this.selectedItemsChange.emit(this.selectedItems);
    }
    this.searchQuery = '';
  }

  /** Removes a selected item */
  removeItem(item): void {
    this.selectedItems = this.selectedItems.filter(i => i[this.field] !== item[this.field]);
    this.selectedItemsChange.emit(this.selectedItems);
  }
}
