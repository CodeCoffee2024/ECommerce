import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementTypeUpdateComponent } from './unit-of-measurement-type-update.component';

describe('UnitOfMeasurementTypeUpdateComponent', () => {
  let component: UnitOfMeasurementTypeUpdateComponent;
  let fixture: ComponentFixture<UnitOfMeasurementTypeUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementTypeUpdateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementTypeUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
