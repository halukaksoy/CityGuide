import { Photo } from "./../models/photo";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { City } from "../models/city";
import { AlertifyService } from "./alertify.service";
import { Router } from "@angular/router";
import { connectableObservableDescriptor } from "rxjs/internal/observable/ConnectableObservable";

@Injectable({
  providedIn: "root"
})
export class CityService {
  constructor(
    private http: HttpClient,
    private alertifyService: AlertifyService,
    private router: Router
  ) {}
  path = "https://localhost:44335/api/";
  getCities(): Observable<City[]> {
    return this.http.get<City[]>(this.path + "cities");
  }
  getCityById(cityId): Observable<City> {
    return this.http.get<City>(this.path + "cities/detail/" + cityId);
  }

  getPhotosByCityId(cityId): Observable<Photo[]> {
    return this.http.get<Photo[]>(this.path + "cities/photos/" + cityId);
  }

  add(city) {
    this.http.post(this.path + "cities/add", city).subscribe(data => {
      this.alertifyService.success("Operation Successful");
      this.router.navigateByUrl("/cityDetail/" + data["id"]);
    });
  }
  delete(city) {
   return this.http.post(this.path + "cities/delete", city);   
  }
}
