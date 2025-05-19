import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCategoryNewComponent } from './product-category-new.component';

describe('ProductCategoryNewComponent', () => {
  let component: ProductCategoryNewComponent;
  let fixture: ComponentFixture<ProductCategoryNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductCategoryNewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProductCategoryNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
