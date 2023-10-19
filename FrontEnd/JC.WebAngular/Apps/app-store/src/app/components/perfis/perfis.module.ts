import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PerfisRoutingModule } from './perfis.routing.module';
import { PerfisComponent } from './listar/perfis.component';
import { PerfisService } from './perfis.service';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { ManterPerfisComponent } from './manter/manter-perfis.component';
import { PrimengModule } from '../../shared/primeng.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        PerfisRoutingModule,
        UtilsModule,
        PrimengModule
    ],
    exports: [],
    declarations: [
        PerfisComponent,
        ManterPerfisComponent
    ],
    providers: [PerfisService],
})
export class PerfisModule { }
