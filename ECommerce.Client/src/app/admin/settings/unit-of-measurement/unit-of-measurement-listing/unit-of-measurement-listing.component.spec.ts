import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementListingComponent } from './unit-of-measurement-listing.component';

describe('UnitOfMeasurementListingComponent', () => {
  let component: UnitOfMeasurementListingComponent;
  let fixture: ComponentFixture<UnitOfMeasurementListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementListingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
