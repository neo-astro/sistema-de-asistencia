import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { EnvironmentUrlService } from 'src/app/shared/services/environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class SuscripcionService {


  url = "https://localhost:5001/api/Suscripcion/"
  constructor(private _http: HttpClient, private envUrl: EnvironmentUrlService, private jwtHelper: JwtHelperService) { }


  getAll(){
    console.log(this._http.get<any[]> (this.url))
    return this._http.get<any[]> (this.url);
    
  }

  create(data: any): Observable<any> {
    return this._http.post(`https://localhost:5001/api/Suscripcion/create`, data);
  }
  update(id: number, data: any): Observable<any> {
    return this._http.put(`https://localhost:5001/api/Suscripcion/update/${id}`, data);
  }
  deleteSucripcion(id: number): Observable<any> {
    return this._http.delete(`https://localhost:5001/api/Suscripcion/delete/${id}`);
  }
}
