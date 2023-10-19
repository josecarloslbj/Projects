import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../../shared/utils/utils.module';
import { PrimengModule } from '../../../shared/primeng.module';
import { PerfisModule } from '../../perfis/perfis.module';
import { ManterEstadoComponent } from './manter/manter-estado.component';
import { EstadoComponent } from './listar/estado.component';
import { EstadoRoutingModule } from './estado-routing.module';
import { EstadoService } from './estado.service';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        EstadoRoutingModule,
        UtilsModule,
        PrimengModule,
        PerfisModule
    ],
    exports: [],
    declarations: [
        EstadoComponent,
        ManterEstadoComponent
    ],
    providers: [EstadoService,LocalizacaoService ],
})
export class EstadoModule { }
