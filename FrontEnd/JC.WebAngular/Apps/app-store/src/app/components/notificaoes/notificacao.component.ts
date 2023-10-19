import { animate, style, transition, trigger } from "@angular/animations";
import { AfterViewInit, Component, OnInit } from "@angular/core";

@Component({
    selector: 'app-notificacao',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],

    templateUrl: './notificacao.component.html',
    styleUrls: ['./notificacao.component.scss'],
    providers: []
})
export class NotificacaoComponent implements OnInit, AfterViewInit {

    selectedValues: string[] = [];
    constructor() {

    }

    ngOnInit(): void {

    }

    ngAfterViewInit() {

    }
}