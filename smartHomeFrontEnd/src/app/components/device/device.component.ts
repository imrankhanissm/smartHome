import { Component, OnInit, Input } from '@angular/core';
import { WsService } from 'src/app/services/ws/ws.service';
import { HttpService } from 'src/app/services/http/http.service';
import { DeviceService } from 'src/app/services/device/device.service';

@Component({
  selector: 'app-device',
  templateUrl: './device.component.html',
  styleUrls: ['./device.component.css']
})
export class DeviceComponent implements OnInit {
  @Input() deviceId: number;

  constructor(private wsService: WsService, private httpService: HttpService, private deviceService: DeviceService) { }

  ngOnInit() {
  }

  setClass() {
    const classes = {
      Switch: true,
      SwitchOn: this.deviceService.devices[this.deviceId].relaySwitch,
      SwitchOff: !this.deviceService.devices[this.deviceId].relaySwitch
    };

    return classes;
  }

  switch() {
    // this.httpService.switch(this.deviceId);
    this.wsService.switch(this.deviceId);
  }
}
