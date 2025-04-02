import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserActivityLogShowComponent } from './user-activity-log-show.component';

describe('UserActivityLogShowComponent', () => {
  let component: UserActivityLogShowComponent;
  let fixture: ComponentFixture<UserActivityLogShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserActivityLogShowComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserActivityLogShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
