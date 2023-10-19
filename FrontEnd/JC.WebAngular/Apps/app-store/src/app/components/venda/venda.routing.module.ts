import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AberturaFechamentoCaixaComponent } from './aberturafechamentocaixa/aberturafechamentocaixa.component';
import { CupomCaixaComponent } from './cupom/cupom.component';
import { VendaCaixaComponent } from './realizar/caixa.component';

const routes: Routes = [
    { path: '', component: VendaCaixaComponent },
    { path: 'cupom', component: CupomCaixaComponent },
    { path: 'status', component: AberturaFechamentoCaixaComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class VendaRoutingModule { }