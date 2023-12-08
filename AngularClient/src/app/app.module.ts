import { AdminGuard } from './shared/guards/admin.guard';
import { AuthGuard } from './shared/guards/auth.guard';
import { ErrorHandlerService } from './shared/services/error-handler.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { JwtModule } from "@auth0/angular-jwt";
 
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';


import { TipoSuscripcionComponent } from './modules/tipo-suscripcion/tipo-suscripcion.component';
import { AddUpdateFormComponent } from './modules/tipo-suscripcion/components/add-update-form/add-update-form.component';

import {MatListModule} from '@angular/material/list';


import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { AsistenciaComponent } from './modules/asistencia/asistencia.component';
import { SuscripcionComponent } from './modules/suscripcion/suscripcion.component';
import { AddEditFormComponent } from './modules/suscripcion/components/add-edit-form/add-edit-form.component';
import { VentasComponent } from './modules/ventas/ventas.component';
import { FormVentaAddComponent } from './modules/ventas/components/form-venta-add/form-venta-add.component';
import { AsistenciaFormAddComponent } from './modules/asistencia/components/asistencia-form-add/asistencia-form-add.component';
export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuComponent,
    NotFoundComponent,
    ForbiddenComponent,
    TipoSuscripcionComponent,
    AddUpdateFormComponent,
    VentasComponent,
    AsistenciaComponent,
    SuscripcionComponent,
    AddEditFormComponent,
    FormVentaAddComponent,
    AsistenciaFormAddComponent,
  ],
  imports: [
    //modulos de ui
    MatListModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatRadioModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatSnackBarModule,


    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent },
      { path: 'authentication', loadChildren: () => import('./modules/authentication/authentication.module').then(m => m.AuthenticationModule) },
      {path: 'tipoSuscripcion', component : TipoSuscripcionComponent,canActivate: [AuthGuard] },
      {path: 'suscripcion', component : SuscripcionComponent,canActivate: [AuthGuard] },
      {path: 'ventas', component : VentasComponent,canActivate: [AuthGuard] },
      {path: 'asistencia', component : AsistenciaComponent,canActivate: [AuthGuard] },

      { path: '404', component : NotFoundComponent},
      { path: 'forbidden', component: ForbiddenComponent },
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: '**', redirectTo: '/404', pathMatch: 'full'}
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }