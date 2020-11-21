import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ErrorService } from '../models/error-service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  /**
   *
   */
  constructor(private router: Router,private toaster: ToastrService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
   // var temp = localStorage.getItem('token') ;
    if (localStorage.getItem('token') != null) {
      const clonedReq = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + localStorage.getItem('token'))
      });
      if (!req.headers.has('Content-Type')) {
        req = req.clone({ headers: req.headers.set('Content-Type', 'application/json') });
      }
      return next.handle(clonedReq).pipe(
        tap(
          succ => { },
          err => {
            if (err.status == 401) {
              localStorage.removeItem('token');
              this.router.navigateByUrl('/mauth');
            } else if (err.status == 403) {
              this.toaster.warning("", "Auth Error.");
              localStorage.removeItem('token');
              this.router.navigateByUrl('/mauth');
            }
            else {
              let errorMessage = '';
              if (err.error instanceof ErrorEvent) {
                // Get client-side error
                errorMessage = err.error.message;
                window.alert(errorMessage);
              } else {
                // Get server-side error
                //error.error have the server respnse contains meta 
                if (err.error.Meta === undefined) {    
                  this.toaster.error(err.status, err.message);         
                  // this.errorService.code = err.status;
                  // this.errorService.message = err.name;
                  // this.errorService.detail = err.message;

                } else {
                  this.toaster.error(err.error.Meta.MessageDetail, err.error.Meta.Message);
                }
                this.router.navigate(['/error']);
                // errorMessage = `Error Code: ${err.status}\nMessage: ${err.error.meta.message}\nDetail: ${err.error.meta.messageDetail}`;
              }



            }
          }
        )
      )
    }
    else
      return next.handle(req.clone());
  }

}