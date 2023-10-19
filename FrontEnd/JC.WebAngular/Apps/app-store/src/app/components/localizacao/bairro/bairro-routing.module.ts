import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BairroComponent } from './listar/bairro.component';
import { ManterBairroComponent } from './manter/manter-bairro.component';

const usuariosRoutes: Routes = [
    { path: '', component: BairroComponent },    
    { path: ':id', component: ManterBairroComponent }
    // { path: 'naoEncontrado', component: UsuarioNaoEncontradoComponent },    
];

@NgModule({
    imports: [RouterModule.forChild(usuariosRoutes)],
    exports: [RouterModule]
})
export class BairroRoutingModule {}