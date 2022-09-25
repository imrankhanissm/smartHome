import { Component, OnInit } from '@angular/core';
import { LoginModel } from 'src/app/models/loginModel';
import { HttpService } from 'src/app/services/http/http.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginModel: LoginModel;
  constructor(private httpService: HttpService, private router: Router) {
    this.loginModel = new LoginModel();
  }

  ngOnInit() {
    if (localStorage.getItem('jwtAccessToken') != null) {
      this.router.navigateByUrl('home');
    }
  }

  login(): void {
    this.httpService.login(this.loginModel);
  }
}
