import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UnitOfMeasurementTypeActivityLogShowComponent } from './unit-of-measurement-type-activity-log-show.component';


describe('UnitOfMeasurementTypeActivityLogShowComponent', () => {
  let component: UnitOfMeasurementTypeActivityLogShowComponent;
  let fixture: ComponentFixture<UnitOfMeasurementTypeActivityLogShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementTypeActivityLogShowComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UnitOfMeasurementTypeActivityLogShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
