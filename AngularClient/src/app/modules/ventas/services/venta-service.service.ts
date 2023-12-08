import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VentaService {

  constructor(private _http: HttpClient) { }

  createVenta(data: any): Observable<any> {
    return this._http.post('https://localhost:5001/api/VentaSuscripcion/create', data);
  }
  updateVenta(id,data): Observable<any>{
    return
  }
}
