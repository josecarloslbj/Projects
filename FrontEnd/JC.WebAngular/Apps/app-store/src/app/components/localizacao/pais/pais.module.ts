import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaisRoutingModule } from './pais.routing.module';
import { PaisComponent } from './listar/pais-component';
import { PaisService } from './pais.service';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../../shared/utils/utils.module';
import { ManterPaisComponent } from './manter/manter-pais-component';
import { PrimengModule } from '../../../shared/primeng.module';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';
import { ComponentesComunsModule } from 'src/app/shared/componentes/componentes-comun.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        PaisRoutingModule,
        UtilsModule,
        PrimengModule,
        ComponentesComunsModule,
    ],
    exports: [],
    declarations: [
        PaisComponent,
        ManterPaisComponent
    ],
    providers: [PaisService, LocalizacaoService],
})
export class PaisModule { }
