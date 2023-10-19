import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GrupoProdutoComponent } from './listar/grupo-produto.component';
import { ManterGrupoProdutoComponent } from './manter/manter-grupo-produto.component';



const routes: Routes = [
    { path: '', component: GrupoProdutoComponent },    
    { path: ':id', component: ManterGrupoProdutoComponent }    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class GrupoProdutoRoutingModule {}