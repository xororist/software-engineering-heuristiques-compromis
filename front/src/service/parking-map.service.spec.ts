import { TestBed } from '@angular/core/testing';

import { ParkingMapService } from './parking-map.service';

describe('ParkingMapService', () => {
  let service: ParkingMapService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ParkingMapService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
