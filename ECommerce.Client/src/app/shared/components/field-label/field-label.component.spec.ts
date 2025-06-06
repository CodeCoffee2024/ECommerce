import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldLabelComponent } from './field-label.component';

describe('FieldLabelComponent', () => {
  let component: FieldLabelComponent;
  let fixture: ComponentFixture<FieldLabelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FieldLabelComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FieldLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
