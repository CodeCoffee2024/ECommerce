import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPermissionActivityLogShowComponent } from './user-permission-activity-log-show.component';

describe('UserPermissionActivityLogShowComponent', () => {
  let component: UserPermissionActivityLogShowComponent;
  let fixture: ComponentFixture<UserPermissionActivityLogShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserPermissionActivityLogShowComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserPermissionActivityLogShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
