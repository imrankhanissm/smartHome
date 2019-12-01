import { Injectable } from '@angular/core';
import { AnalogDeviceModel } from 'src/app/models/analogDeviceModel';

@Injectable({
  providedIn: 'root'
})
export class AnalogDeviceService {
  public analogDevices: object;

  constructor() {
    this.analogDevices = {};
  }

  init(analogDevices: AnalogDeviceModel[]) {
    this.analogDevices = {};
    for (const i of analogDevices) {
      this.analogDevices[i.analogDeviceId] = i;
    }
  }

  updateAnalogDevice(analogDevice: AnalogDeviceModel) {
    this.analogDevices[analogDevice.analogDeviceId].value = analogDevice.value;
  }
}
