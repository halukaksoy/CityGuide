import { HomeComponent } from './components/home/home.component';
import { JwtInterceptor } from './helper/JwtInterceptor';
import { PhotoComponent } from './components/city/photo/photo.component';
import { CityAddComponent } from "./components/city/city-add/city-add.component";
import { CityDetailComponent } from "./components/city/city-detail/city-detail.component";
import {
  BrowserModule,
  HammerGestureConfig,
  HAMMER_GESTURE_CONFIG
} from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { AppRoutingModule } from "./app-routing.module";
import { NgxGalleryModule } from "ngx-gallery";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgxEditorModule } from "ngx-editor";
import { FileUploadModule } from "ng2-file-upload";
import { AppComponent } from "./app.component";
import { NavComponent } from "./components/nav/nav.component";
import { CityComponent } from "./components/city/city.component";
import { AlertifyService } from "./services/alertify.service";
import { RegisterComponent } from "./components/auth/register/register.component";
import { ErrorInterceptor } from './helper/ErrorInterceptor';

export class CustomHammerConfig extends HammerGestureConfig {
  overrides = {
    pinch: { enable: false },
    rotate: { enable: false }
  };
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    CityComponent,
    CityDetailComponent,
    CityAddComponent,
    RegisterComponent,
    PhotoComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgxGalleryModule,
    FormsModule,
    ReactiveFormsModule,
    NgxEditorModule,
    FileUploadModule
  ],
  providers: [
    { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig },
    { provide: HTTP_INTERCEPTORS,useClass:JwtInterceptor,multi:true},
    { provide: HTTP_INTERCEPTORS,useClass:ErrorInterceptor,multi:true},
    AlertifyService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
