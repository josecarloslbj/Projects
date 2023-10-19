import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { PrimengModule } from '../../shared/primeng.module';
import { PerfisService } from '../perfis/perfis.service';
import { FornecedorRoutingModule } from './fornecedor.routing.module';
import { FornecedorComponent } from './listar/fornecedor.component';
import { ManterFornecedorComponent } from './manter/manter-fornecedor.component';
import { FornecedorService } from './fornecedor.service';
import { UsuarioService } from '../usuarios/usuario.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        FornecedorRoutingModule,
        UtilsModule,
        PrimengModule
    ],
    exports: [],
    declarations: [
        FornecedorComponent,
        ManterFornecedorComponent
    ],
    providers: [FornecedorService, PerfisService, UsuarioService ],
})
export class FornecedorModule { }
