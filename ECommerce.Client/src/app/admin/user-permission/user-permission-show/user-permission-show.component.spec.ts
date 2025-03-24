import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPermissionShowComponent } from './user-permission-show.component';

describe('UserPermissionShowComponent', () => {
  let component: UserPermissionShowComponent;
  let fixture: ComponentFixture<UserPermissionShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserPermissionShowComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserPermissionShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
