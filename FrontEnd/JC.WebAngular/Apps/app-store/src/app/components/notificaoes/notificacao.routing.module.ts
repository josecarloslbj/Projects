import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotificacaoComponent } from './notificacao.component';


const routes: Routes = [
    { path: '', component: NotificacaoComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class NotificacaoRoutingModule { }