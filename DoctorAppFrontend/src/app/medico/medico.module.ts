import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompartidoModule } from '../compartido/compartido.module';
import { MaterialModule } from '../material/material.module';
import { ListadoMedicoComponent } from './pages/listado-medico/listado-medico.component';
import { MedicoService } from './servicios/medico.service';



@NgModule({
  declarations: [
    ListadoMedicoComponent
  ],
  imports: [
    CommonModule,
    CompartidoModule,
    MaterialModule
  ],
  providers:[
    MedicoService
  ]
})
export class MedicoModule { }
