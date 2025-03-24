import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';

@Component({
  selector: 'app-search-mono',
  templateUrl: './search-mono.component.html',
  styleUrl: './search-mono.component.scss'
})
export class SearchMonoComponent implements OnInit {

  @Input() placeholder: string;
  @Input() searchModel: string; // ✅ Two-way bound model
  @Output() searchModelChange = new EventEmitter<string>(); // ✅ Emits changes
  @Output() searchChanged = new EventEmitter<string>(); // ✅ Emits debounced search input

  searchForm: FormGroup = new FormGroup({
    searchTerm: new FormControl('')
  });

  private searchSubject = new Subject<string>();

  ngOnInit() {
    this.searchForm.get('searchTerm')?.setValue(this.searchModel); // ✅ Set initial value

    // ✅ Emit debounced search values
    this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(searchText => {
      this.searchChanged.emit(searchText);
      this.searchModelChange.emit(searchText);
    });

    // ✅ Listen for input changes
    this.searchForm.get('searchTerm')?.valueChanges.subscribe(value => {
      this.searchSubject.next(value || '');
    });
  }
}