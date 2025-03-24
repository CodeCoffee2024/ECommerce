import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPermissionNewComponent } from './user-permission-new.component';

describe('UserPermissionNewComponent', () => {
  let component: UserPermissionNewComponent;
  let fixture: ComponentFixture<UserPermissionNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserPermissionNewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserPermissionNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
