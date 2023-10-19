import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { PrimengModule } from '../../shared/primeng.module';
import { ManterUsuarioComponent } from './manter/manter-usuarios.component';
import { UsuarioComponent  } from './listar/usuarios.component';
import { UsuarioRoutingModule } from './usuario.routing.module';
import { UsuarioService } from './usuario.service';
import { PerfisService } from '../perfis/perfis.service';
import { ComponentesComunsModule } from 'src/app/shared/componentes/componentes-comun.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        UsuarioRoutingModule,
        UtilsModule,
        PrimengModule,
        ComponentesComunsModule
    ],
    exports: [],
    declarations: [
        UsuarioComponent,
        ManterUsuarioComponent
    ],
    providers: [UsuarioService,PerfisService],
})
export class UsuarioModule { }
