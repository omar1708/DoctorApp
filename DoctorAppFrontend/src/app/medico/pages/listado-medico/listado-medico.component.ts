import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Medico } from '../../interfaces/medico';
import { MatPaginator } from '@angular/material/paginator';
import { MedicoService } from '../../servicios/medico.service';
import { CompartidoService } from 'src/app/compartido/compartido.service';

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

  dataInicial: Medico[]=[];
  dataSource = new MatTableDataSource(this.dataInicial);
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private _medicoServicio: MedicoService,
    private _compartidoServicio: CompartidoService
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

  }

  editarMedico(medico: Medico){

  }

  removerMedico(medico: Medico){

  }

  aplicarFiltroListado(event: Event){
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
    if(this.dataSource.paginator){
      this.dataSource.paginator.firstPage();
    }
  }

}
