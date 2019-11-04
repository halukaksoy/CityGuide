import { AlertifyService } from './../services/alertify.service';
import { AuthService } from "src/app/services/auth.service";
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent
} from "@angular/common/http";
import { Observable,throwError } from "rxjs";
import { Injectable } from "@angular/core";
import { catchError } from "rxjs/operators";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService,private alertify:AlertifyService) {}

  intercept(request: HttpRequest<any>,next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
        if (err.status === 401) {
            this.authService.logOut();
            this.alertify.error("Please Login!");
            location.reload(true);
        }
        const error = err.error.message || err.statusText;

        return throwError(error);
      }));  
  }
}
