import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProdutoComponent } from './listar/produto.component';
import { ManterProdutoComponent } from './manter/manter-produto.component';

const routes: Routes = [
    { path: '', component: ProdutoComponent },
    { path: ':id', component: ManterProdutoComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProdutoRoutingModule { }