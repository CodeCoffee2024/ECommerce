import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementUpdateFormComponent } from './unit-of-measurement-update-form.component';

describe('UnitOfMeasurementUpdateFormComponent', () => {
  let component: UnitOfMeasurementUpdateFormComponent;
  let fixture: ComponentFixture<UnitOfMeasurementUpdateFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementUpdateFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementUpdateFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
