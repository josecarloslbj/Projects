import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManterUnidadeComponent } from './manter/manter-unidade.component';
import { UnidadeComponent } from './listar/listar-unidade.component';

const usuariosRoutes: Routes = [
    { path: '', component: UnidadeComponent },    
    { path: ':id', component: ManterUnidadeComponent }    
];

@NgModule({
    imports: [RouterModule.forChild(usuariosRoutes)],
    exports: [RouterModule]
})
export class UnidadeRoutingModule {}