import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementActivityLogShowComponent } from './unit-of-measurement-activity-log-show.component';

describe('UnitOfMeasurementActivityLogShowComponent', () => {
  let component: UnitOfMeasurementActivityLogShowComponent;
  let fixture: ComponentFixture<UnitOfMeasurementActivityLogShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementActivityLogShowComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementActivityLogShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
