import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { WsService } from '../services/ws/ws.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {

  constructor(private router: Router, private wsService: WsService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let clonedReq = req;
    if (localStorage.getItem('jwtAccessToken') != null) {
      clonedReq = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + localStorage.getItem('jwtAccessToken'))
      });
    }
    return next.handle(clonedReq).pipe(
      tap(
        succ => { },
        err => {
          if (err.status == 401) {
            // console.log('unauthorized error: ' + err.headers.values());
            this.wsService.hubConnection.stop();
            localStorage.removeItem('jwtAccessToken');
            this.router.navigateByUrl('login');
          }
        }
      )
    );
  }
}
