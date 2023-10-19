import { Component, OnInit, AfterViewInit } from '@angular/core';

@Component({
    selector: 'app-listar-comum',
    template: ` `
})
export class ListarComumComponent implements OnInit, AfterViewInit {

    defaultRow: number = 10;
    totalRecords!: number;
    selectedRecords: any[] = [];
    selectAllRecords: boolean = false;

    constructor() { }

    ngOnInit() { }

    ngAfterViewInit() { }

    ngOnDestroy() { }
}