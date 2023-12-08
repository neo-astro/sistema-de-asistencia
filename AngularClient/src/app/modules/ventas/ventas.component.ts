import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CoreService } from 'src/app/core/core.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { VentaService } from './services/venta-service.service';
import { FormVentaAddComponent } from './components/form-venta-add/form-venta-add.component';
@Component({
  selector: 'app-ventas',
  templateUrl: './ventas.component.html',
  styleUrls: ['./ventas.component.css']
})
export class VentasComponent {
  dataSource!: MatTableDataSource<any>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  constructor(
    private _service: VentaService,
    private _dialog: MatDialog,
    private _coreService: CoreService) { }

  ngOnInit(): void {
  }


 
  openAddForm() {
    const dialogRef = this._dialog.open(FormVentaAddComponent);
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this._service.createVenta(val)
        }
      },
    });
  }



}
