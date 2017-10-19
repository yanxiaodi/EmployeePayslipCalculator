import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing/app-routing.module';

import { FieldsetModule, SpinnerModule, DropdownModule, ButtonModule, GrowlModule } from 'primeng/primeng';
import { HomeComponent } from './pages/home/home.component';
import { GlobalContextService, HttpClientService, CalculatorService } from './services';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FieldsetModule,
    SpinnerModule,
    DropdownModule,
    ButtonModule,
    GrowlModule
  ],
  providers: [
    GlobalContextService,
    HttpClientService,
    CalculatorService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
