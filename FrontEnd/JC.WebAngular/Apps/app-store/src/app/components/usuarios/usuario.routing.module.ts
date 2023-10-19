import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsuarioComponent } from './listar/usuarios.component';
import { ManterUsuarioComponent } from './manter/manter-usuarios.component';


const usuariosRoutes: Routes = [
    { path: '', component: UsuarioComponent },    
    { path: ':id', component: ManterUsuarioComponent }    
];

@NgModule({
    imports: [RouterModule.forChild(usuariosRoutes)],
    exports: [RouterModule]
})
export class UsuarioRoutingModule {}