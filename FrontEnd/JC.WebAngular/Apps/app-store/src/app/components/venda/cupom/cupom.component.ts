
import { Router } from '@angular/router';
import { Component, OnInit, AfterViewInit,  Injectable } from '@angular/core';
import {
    trigger,
    state,
    style,
    animate,
    transition,
    // ...
} from '@angular/animations';
import {  CupomFiscalModel, ProdutoVendaModel } from '../../produto/model/produto-model';


@Injectable({
    providedIn: 'root'
})
@Component({
    selector: 'app-cupom-caixa',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],        // end of trigger block    
    templateUrl: './cupom.component.html',
    styleUrls: ['./cupom.component.scss']
})
export class CupomCaixaComponent implements OnInit, AfterViewInit {

    
    cupomFiscalModel: CupomFiscalModel = new CupomFiscalModel();
    constructor(
       
    ) { }

    ngOnInit() {
        
    }

    ngAfterViewInit() {

        let userToken = localStorage.getItem('cupom-produtosFinalizaVenda');
        this.cupomFiscalModel = JSON.parse(userToken!);       


        
        setTimeout(() => {
            window.print();
        }, 100);

       // window.print();
    }

    ngOnDestroy() {
     
    }
}

