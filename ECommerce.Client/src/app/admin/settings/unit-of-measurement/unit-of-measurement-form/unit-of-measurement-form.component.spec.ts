import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementFormComponent } from './unit-of-measurement-form.component';

describe('UnitOfMeasurementFormComponent', () => {
  let component: UnitOfMeasurementFormComponent;
  let fixture: ComponentFixture<UnitOfMeasurementFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
