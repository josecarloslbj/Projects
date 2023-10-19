import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PerfisComponent } from './listar/perfis.component';
import { ManterPerfisComponent } from './manter/manter-perfis.component';


const usuariosRoutes: Routes = [
    { path: '', component: PerfisComponent },
    // { path: 'novo', component: ManterPerfisComponent },
    { path: ':id', component: ManterPerfisComponent }
    // { path: 'naoEncontrado', component: UsuarioNaoEncontradoComponent },
    // { path: ':id', component: UsuarioDetalheComponent }
];

@NgModule({
    imports: [RouterModule.forChild(usuariosRoutes)],
    exports: [RouterModule]
})
export class PerfisRoutingModule {}