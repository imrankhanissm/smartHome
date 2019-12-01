import { Component, OnInit, DefaultIterableDiffer } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { WsService } from './services/ws/ws.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'smartHomeFrontEnd';

  constructor(public wsService: WsService) { }

  ngOnInit() {
  }
}
