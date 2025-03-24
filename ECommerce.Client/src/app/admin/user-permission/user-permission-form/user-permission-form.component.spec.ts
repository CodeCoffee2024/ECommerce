import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPermissionFormComponent } from './user-permission-form.component';

describe('UserPermissionFormComponent', () => {
  let component: UserPermissionFormComponent;
  let fixture: ComponentFixture<UserPermissionFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserPermissionFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserPermissionFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
