import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CidadeComponent } from './listar/cidade.component';
import { ManterCidadeComponent } from './manter/manter-cidade.component';

const usuariosRoutes: Routes = [
    { path: '', component: CidadeComponent },    
    { path: ':id', component: ManterCidadeComponent }
    // { path: 'naoEncontrado', component: UsuarioNaoEncontradoComponent },    
];

@NgModule({
    imports: [RouterModule.forChild(usuariosRoutes)],
    exports: [RouterModule]
})
export class CidadeRoutingModule {}