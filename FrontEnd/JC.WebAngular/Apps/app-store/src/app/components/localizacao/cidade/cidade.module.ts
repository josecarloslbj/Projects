import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../../shared/utils/utils.module';
import { PrimengModule } from '../../../shared/primeng.module';
import { ManterCidadeComponent } from './manter/manter-cidade.component';
import { CidadeComponent } from './listar/cidade.component';
import { CidadeRoutingModule } from './cidade-routing.module';
import { CidadeService } from './cidade.service';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        CidadeRoutingModule,
        UtilsModule,
        PrimengModule        
    ],
    exports: [],
    declarations: [
        CidadeComponent,
        ManterCidadeComponent
    ],
    providers: [CidadeService, LocalizacaoService],
})
export class CidadeModule { }
