import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementTypeComponent } from './unit-of-measurement-type.component';

describe('UnitOfMeasurementTypeComponent', () => {
  let component: UnitOfMeasurementTypeComponent;
  let fixture: ComponentFixture<UnitOfMeasurementTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementTypeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
