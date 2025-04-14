import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasurementNewComponent } from './unit-of-measurement-new.component';

describe('UnitOfMeasurementNewComponent', () => {
  let component: UnitOfMeasurementNewComponent;
  let fixture: ComponentFixture<UnitOfMeasurementNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementNewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
