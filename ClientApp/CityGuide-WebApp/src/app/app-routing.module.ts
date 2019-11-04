import { HomeComponent } from './components/home/home.component';
import { CityAddComponent } from "./components/city/city-add/city-add.component";
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { CityComponent } from "./components/city/city.component";
import { CityDetailComponent } from "./components/city/city-detail/city-detail.component";
import { RegisterComponent } from './components/auth/register/register.component';
import { AuthGuard } from './helper/AuthGuard';

const routes: Routes = [
  { path: "city", component: CityComponent, canActivate: [AuthGuard] },
  { path: "cityAdd", component: CityAddComponent, canActivate: [AuthGuard] },
  { path: "cityDetail/:cityId", component: CityDetailComponent, canActivate: [AuthGuard] },
  { path: "register", component: RegisterComponent },
  { path: "home", component: HomeComponent },
  { path: "**", redirectTo: "home", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
