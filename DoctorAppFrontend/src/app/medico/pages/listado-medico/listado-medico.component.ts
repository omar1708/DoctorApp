import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Medico } from '../../interfaces/medico';
import { MatPaginator } from '@angular/material/paginator';
import { MedicoService } from '../../servicios/medico.service';
import { CompartidoService } from 'src/app/compartido/compartido.service';
import { MatDialog } from '@angular/material/dialog';
import { ModalMedicoComponent } from '../../modales/modal-medico/modal-medico.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-listado-medico',
  templateUrl: './listado-medico.component.html',
  styleUrls: ['./listado-medico.component.css']
})
export class ListadoMedicoComponent implements OnInit, AfterViewInit{

  displayedColumns: string[]=[
    'apellidos',
    'nombres',
    'telefono',
    'genero',
    'nombreEspecialidad',
    'estado',
    'acciones'
  ];

  dataInicial: Medico[] = [];
  dataSource = new MatTableDataSource(this.dataInicial);
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private _medicoServicio: MedicoService,
    private _compartidoServicio: CompartidoService,
    private _dialog: MatDialog
  ){

  }

  obtenerMedicos(){
    this._medicoServicio.lista().subscribe({
      next: (data) => {
        if(data.isExitosa){
          this.dataSource=new MatTableDataSource(data.resultado);
          this.dataSource.paginator = this.paginator;
        }
        else{
          this._compartidoServicio.mostrarAlerta('No se encontraron datos', 'Advertencia!');
        }
      },
      error: (e) => {}
    })
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {
    this.obtenerMedicos();
  }

  nuevoMedico(){
    this._dialog
      .open(ModalMedicoComponent, {disableClose: true, width: '600px'})
      .afterClosed()
      .subscribe((resultado) => {
        if(resultado === 'true') this.obtenerMedicos();
      });
  }

  editarMedico(medico: Medico){
    this._dialog
      .open(ModalMedicoComponent, {disableClose: true, width: '600px', data: medico})
      .afterClosed()
      .subscribe((resultado) => {
        if(resultado === 'true') this.obtenerMedicos();
      });
  }

  removerMedico(medico: Medico){
    Swal.fire({
      title: 'Desea eliminar el medico?',
      text: medico.apellidos + ' ' + medico.nombres,
      icon: 'warning',
      confirmButtonColor: '#385d6',
      confirmButtonText: 'Si, eliminar',
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'No',
    }).then((resultado) => {
      if(resultado.isConfirmed){
        this._medicoServicio.eliminar(medico.id).subscribe({
          next: (data) =>{
            if(data.isExitosa){
              this._compartidoServicio.mostrarAlerta('El medico fue eliminado', 'Completo');
              this.obtenerMedicos();
            }
            else{
              this._compartidoServicio.mostrarAlerta('No se pudo eliminar el medico', 'Error!')
            }
          }
        })
      }
    });
  }

  aplicarFiltroListado(event: Event){
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
    if(this.dataSource.paginator){
      this.dataSource.paginator.firstPage();
    }
  }

}
