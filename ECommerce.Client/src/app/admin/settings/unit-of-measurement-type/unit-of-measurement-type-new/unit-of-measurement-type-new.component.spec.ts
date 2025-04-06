import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementTypeNewComponent } from './unit-of-measurement-type-new.component';

describe('UnitOfMeasurementTypeNewComponent', () => {
  let component: UnitOfMeasurementTypeNewComponent;
  let fixture: ComponentFixture<UnitOfMeasurementTypeNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementTypeNewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementTypeNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
