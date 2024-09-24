import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompartidoModule } from '../compartido/compartido.module';
import { MaterialModule } from '../material/material.module';
import { ListadoEspecialidadComponent } from './pages/listado-especialidad/listado-especialidad.component';
import { ModalEspecialidadComponent } from './modales/modal-especialidad/modal-especialidad.component';



@NgModule({
  declarations: [
    ListadoEspecialidadComponent,
    ModalEspecialidadComponent
  ],
  imports: [
    CommonModule,
    CompartidoModule,
    MaterialModule
  ]
})
export class EspecialidadModule { }
