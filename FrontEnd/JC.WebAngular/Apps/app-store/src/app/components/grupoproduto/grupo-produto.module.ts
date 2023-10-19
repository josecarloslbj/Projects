import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { PrimengModule } from '../../shared/primeng.module';
import {  ManterGrupoProdutoComponent } from './manter/manter-grupo-produto.component';
import {  GrupoProdutoComponent } from './listar/grupo-produto.component';
import { GrupoProdutoRoutingModule } from './grupo-produto.routing.module';
import { GrupoProdutoService } from './grupo-produto.service';
@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        GrupoProdutoRoutingModule,
        UtilsModule,
        PrimengModule
    ],
    exports: [],
    declarations: [
        GrupoProdutoComponent,
        ManterGrupoProdutoComponent
    ],
    providers: [GrupoProdutoService],
})
export class GrupoProdutoModule { }
