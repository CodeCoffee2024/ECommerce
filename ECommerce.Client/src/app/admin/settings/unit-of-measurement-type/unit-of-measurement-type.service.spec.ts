import { TestBed } from '@angular/core/testing';

import { UnitOfMeasurementTypeService } from './unit-of-measurement-type.service';

describe('UnitOfMeasurementTypeService', () => {
  let service: UnitOfMeasurementTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UnitOfMeasurementTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
