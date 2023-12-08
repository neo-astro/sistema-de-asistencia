import { Component,Inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CoreService } from 'src/app/core/core.service';
import { SuscripcionService } from '../../services/suscripcion-service.service';
import { TipoSuscripcionService } from 'src/app/modules/tipo-suscripcion/services/tipo-suscripcion.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ISuscripcion } from '../../interfaces/ISuscripcion';


interface ITipoSuscripcion {
  idTipoSuscripcion: number;
  nombre: string;
}

@Component({
  selector: 'app-add-edit-form',
  templateUrl: './add-edit-form.component.html',
  styleUrls: ['./add-edit-form.component.css']
})
export class AddEditFormComponent {
  form: FormGroup
  tipoSuscripciones: ITipoSuscripcion[];
  suscripciones: ISuscripcion[];
  @ViewChild('precio', { static: false }) inputPrecio;


  constructor(
    private _fb: FormBuilder,    
    private _tipoSuscripcionService: TipoSuscripcionService, // Agrega el servicio de dropdown
    private _service: SuscripcionService,
    private _dialogRef: MatDialogRef< AddEditFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _coreService: CoreService
  ) {
    this.form = this._fb.group({
      nombre: ['', Validators.required],
      descuento: ['', Validators.required],
      duracion: ['', Validators.required],
      precio: ['', Validators.required],
      idTipoSuscripcion: ['', Validators.required],
      // // IdTipoSuscripcion: [null, Validators.required]
    });
  }

  isAlphanumeric(str) {
    return /^[0-9]+$/.test(str);
  }

  format(str) {
    return /^[1-9][0-9]+?[\.[0-9]+]?$/.test(str);
  }

  formatPrecio(event){
    console.log(event);


    if(event.inputType == 'deleteContentBackward'){
      return
    }
    if(!this.isAlphanumeric(event.data) ){
      this.inputPrecio.nativeElement.value =  this.inputPrecio.nativeElement.value.slice(0,-1)     
    }
    if(this.inputPrecio.length != 0 && event.data == '.' && !this.inputPrecio.nativeElement.value.includes('.')){
      this.inputPrecio.nativeElement.value += '.' 
    }

  }

  ngOnInit(): void {
    this.form.patchValue(this.data);
    this.getTipoSuscripciones()
    console.log(this.getTipoSuscripciones())
  }


  getTipoSuscripciones(){
    this._tipoSuscripcionService.getSucripciones()
    .subscribe({
      next: (data: any[])=>{
        this.tipoSuscripciones = data
        console.log('tipo de porcesos get',this.tipoSuscripciones)
      },
      complete: () => {
        this.tipoSuscripciones = this.tipoSuscripciones.map((item) => ({
          idTipoSuscripcion: item.idTipoSuscripcion,
          nombre: item.nombre,
      
        }));
        console.log('datos drop', this.tipoSuscripciones)
      },
    })
  }

   getSuscripciones = () => {
    this._service.getAll()
    .subscribe({
      next: (data: any[])=>{
        this.suscripciones = data   
      },
      error: (err: HttpErrorResponse) => console.log(err)
    })
  } 

  onFormSubmit() {
    if (this.form.valid) {
      if (this.data) {
        this._service
          .update(this.data.idSuscripcion, this.form.value)
          .subscribe({
            next: (val: any) => {
              this._coreService.openSnackBar('Registro actualizado!');
              this._dialogRef.close(true);
            },
            error: (err: any) => {
              console.error(err);
            },
          });
      } else {
        this._service.create(this.form.value).subscribe({
          next: (val: any) => {
            this._coreService.openSnackBar('Registro creado');
            this._dialogRef.close(true);
          },
          error: (err: any) => {
            console.error(err);
          },
        });
      }
    }
  }
  
}
