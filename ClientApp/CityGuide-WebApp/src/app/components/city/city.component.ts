import { AlertifyService } from "./../../services/alertify.service";
import { Component, OnInit } from "@angular/core";
import { City } from "../../models/city";
import { CityService } from "src/app/services/city.service";

@Component({
  selector: "app-city",
  templateUrl: "./city.component.html",
  styleUrls: ["./city.component.css"],
  providers: [CityService]
})
export class CityComponent implements OnInit {
  constructor(
    private cityService: CityService,
    private alertify: AlertifyService
  ) {}

  cities: City[];
  ngOnInit() {
    this.getCities();
  }
  getCities() {
    this.cityService.getCities().subscribe(data => {
      console.log(data);
      this.cities = data;
    });
  }
  deleteCity(city: City) {

    var result = confirm('Are you sure?');
    if(!result)
    { 
      return;
    }
    this.cityService.delete(city).subscribe(data => {
      console.log(data);
      if (data["isSuccess"] === true) {
        this.alertify.success("City Deleted!"); 
        this.getCities();
      } else {
        this.alertify.error("City Delete Fail!" + data["ErrorMessage"]);
      }
    });
  }
}
