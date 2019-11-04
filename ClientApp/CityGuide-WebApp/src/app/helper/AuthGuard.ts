import { AlertifyService } from './../services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({providedIn:'root'})
export class AuthGuard implements CanActivate {

constructor(private router:Router,private authService:AuthService,private alertify:AlertifyService){};

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
       if(this.authService.isLoggedIn()){
           return true;
       }
       this.router.navigate(['home']);
       this.alertify.error("Please Login!");
       return false;
    }
}
