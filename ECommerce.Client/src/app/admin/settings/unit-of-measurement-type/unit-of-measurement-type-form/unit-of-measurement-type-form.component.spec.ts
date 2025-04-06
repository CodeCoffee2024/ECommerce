import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementTypeFormComponent } from './unit-of-measurement-type-form.component';

describe('UnitOfMeasurementTypeFormComponent', () => {
  let component: UnitOfMeasurementTypeFormComponent;
  let fixture: ComponentFixture<UnitOfMeasurementTypeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementTypeFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementTypeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
