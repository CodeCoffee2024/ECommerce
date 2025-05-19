import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCategoryActivityLogShowComponent } from './product-category-activity-log-show.component';

describe('ProductCategoryActivityLogShowComponent', () => {
  let component: ProductCategoryActivityLogShowComponent;
  let fixture: ComponentFixture<ProductCategoryActivityLogShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductCategoryActivityLogShowComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProductCategoryActivityLogShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
