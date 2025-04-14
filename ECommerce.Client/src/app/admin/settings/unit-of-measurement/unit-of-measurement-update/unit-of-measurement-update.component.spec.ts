import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementUpdateComponent } from './unit-of-measurement-update.component';

describe('UnitOfMeasurementUpdateComponent', () => {
  let component: UnitOfMeasurementUpdateComponent;
  let fixture: ComponentFixture<UnitOfMeasurementUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementUpdateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
