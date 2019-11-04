import { AlertifyService } from "./alertify.service";
import { NavComponent } from "./../components/nav/nav.component";
import { Router } from "@angular/router";
import { HttpClient,HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserLoginDto } from "../models/UserLoginDto"; 
import { JwtHelperService } from "@auth0/angular-jwt";
import { UserRegisterDto } from "../models/UserRegisterDto";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private alertify: AlertifyService
  ) {}
  path = "https://localhost:44335/api/security/";
  userToken: any;
  decodedToken: any;
  jwtHelper: JwtHelperService = new JwtHelperService();
  TOKEN_KEY = "token";

  login(loginUser: UserLoginDto) {
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json");
    this.httpClient
      .post(this.path + "login", loginUser, { headers: headers })
      .subscribe(data => {
        this.saveToken(data["tokenString"]);
        this.userToken = data["tokenString"];
        this.decodedToken = this.jwtHelper.decodeToken(data["tokenString"]);
        this.alertify.success("Login Succeeded");
        this.router.navigateByUrl("/city");
      });
  }
  register(registerUser: UserRegisterDto) {
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json");
    this.httpClient
      .post(this.path + "register", registerUser, { headers: headers })
      .subscribe(data => {
        this.alertify.success("Register Succeeded");
        this.router.navigateByUrl("/home");
      });
  }
  saveToken(token) {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  logOut() {
    localStorage.removeItem(this.TOKEN_KEY);
    this.router.navigateByUrl("/home");
    this.alertify.error("Logout Succeeded");
  }
  get token() {
    return localStorage.getItem(this.TOKEN_KEY);
  }
  isLoggedIn() { 
    return !this.jwtHelper.isTokenExpired(this.token);
  }
  getCurrentUserId(){
    return this.jwtHelper.decodeToken(this.token).nameid;
  }
}
