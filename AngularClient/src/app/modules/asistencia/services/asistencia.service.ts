import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AsistenciaService {


  constructor(private _http: HttpClient) { }
  

 consulta(ci:any):Observable<any> {
  return this._http.get(`https://localhost:5001/api/Asistencia/consultar/${ci}`);
}
  get(): Observable<any> {
    return this._http.get('https://localhost:5001/api/Asistencia/');
  }

  create(data: any): Observable<any> {
    return this._http.post('https://localhost:5001/api/Asistencia/create', data);
  }
}
