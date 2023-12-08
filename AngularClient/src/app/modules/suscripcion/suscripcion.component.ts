import { Component } from '@angular/core';
import { SuscripcionService } from './services/suscripcion-service.service';
import { MatDialog } from '@angular/material/dialog';
import { CoreService } from 'src/app/core/core.service';
import { ISuscripcion } from './interfaces/ISuscripcion';
import { HttpErrorResponse } from '@angular/common/http';
import { AddEditFormComponent } from './components/add-edit-form/add-edit-form.component';

@Component({
  selector: 'app-suscripcion',
  templateUrl: './suscripcion.component.html',
  styleUrls: ['./suscripcion.component.css']
})
export class SuscripcionComponent {


  Suscripciones: ISuscripcion[]
  constructor(
    private _service: SuscripcionService,
    private _dialog: MatDialog,
    private _coreService: CoreService) { }

  ngOnInit(): void {
    this.getSuscripcionList()
  }


  getSuscripcionList = () => {
    this._service.getAll()
    .subscribe({
      next: (data: any[])=>{
        this.Suscripciones = data
        console.log(this.Suscripciones)
      },
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }

  
  deleteSucripcion(id: number) {
    this._service.deleteSucripcion(id).subscribe({
      next: (res) => {
        this._coreService.openSnackBar('Registro Eliminado!', 'Listo');
        this.getSuscripcionList()
      },
      error: console.log,
    });
  }
  
  openAddForm() {
    const dialogRef = this._dialog.open(AddEditFormComponent);
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this._service.create(val)
          this.getSuscripcionList()
        }
      },
    });
  }

  openEditForm(data: any) {
    const dialogRef = this._dialog.open(AddEditFormComponent, {
      data,
    });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getSuscripcionList()
        }
      },
    });
  }

}
