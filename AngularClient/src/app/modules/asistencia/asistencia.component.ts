import { Component, OnInit, ViewChild } from '@angular/core';
import { SuscripcionService } from '../suscripcion/services/suscripcion-service.service';
import { HttpErrorResponse } from '@angular/common/http';
import { AsistenciaService } from './services/asistencia.service';

@Component({
  selector: 'app-asistencia',
  templateUrl: './asistencia.component.html',
  styleUrls: ['./asistencia.component.css']
})



export class AsistenciaComponent implements OnInit {
  @ViewChild('ci', { static: false }) inputCi;
  estadoEnviar: boolean=true
res: any
  asistenciaList: any[]
  constructor(private _service:AsistenciaService){}
  ngOnInit(): void {
    this.getAsistenciaList()
  }

  consultar(){
if(this.inputCi.nativeElement.value.length == 10){this.estadoEnviar= true}
  }
 registrarAsistencia(){
  let res
  let fecha = new Date()
  let año = fecha.getFullYear();
let mes = fecha.getMonth() + 1; // Ten en cuenta que los meses van de 0 a 11, por lo que sumamos 1
let dia = fecha.getDate();
let fechaHoy = año + '-'+mes+'-'+dia; 
    this._service.consulta(this.inputCi.nativeElement.value).subscribe({
      next: (data: any[])=>{
        this.res=data
        console.log(data)  
        
      },complete: () => {
        let fechadb = this.res[0].fechaFinSuscripcion.slice(0,10)
        console.log(fechadb)
        console.log(fechaHoy)

        let fechaDbr = new Date(`20${fechadb.replace(/-/g, '/')}`);
        let fechaHoyr = new Date(`20${fechaHoy.replace(/-/g, '/')}`);
        if (fechaDbr >= fechaHoyr) {
          this._service.create({"idVentaSuscripcion":this.res[0].idVentaSuscripcion})
          this.inputCi.nativeElement.value = '';
          this.estadoEnviar = true;
          this.inputCi.nativeElement.focus();
          alert('registrado')
          this.getAsistenciaList()

        } else {
          alert('No hay suscripcion asignada')
          this.inputCi.nativeElement.value = '';
          this.estadoEnviar = true;
          this.inputCi.nativeElement.focus();
        }

      },

      
      error: (err: HttpErrorResponse) => console.log(err)
    })

   
  }

  hora(fecha): string {
    const horas = fecha.getHours().toString().padStart(2, '0');
    const minutos = fecha.getMinutes().toString().padStart(2, '0');
    const segundos = fecha.getSeconds().toString().padStart(2, '0');
    return `${horas}:${minutos}:${segundos}`;
  }
  getAsistenciaList = () => {
    this._service.get()
    .subscribe({
      next: (data: any[])=>{
        this.asistenciaList = data
        console.log(this.asistenciaList)
      },
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }


  isAlphanumeric(str) {
    return /^[0-9]+$/.test(str);
  }

  formatCi(event){
      console.log(event);
  
  
      if(event.inputType == 'deleteContentBackward'){
        return
      }

      if(!this.isAlphanumeric(event.data)){
        this.inputCi.nativeElement.value =  this.inputCi.nativeElement.value.slice(0,-1)     
      }

      if(this.inputCi.nativeElement.value.length == 10){this.estadoEnviar= false}
      // if(this.inputCi.length != 0 && event.data == '.' && !this.inputCi.nativeElement.value.includes('.')){
      //   this.inputCi.nativeElement.value += '.' 
      // }
  
    
  }
}
