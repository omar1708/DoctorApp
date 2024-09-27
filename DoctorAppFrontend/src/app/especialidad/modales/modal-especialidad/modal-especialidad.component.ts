import { Component, Inject, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EspecialidadService } from '../../servicios/especialidad.service';
import { CompartidoService } from 'src/app/compartido/compartido.service';
import { Especialidad } from '../../interfaces/especialidad';

@Component({
  selector: 'app-modal-especialidad',
  templateUrl: './modal-especialidad.component.html',
  styleUrls: ['./modal-especialidad.component.css']
})
export class ModalEspecialidadComponent implements OnInit {
  formEspecialidad: FormGroup;
  titulo: string = "Agregar";
  nombreBoton: string = "Guardar";

  constructor(private modal: MatDialogRef<ModalEspecialidadComponent>,
              @Inject(MAT_DIALOG_DATA) public datosEspecialidad: Especialidad,
              private fb: FormBuilder,
              private _especialidadServicio: EspecialidadService,
              private _compartidoService: CompartidoService
  ){
    this.formEspecialidad = this.fb.group({
      nombreEspecialidad: ['', Validators.required],
      descripcion: ['', Validators.required],
      estado: ['1', Validators.required]
    });

    if(this.datosEspecialidad != null){
      this.titulo = 'Editar';
      this.nombreBoton = 'Actualizar';
    }
  }
  
  ngOnInit(): void {
    if(this.datosEspecialidad != null){
      this.formEspecialidad.patchValue({
        nombreEspecialidad: this.datosEspecialidad.nombreEspecialidad,
        descripcion: this.datosEspecialidad.descripcion,
        estado: this.datosEspecialidad.estado.toString()
      })
    }
  }

  crearModificarEspecialidad(){
    const especialidad: Especialidad = {
      id: this.datosEspecialidad == null ? 0: this.datosEspecialidad.id,
      nombreEspecialidad: this.formEspecialidad.value.nombreEspecialidad,
      descripcion: this.formEspecialidad.value.descripcion,
      estado: parseInt(this.formEspecialidad.value.estado)
    }

    if(this.datosEspecialidad == null){
      //crear nuea especialidad
      this._especialidadServicio.crear(especialidad).subscribe({
        next: (data)=>{
          if(data.isExitosa){
            this._compartidoService.mostrarAlerta('La especialidad ha sido grabada con exito', 'Completo');
            this.modal.close("true");
          }
          else{
            this._compartidoService.mostrarAlerta('No se puedo crear la especialidad', 'Error')
          }
        },
        error: (e)=>{
          this._compartidoService.mostrarAlerta(e.error.mensaje, 'Error!')
        }
      })
    }
    else{
      //Editar/Actualizar especialidad
      this._especialidadServicio.editar(especialidad).subscribe({
        next: (data)=>{
          if(data.isExitosa){
            this._compartidoService.mostrarAlerta('La especialidad ha sido actualizada con exito', 'Completo');
            this.modal.close("true");
          }
          else{
            this._compartidoService.mostrarAlerta('No se puedo actualizar la especialidad', 'Error');
          }
        },
        error: (e)=>{
          this._compartidoService.mostrarAlerta(e.error.mensaje, 'Error!');
        }
      })
    }
  }

}
