import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClienteComponent } from './listar/cliente.component';
import { ManterClienteComponent } from './manter/manter-cliente.component';

const routes: Routes = [
    { path: '', component: ClienteComponent },    
    { path: ':id', component: ManterClienteComponent }    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ClienteRoutingModule {}