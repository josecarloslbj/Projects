import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../../shared/utils/utils.module';
import { PrimengModule } from '../../../shared/primeng.module';
import { PerfisModule } from '../../perfis/perfis.module';
import { ManterBairroComponent } from './manter/manter-bairro.component';
import { BairroComponent } from './listar/bairro.component';
import { BairroRoutingModule } from './bairro-routing.module';
import { BairroService } from './bairro.service';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';
import { BlockUIModule } from 'primeng/blockui';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        BairroRoutingModule,
        UtilsModule,
        PrimengModule,
        PerfisModule,
        BlockUIModule
    ],
    exports: [],
    declarations: [
        BairroComponent,
        ManterBairroComponent
    ],
    providers: [BairroService, LocalizacaoService],
})
export class BairroModule { }
