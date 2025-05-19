import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCategoryUpdateComponent } from './product-category-update.component';

describe('ProductCategoryUpdateComponent', () => {
  let component: ProductCategoryUpdateComponent;
  let fixture: ComponentFixture<ProductCategoryUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductCategoryUpdateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProductCategoryUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
