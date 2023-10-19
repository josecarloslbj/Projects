import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { PrimengModule } from '../../shared/primeng.module';
import { PerfisService } from '../perfis/perfis.service';
import { UsuarioService } from '../usuarios/usuario.service';
import { ManterFabricanteComponent } from './manter/manter-fabricante.component';
import { FabricanteComponent } from './listar/fabricante.component';
import { FabricanteRoutingModule } from './fabricante.routing.module';
import { FabricanteService } from './fabricante.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        FabricanteRoutingModule,
        UtilsModule,
        PrimengModule
    ],
    exports: [],
    declarations: [
        FabricanteComponent,
        ManterFabricanteComponent
    ],
    providers: [FabricanteService],
})
export class FabricanteModule { }
