import { Injectable } from '@angular/core';
import { DeviceModel } from 'src/app/models/deviceModel';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  public devices: object;

  constructor() {
    this.devices = {};
  }

  init(devices: DeviceModel[]) {
    this.devices = {};
    for (const i of devices) {
      this.devices[i.deviceId] = i;
    }
  }

  // switchDevice(id: number) {
  //   this.devices[id].relaySwitch = !this.devices[id].relaySwitch;
  // }

  updateDevice(device: DeviceModel) {
    this.devices[device.deviceId].relaySwitch = device.relaySwitch;
  }
}
