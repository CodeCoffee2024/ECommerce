import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ThHeaderComponent } from './th-header.component';

describe('ThHeaderComponent', () => {
  let component: ThHeaderComponent;
  let fixture: ComponentFixture<ThHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ThHeaderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ThHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
