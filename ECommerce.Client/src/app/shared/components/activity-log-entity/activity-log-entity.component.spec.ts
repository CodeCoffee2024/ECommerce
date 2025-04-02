import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivityLogEntityComponent } from './activity-log-entity.component';

describe('ActivityLogEntityComponent', () => {
  let component: ActivityLogEntityComponent;
  let fixture: ComponentFixture<ActivityLogEntityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ActivityLogEntityComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ActivityLogEntityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
