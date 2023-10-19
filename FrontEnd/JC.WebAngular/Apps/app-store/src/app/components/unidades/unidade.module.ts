import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { PrimengModule } from '../../shared/primeng.module';
import { ComponentesComunsModule } from 'src/app/shared/componentes/componentes-comun.module';
import { ManterUnidadeComponent } from './manter/manter-unidade.component';
import { UnidadeComponent } from './listar/listar-unidade.component';
import { UnidadeService } from './unidade.service';
import { UnidadeRoutingModule } from './unidade.routing.module';
import { UsuarioService } from '../usuarios/usuario.service';
import { PerfisService } from '../perfis/perfis.service';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        UnidadeRoutingModule,
        UtilsModule,
        PrimengModule,
        ComponentesComunsModule
    ],
    exports: [],
    declarations: [
        UnidadeComponent,
        ManterUnidadeComponent
    ],
    providers: [UnidadeService, UsuarioService, LocalizacaoService],
})
export class UnidadeModule { }
