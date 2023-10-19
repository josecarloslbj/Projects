import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PrimengModule } from './shared/primeng.module';
import { CommonModule } from '@angular/common';
import * as ngCommon from "@angular/common";
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { AppLayoutModule } from './layout/app.layout.module';
import { ProductService } from './demo/service/product.service';
import { CountryService } from './demo/service/country.service';
import { CustomerService } from './demo/service/customer.service';
import { EventService } from './demo/service/event.service';
import { IconService } from './demo/service/icon.service';
import { NodeService } from './demo/service/node.service';
import { PhotoService } from './demo/service/photo.service';
import { AuthenticationService } from './login/authentication.service';
import { HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './guards/jwt.interceptor';
import { ErrorInterceptor } from './guards/error.interceptor';
import { appInitializer } from './guards/app.initializer';
import { ConfirmationService, MessageService } from 'primeng/api';
import { VendaModule } from './components/venda/venda.module';
import { TranslateLoader, TranslateModule, TranslateService, TranslateStore } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { ComponentesComunsModule } from './shared/componentes/componentes-comun.module';
import { UploadService } from './shared/componentes/upload/upload.service';

// AoT requires an exported function for factories
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent    
  ],
  imports: [
    AppRoutingModule,
    AppLayoutModule,
    BrowserModule,
    BrowserAnimationsModule,
    ngCommon.CommonModule,
    FormsModule,
    CommonModule,
    PrimengModule,
    AppRoutingModule,
    VendaModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    ComponentesComunsModule
  ],
  exports: [
    PrimengModule
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: appInitializer, multi: true, deps: [AuthenticationService] },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true, deps: [AuthenticationService] },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true, deps: [MessageService] },

    CountryService, CustomerService, EventService, IconService, NodeService,
    PhotoService, ProductService,
    AuthenticationService,
    AuthGuard,
    MessageService,
    ConfirmationService,
    UploadService,
    TranslateService,
    TranslateStore,    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

