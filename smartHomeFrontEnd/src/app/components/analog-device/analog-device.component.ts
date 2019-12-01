import { Component, OnInit, Input } from '@angular/core';
import { WsService } from 'src/app/services/ws/ws.service';
import { AnalogDeviceService } from 'src/app/services/analogDeviceService/analog-device.service';

@Component({
  selector: 'app-analog-device',
  templateUrl: './analog-device.component.html',
  styleUrls: ['./analog-device.component.css']
})
export class AnalogDeviceComponent implements OnInit {
  @Input() analogDeviceId: number;

  constructor(private wsService: WsService, private analogDeviceService: AnalogDeviceService) { }

  ngOnInit() {
  }

  show(val: number) {
    console.log("trigger " + val);
  }

  setValue(): number {
    return this.analogDeviceService.analogDevices[this.analogDeviceId].value;
  }

  updateValue(id: number, value: number): void {
    this.wsService.updateValue(id, value);
  }

  updateValueUI(ref, value: number): void {
    ref.value = value;
  }
}
