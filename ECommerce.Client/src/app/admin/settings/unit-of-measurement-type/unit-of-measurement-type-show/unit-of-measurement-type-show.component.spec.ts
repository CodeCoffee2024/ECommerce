import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementTypeShowComponent } from './unit-of-measurement-type-show.component';

describe('UnitOfMeasurementTypeShowComponent', () => {
  let component: UnitOfMeasurementTypeShowComponent;
  let fixture: ComponentFixture<UnitOfMeasurementTypeShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementTypeShowComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementTypeShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
