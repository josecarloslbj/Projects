import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { PrimengModule } from '../../shared/primeng.module';
import { ManterProdutoComponent } from './manter/manter-produto.component';
import { ProdutoComponent } from './listar/produto.component';
import { ProdutoRoutingModule } from './produto.routing.module';
import { ProdutoService } from './produto.service';
import { ComponentesComunsModule } from "../../shared/componentes/componentes-comun.module";
import { UploadComponent } from 'src/app/shared/componentes/upload/upload.component';


@NgModule({
    exports: [],
    declarations: [
        ProdutoComponent,
        ManterProdutoComponent,        
    ],
    providers: [ProdutoService],
    imports: [
        CommonModule,
        FormsModule,
        ProdutoRoutingModule,
        UtilsModule,
        PrimengModule,
        ComponentesComunsModule,
    ]
})
export class ProdutoModule { }
