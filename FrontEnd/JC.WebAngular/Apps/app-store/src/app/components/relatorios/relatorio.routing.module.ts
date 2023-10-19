import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RelatorioVendaComponent } from './venda/rel-venda.component';

const routes: Routes = [
    { path: 'venda', component: RelatorioVendaComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RelatorioRoutingModule { }