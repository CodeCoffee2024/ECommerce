import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchMonoComponent } from './search-mono.component';

describe('SearchMonoComponent', () => {
  let component: SearchMonoComponent;
  let fixture: ComponentFixture<SearchMonoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SearchMonoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SearchMonoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
