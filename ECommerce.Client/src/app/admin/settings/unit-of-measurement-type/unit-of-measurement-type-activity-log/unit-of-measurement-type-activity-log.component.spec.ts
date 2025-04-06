import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementTypeActivityLogComponent } from './unit-of-measurement-type-activity-log.component';

describe('UnitOfMeasurementTypeActivityLogComponent', () => {
  let component: UnitOfMeasurementTypeActivityLogComponent;
  let fixture: ComponentFixture<UnitOfMeasurementTypeActivityLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementTypeActivityLogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementTypeActivityLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
