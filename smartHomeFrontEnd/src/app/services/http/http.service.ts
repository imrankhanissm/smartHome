import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from 'src/app/models/loginModel';
import { Router } from '@angular/router';
import { Urls } from '../urls';
import { DeviceModel } from 'src/app/models/deviceModel';
import { DeviceService } from '../device/device.service';

@Injectable({
	providedIn: 'root'
})
export class HttpService {
	// statusUrl = 'api/Devices/status';
	// switchUrl = 'api/Devices/switch/';
	// public devices: object;
	constructor(private http: HttpClient, private router: Router, private deviceService: DeviceService) { }

	login(loginModel: LoginModel): void {
		this.http.post(Urls.serverUrl + 'api/auth/Login', loginModel).subscribe(
			res => {
				localStorage.setItem('jwtAccessToken', res['token']);
				this.router.navigateByUrl('/home');
			},
			err => {
				console.log('error login' + err.status);
			}
		);
	}

	// values() {
	//   this.http.get(Urls.serverUrl + 'api/values').subscribe(
	//     res => {
	//       console.log(res);
	//     },
	//     err => {
	//       console.log('values error: ' + err.status);
	//     }
	//   )
	// }

	// status(): void {
	// 	this.http.get<DeviceModel[]>(Urls.serverUrl + this.statusUrl).subscribe(
	// 		res => {
	// 			this.deviceService.init(res);
	// 		},
	// 		err => {
	// 			// console.log('error getting status');
	// 		}
	// 	);
	// }

	// switch(id: number): void {
	// 	this.http.put<DeviceModel>(Urls.serverUrl + this.switchUrl + id, {}).subscribe(
	// 		res => { },
	// 		err => {
	// 			console.log('error switching device');
	// 		}
	// 	);
	// }

	logout(): void {
		localStorage.removeItem('jwtAccessToken');
		this.router.navigateByUrl('login');
	}
}
