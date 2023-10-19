import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { PrimengModule } from '../../shared/primeng.module';
import { NotificacaoComponent } from './notificacao.component';
import { NotificacaoRoutingModule } from './notificacao.routing.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        NotificacaoRoutingModule,
        UtilsModule,
        PrimengModule
    ],
    exports: [],
    declarations: [
        NotificacaoComponent
    ],
    providers: []
})
export class NotificacaoModule { }
