import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthConfigModule } from './auth/auth-config.module';
import { HeaderComponent } from './components/header/header.component';
import { DolciInVenditaComponent } from './components/dolci-in-vendita/dolci-in-vendita.component';
import { BackofficeComponent } from './components/backoffice/backoffice.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { NgxMasonryModule } from 'ngx-masonry';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { DialogInputDisponibilitaComponent } from './components/dialog-input-disponibilita/dialog-input-disponibilita.component';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { DialogNuovoDolceInVetrinaComponent } from './components/dialog-nuovo-dolce-in-vetrina/dialog-nuovo-dolce-in-vetrina.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    DolciInVenditaComponent,
    BackofficeComponent,
    DialogInputDisponibilitaComponent,
    DialogNuovoDolceInVetrinaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthConfigModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,
    NgxMasonryModule,
    MatTableModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
