import { Injectable, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UtilsModule } from '../../shared/utils/utils.module';
import { PrimengModule } from '../../shared/primeng.module';
import { UploadComponent } from './upload/upload.component';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ClienteRoutingModule } from 'src/app/components/cliente/cliente.routing.module';
import { UploadService } from './upload/upload.service';

@Injectable({
    providedIn: 'root'
})
@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ClienteRoutingModule,
        UtilsModule,
        PrimengModule
    ],
    exports: [UploadComponent],
    declarations: [
        UploadComponent
    ],
     providers: [
        UploadService
        // MessageService, ConfirmationService
    ],
    schemas: []
})
export class ComponentesComunsModule { }
