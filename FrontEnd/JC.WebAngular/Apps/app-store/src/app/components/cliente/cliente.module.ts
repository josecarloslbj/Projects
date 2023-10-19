import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { PrimengModule } from '../../shared/primeng.module';
import { ManterClienteComponent } from './manter/manter-cliente.component';
import { ClienteComponent  } from './listar/cliente.component';
import { PerfisService } from '../perfis/perfis.service';
import { ClienteRoutingModule } from './cliente.routing.module';
import { ClienteService } from './cliente.service';
import { ComponentesComunsModule } from 'src/app/shared/componentes/componentes-comun.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ClienteRoutingModule,
        UtilsModule,
        PrimengModule,
        ComponentesComunsModule
    ],
    exports: [],
    declarations: [
        ClienteComponent,
        ManterClienteComponent
    ],
    providers: [ClienteService,PerfisService],
})
export class ClienteModule { }
