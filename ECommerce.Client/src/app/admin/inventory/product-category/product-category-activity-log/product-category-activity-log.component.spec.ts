import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCategoryActivityLogComponent } from './product-category-activity-log.component';

describe('ProductCategoryActivityLogComponent', () => {
  let component: ProductCategoryActivityLogComponent;
  let fixture: ComponentFixture<ProductCategoryActivityLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductCategoryActivityLogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProductCategoryActivityLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
