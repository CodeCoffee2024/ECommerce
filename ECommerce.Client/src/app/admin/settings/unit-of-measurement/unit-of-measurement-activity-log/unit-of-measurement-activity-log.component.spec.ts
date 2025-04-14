import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementActivityLogComponent } from './unit-of-measurement-activity-log.component';

describe('UnitOfMeasurementActivityLogComponent', () => {
  let component: UnitOfMeasurementActivityLogComponent;
  let fixture: ComponentFixture<UnitOfMeasurementActivityLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementActivityLogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementActivityLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
