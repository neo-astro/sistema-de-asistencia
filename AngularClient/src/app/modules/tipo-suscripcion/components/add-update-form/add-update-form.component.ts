import { Component, Inject, OnInit } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TipoSuscripcionService } from '../../services/tipo-suscripcion.service';
import { MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CoreService } from 'src/app/core/core.service';

@Component({
  selector: 'app-add-update-form',
  templateUrl: './add-update-form.component.html',
  styleUrls: ['./add-update-form.component.css']
})
export class AddUpdateFormComponent {
  form: FormGroup

  constructor(
    private _fb: FormBuilder,
    private _service: TipoSuscripcionService,
    private _dialogRef: MatDialogRef< AddUpdateFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _coreService: CoreService
  ) {
    this.form = this._fb.group({
      nombre: ['', Validators.required],
    });
  }

 

  ngOnInit(): void {
    this.form.patchValue(this.data);
  }

  onFormSubmit() {
    if (this.form.valid) {
      console.log('dataa', this.data)
      if (this.data) {
        this._service
          .updateSucripciones(this.data.idTipoSuscripcion, this.form.value)
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
        this._service.createSucripciones(this.form.value).subscribe({
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
