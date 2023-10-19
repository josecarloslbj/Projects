import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { PrimengModule } from '../../shared/primeng.module';
import { VendaCaixaComponent } from './realizar/caixa.component';
import { AuthenticationService } from 'src/app/login/authentication.service';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AutoFocusModule } from 'primeng/autofocus';
import { ProdutoService } from '../produto/produto.service';
import { VendaService } from './venda.service';
import { ProdutoComponent } from '../produto/listar/produto.component';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ClienteService } from '../cliente/cliente.service';
import { CupomCaixaComponent } from './cupom/cupom.component';
import { VendaRoutingModule } from './venda.routing.module';
import { AberturaFechamentoCaixaComponent } from './aberturafechamentocaixa/aberturafechamentocaixa.component';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        UtilsModule,
        VendaRoutingModule,
        PrimengModule,
        AutoFocusModule,

    ],
    exports: [],
    declarations: [
        VendaCaixaComponent,
        CupomCaixaComponent,
        AberturaFechamentoCaixaComponent
    ],
    providers: [AuthenticationService, ProdutoService, VendaService, ClienteService,
        DialogService, DynamicDialogRef, DynamicDialogConfig, ProdutoComponent
       
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class VendaModule { }