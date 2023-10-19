import { animate, style, transition, trigger } from "@angular/animations";
import { AfterViewInit, Component, OnInit, ViewChild } from "@angular/core";
import { Calendar } from "primeng/calendar";
import { Table } from "primeng/table";
import { AppComponent } from "src/app/app.component";
import { ListarComumComponent } from "src/app/shared/utils/comum.component";
import { environment } from "src/environments/environment";
import { PedidoFilterModel, PedidoModel } from "../../venda/model/pedido.model";
import { VendaService } from "../../venda/venda.service";

@Component({
    selector: 'app-produto',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],

    templateUrl: './rel-venda.component.html',
    styleUrls: ['./rel-venda.component.scss'],
    providers: []
})
export class RelatorioVendaComponent extends ListarComumComponent implements OnInit, AfterViewInit {

    loading!: boolean;
    baseImg = environment.production ? environment.apiUrl : environment.imgUrl;
    records: PedidoModel[] = []

    rangeDates!: Date[];
    maxDate: Date = new Date();


    @ViewChild('calendar') calendar!: Calendar;

    constructor(private vendaService: VendaService,
        private appComponent: AppComponent) {
        super();

    }
    override ngOnInit(): void {

        // this.rangeDates[0].setDate((new Date()).getDate() - 5);

        let today = new Date();
        let firstDate = new Date();
        firstDate.setDate(today.getDate());
        this.rangeDates = [firstDate, today];    

    }

    override ngAfterViewInit() {
        setTimeout(() => {
            this.pesquisar();
          }, 500);
    }

    pesquisar() {
        this.getRelatorio();
    }

    onTodayClick() {
        if (this.calendar) {
            if (this.calendar.value.length >= 2) {
                if (this.calendar.value[0] && !this.calendar.value[1]) {
                    this.calendar.value[1] = this.calendar.value[0];
                    this.rangeDates = [this.calendar.value[0], this.calendar.value[1]];
                    this.calendar.hideOverlay();
                }
            }
        }
    }

    onSelect_date(event: Event, el: any) {

        if (this.calendar) {

            if (this.calendar.value.length >= 2) {

                if (this.calendar.value[1])
                    this.calendar.hideOverlay();

            }
        }
    }

    getRelatorio(event?: any) {

        if (event === undefined) {
            event = new PedidoFilterModel();
        }

        if (this.calendar && this.calendar.value) {
            event.dataInicio = this.calendar.value[0];
            event.dataFim = this.calendar.value[1];
        } else {
            event.dataInicio = new Date();
            event.dataFim = new Date();
        }
        event.sortField = "dataCadastro";
        event.SortOrder = 0;

        this.loading = true;

        this.vendaService.getPedidoPaged(event)
            .then((res: any) => {
                res.subscribe((r: any) => {
                    this.loading = false;

                    if (r && r.sucesso === true && r.conteudo != null) {
                        this.records = r.conteudo.items;
                        this.totalRecords = r.conteudo.totalCount;
                    } else {
                        this.appComponent.showErrorMessage('Não foi possível recuperar os registros.', r.mensagemRetorno);
                    }
                },
                    () => {
                        this.loading = false;
                        this.appComponent.showErrorMessage('Não foi possível recuperar os registros.');
                    })
            })
            ;
    }

    getImageProduct(product: any) {
        if (product && product.urlArquivo)
            return this.baseImg + '/' + product.urlArquivo;

        return this.baseImg + '/Comuns/Images/produto-sem-img.png';
    }


    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }
    clear(table: Table) {
        table.clear();
    }

}