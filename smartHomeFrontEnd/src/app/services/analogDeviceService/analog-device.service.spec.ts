import { TestBed } from '@angular/core/testing';

import { AnalogDeviceService } from './analog-device.service';

describe('AnalogDeviceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AnalogDeviceService = TestBed.get(AnalogDeviceService);
    expect(service).toBeTruthy();
  });
});
