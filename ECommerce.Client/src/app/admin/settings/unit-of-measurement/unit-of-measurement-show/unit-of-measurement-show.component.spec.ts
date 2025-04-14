import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementShowComponent } from './unit-of-measurement-show.component';

describe('UnitOfMeasurementShowComponent', () => {
  let component: UnitOfMeasurementShowComponent;
  let fixture: ComponentFixture<UnitOfMeasurementShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementShowComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
