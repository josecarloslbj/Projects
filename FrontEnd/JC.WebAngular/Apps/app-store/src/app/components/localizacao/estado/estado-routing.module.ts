import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EstadoComponent } from './listar/estado.component';
import { ManterEstadoComponent } from './manter/manter-estado.component';

const usuariosRoutes: Routes = [
    { path: '', component: EstadoComponent },    
    { path: ':id', component: ManterEstadoComponent }
    // { path: 'naoEncontrado', component: UsuarioNaoEncontradoComponent },    
];

@NgModule({
    imports: [RouterModule.forChild(usuariosRoutes)],
    exports: [RouterModule]
})
export class EstadoRoutingModule {}