import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCategoryListingComponent } from './product-category-listing.component';

describe('ProductCategoryListingComponent', () => {
  let component: ProductCategoryListingComponent;
  let fixture: ComponentFixture<ProductCategoryListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductCategoryListingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProductCategoryListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
