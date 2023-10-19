import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FornecedorComponent } from './listar/fornecedor.component';
import { ManterFornecedorComponent } from './manter/manter-fornecedor.component';


const routes: Routes = [
    { path: '', component: FornecedorComponent },    
    { path: ':id', component: ManterFornecedorComponent }    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class FornecedorRoutingModule {}