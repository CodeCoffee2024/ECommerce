import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPermissionActivityLogComponent } from './user-permission-activity-log.component';

describe('UserPermissionActivityLogComponent', () => {
  let component: UserPermissionActivityLogComponent;
  let fixture: ComponentFixture<UserPermissionActivityLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserPermissionActivityLogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserPermissionActivityLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
