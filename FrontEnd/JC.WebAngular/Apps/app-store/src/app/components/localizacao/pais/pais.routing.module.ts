import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PaisComponent } from './listar/pais-component';
import { ManterPaisComponent } from './manter/manter-pais-component';

const rotas: Routes = [
    { path: '', component: PaisComponent },    
    { path: ':id', component: ManterPaisComponent }
    // { path: 'naoEncontrado', component: UsuarioNaoEncontradoComponent },    
];

@NgModule({
    imports: [RouterModule.forChild(rotas)],
    exports: [RouterModule]
})
export class PaisRoutingModule {}