import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementTypeListingComponent } from './unit-of-measurement-type-listing.component';

describe('UnitOfMeasurementTypeListingComponent', () => {
  let component: UnitOfMeasurementTypeListingComponent;
  let fixture: ComponentFixture<UnitOfMeasurementTypeListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementTypeListingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementTypeListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
