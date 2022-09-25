import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { HttpService } from '../http/http.service';
import { DeviceService } from '../device/device.service';
import { AnalogDeviceService } from '../analogDeviceService/analog-device.service';
import { environment } from 'src/environments/environment';

@Injectable({
	providedIn: 'root'
})
export class WsService {
	public hubConnection: HubConnection;

	constructor(private httpService: HttpService, private deviceService: DeviceService, private analogDeviceService: AnalogDeviceService) {
	}

	connect(): void {
		this.hubConnection = new HubConnectionBuilder().withUrl(environment.api + '/Home').build();

		this.hubConnection.on('UpdateFromServer', data => {
			console.log(data);
			this.deviceService.updateDevice(data);

		});

		this.hubConnection.on('updateAnalogDevice', data => {
			this.analogDeviceService.updateAnalogDevice(data);
		});

		this.hubConnection.on('status', (devices, analogDevices) => {
			this.deviceService.init(devices);
			this.analogDeviceService.init(analogDevices);
		});

		this.hubConnection.onclose(() => {
			console.log('websocket disconnected');
			alert('disconnected');
		});

		this.hubConnection.start().then(() => {
			console.log('websocket connected');
			this.status();
		}).catch(err => {
			console.log('error connecting to websocket: ' + err.message);
		});
	}

	status(): void {
		this.hubConnection.invoke('Status').then(
			res => { },
			err => {
				console.log('error getting status' + err.message);
			}
		);
	}

	switch(id: number): void {
		this.hubConnection.invoke('switch', id).then(
			res => { },
			err => {
				console.log('error switching');
			}
		)
	}

	updateValue(id: number, value: number): void {
		this.hubConnection.invoke('UpdateAnalogDevice', id, value).then(res => {
			console.log('success updation');
		}).catch(err => {
			console.log('error updation' + err.message);
		});
	}
}
