import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchMonoSelectComponent } from './search-mono-select.component';

describe('SearchMonoSelectComponent', () => {
  let component: SearchMonoSelectComponent;
  let fixture: ComponentFixture<SearchMonoSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SearchMonoSelectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SearchMonoSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
