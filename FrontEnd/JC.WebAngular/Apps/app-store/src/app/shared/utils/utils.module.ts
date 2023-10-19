
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GroupByPipe } from '../pipes/groupBy';
import { ButtonLoaderDirective } from './button-loading-directive';

@NgModule({
    imports: [
        CommonModule,
        FormsModule        
    ],
    providers: [

    ],
    declarations: [
        ButtonLoaderDirective,
        GroupByPipe
    ],
    exports: [
        ButtonLoaderDirective,
        GroupByPipe,        
    ]
})
export class UtilsModule { }
