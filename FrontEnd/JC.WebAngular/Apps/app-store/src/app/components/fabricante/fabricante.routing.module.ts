import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FabricanteComponent } from './listar/fabricante.component';
import { ManterFabricanteComponent } from './manter/manter-fabricante.component';


const routes: Routes = [
    { path: '', component: FabricanteComponent },    
    { path: ':id', component: ManterFabricanteComponent }    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class FabricanteRoutingModule {}