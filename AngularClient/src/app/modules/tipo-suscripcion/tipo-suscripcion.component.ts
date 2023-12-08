import { Component, OnInit, ViewChild } from '@angular/core';
import { TipoSuscripcionService } from './services/tipo-suscripcion.service';
import { HttpErrorResponse } from '@angular/common/http';
import { TipoSuscripcion } from './interfaces/tipoSuscripcion';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { CoreService } from '../../core/core.service';
import { AddUpdateFormComponent } from './components/add-update-form/add-update-form.component';
@Component({
  selector: 'app-tipo-suscripcion',
  templateUrl: './tipo-suscripcion.component.html',
  styleUrls: ['./tipo-suscripcion.component.css']
})
export class TipoSuscripcionComponent implements OnInit {
  
  dataSource!: MatTableDataSource<any>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  TipoSuscripcion: TipoSuscripcion[]

  constructor(
    private _service: TipoSuscripcionService,
    private _dialog: MatDialog,
    private _coreService: CoreService) { }

  ngOnInit(): void {
    this.getTipoSuscripcion()
  }


  getTipoSuscripcion = () => {
    this._service.getSucripciones()
    .subscribe({
      next: (data: any[])=>{
        this.TipoSuscripcion = data
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.sort = this.sort;
        console.log(this.TipoSuscripcion)
      },
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }

  
  deleteSucripcion(id: number) {
    this._service.deleteSucripciones(id).subscribe({
      next: (res) => {
        this._coreService.openSnackBar('Registro Eliminado!', 'Listo');

        this.getTipoSuscripcion()

      },
      error: console.log,
    });
  }
  
  openAddForm() {
    const dialogRef = this._dialog.open(AddUpdateFormComponent);
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this._service.createSucripciones(val)
          this.getTipoSuscripcion()
        }
      },
    });
  }



  openEditForm(data: any) {
    const dialogRef = this._dialog.open(AddUpdateFormComponent, {
      data,
    });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          
          this.getTipoSuscripcion()

        }
      },
    });
  }

}
