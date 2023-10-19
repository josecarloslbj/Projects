import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { PrimengModule } from '../../shared/primeng.module';
import { RelatorioVendaComponent } from './venda/rel-venda.component';
import { RelatorioRoutingModule } from './relatorio.routing.module';
import { VendaService } from '../venda/venda.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RelatorioRoutingModule,
        UtilsModule,
        PrimengModule
    ],
    exports: [],
    declarations: [
        RelatorioVendaComponent       
    ],
    providers: [VendaService]
})
export class RelatorioModule { }
