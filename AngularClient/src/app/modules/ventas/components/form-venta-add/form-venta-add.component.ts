import { Component, Inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VentaService } from '../../services/venta-service.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CoreService } from 'src/app/core/core.service';
import { ISuscripcion } from 'src/app/modules/suscripcion/interfaces/ISuscripcion';
import { SuscripcionService } from 'src/app/modules/suscripcion/services/suscripcion-service.service';

@Component({
  selector: 'app-form-venta-add',
  templateUrl: './form-venta-add.component.html',
  styleUrls: ['./form-venta-add.component.css']
})
export class FormVentaAddComponent {
  form: FormGroup
  @ViewChild('ci', { static: false }) inputCi;
  suscripciones: any[]
  constructor(
    private _fb: FormBuilder,
    private _service: VentaService,
    private _serviceSuscripcion: SuscripcionService,
    private _dialogRef: MatDialogRef< FormVentaAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _coreService: CoreService
  ) {
    this.form = this._fb.group({
      nombreCliente: ['', Validators.required],
      ci:['', Validators.required],
      idSuscripcion: ['', Validators.required],


    });
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
      // if(this.inputCi.length != 0 && event.data == '.' && !this.inputCi.nativeElement.value.includes('.')){
      //   this.inputCi.nativeElement.value += '.' 
      // }
  
    
  }

  ngOnInit(): void {
    this.form.patchValue(this.data);
    this.getSuscripciones()
    console.log(    this.getSuscripciones()
    )
  }


  getSuscripciones(){
    this._serviceSuscripcion.getAll()
    .subscribe({
      next: (data: any[])=>{
        this.suscripciones = data
        console.log('tipo de suscripcion get',this.suscripciones)
      },
      complete: () => {
        this.suscripciones = this.suscripciones.map((item) => ({
          idSuscripcion:item.idSuscripcion,
          nombre: item.nombre,
          descuento: item.descuento,
          precio: item.precio
        }));
        console.log('datos drop', this.suscripciones)
      },
    })
  }

  onFormSubmit() {
    console.log('form',this.form)
    if (this.form.valid) {
      if (this.data) {
        this._service
          .updateVenta(this.data.idSuscripcion, this.form.value)
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
        this._service.createVenta(this.form.value).subscribe({
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
