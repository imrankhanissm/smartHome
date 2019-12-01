import { Component, OnInit } from '@angular/core';
import { WsService } from 'src/app/services/ws/ws.service';
import { HttpService } from 'src/app/services/http/http.service';
import { DeviceService } from 'src/app/services/device/device.service';
import { AnalogDeviceService } from 'src/app/services/analogDeviceService/analog-device.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private wsService: WsService, private httpService: HttpService, private deviceService: DeviceService, private analogDeviceService: AnalogDeviceService) { }

  ngOnInit() {
    this.wsService.connect();
  }

  logout(): void {
    this.httpService.logout();
  }
}
