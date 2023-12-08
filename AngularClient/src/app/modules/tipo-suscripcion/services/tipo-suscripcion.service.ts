import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from '../../../shared/services/environment-url.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { TipoSuscripcion } from '../interfaces/tipoSuscripcion';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TipoSuscripcionService {

  // constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }

  // public getData = (route: string) => {
  //   return this.http.get<Company[]>(this.createCompleteRoute(route, this.envUrl.urlAddress));
  // }

  // public getClaims = (route: string) => {
  //   return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress));
  // }

  // private createCompleteRoute = (route: string, envAddress: string) => {
  //   return `${envAddress}/${route}`;
  // }


  url :string = 'https://localhost:5001/api/TipoSuscripcion'
  constructor(private _http: HttpClient, private envUrl: EnvironmentUrlService, private jwtHelper: JwtHelperService) { }

  getSucripciones(){
    console.log(this._http.get<any[]> (this.url))
    return this._http.get<any[]> (this.url);
    
  }

  createSucripciones(data: any): Observable<any> {
    return this._http.post('https://localhost:5001/api/TipoSuscripcion/create', data);
  }
  updateSucripciones(id: number, data: any): Observable<any> {
    return this._http.put(`https://localhost:5001/api/TipoSuscripcion/update/${id}`, data);
  }
  deleteSucripciones(id: number): Observable<any> {
    return this._http.delete(`https://localhost:5001/api/TipoSuscripcion/delete/${id}`);
  }
}
