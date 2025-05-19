import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCategoryShowComponent } from './product-category-show.component';

describe('ProductCategoryShowComponent', () => {
  let component: ProductCategoryShowComponent;
  let fixture: ComponentFixture<ProductCategoryShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductCategoryShowComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProductCategoryShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
