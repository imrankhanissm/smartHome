import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnalogDeviceComponent } from './analog-device.component';

describe('AnalogDeviceComponent', () => {
  let component: AnalogDeviceComponent;
  let fixture: ComponentFixture<AnalogDeviceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnalogDeviceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnalogDeviceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
