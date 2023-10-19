import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];

    iconRootMenu: string = 'pi pi-chevron-circle-right';

    constructor(public layoutService: LayoutService) { }

    ngOnInit() {
        this.model = [
            {
                label: 'Principal',
                items: [
                    { label: 'Painel', icon: 'pi pi-fw pi-home', routerLink: ['/'] }
                ]
            },
            {
                label: 'Administração',
                icon: this.iconRootMenu,
                items: [

                    {
                        label: 'Segurança',
                        icon: 'pi pi-fw pi-shield',
                        items: [
                            { label: 'Perfil', icon: 'pi pi-fw pi-id-card', routerLink: ['../perfis'] },
                            { label: 'Usuários', icon: 'pi pi-fw pi-id-card', routerLink: ['../usuarios'] },
                        ]
                    },
                    {
                        label: 'Cadastros',
                        icon: 'pi pi-fw pi-book',
                        items: [
                            { label: 'Fabricante', icon: 'pi pi-fw pi-id-card', routerLink: ['/fabricante'] },
                            { label: 'Grupo de Produtos', icon: 'pi pi-fw pi-id-card', routerLink: ['/grupoproduto'] },
                            { label: 'Fornecedor', icon: 'pi pi-fw pi-id-card', routerLink: ['/fornecedor'] },
                            { label: 'Produto', icon: 'pi pi-fw pi-id-card', routerLink: ['/produto'] },
                            { label: 'Unidade', icon: 'pi pi-fw pi-id-card', routerLink: ['/unidades'] },
                        ]
                    },
                    {
                        label: 'Clientes',
                        icon: 'pi pi-fw pi-users',
                        items: [
                            { label: 'Clientes', icon: 'pi pi-fw pi-id-card', routerLink: ['/cliente'] },

                        ]
                    },
                    {
                        label: 'Localização',
                        icon: 'pi pi-fw pi-map',
                        items: [
                            { label: 'Pais', icon: 'pi pi-fw pi-map-marker', routerLink: ['../pais'] },
                            { label: 'Estados', icon: 'pi pi-fw pi-map-marker', routerLink: ['../estado'] },
                            { label: 'Cidades', icon: 'pi pi-fw pi-map-marker', routerLink: ['../cidade'] },
                            { label: 'Bairros', icon: 'pi pi-fw pi-map-marker', routerLink: ['../bairro'] },
                        ]
                    },
                    {
                        label: 'Compra/Venda',
                        icon: 'pi pi-fw pi-gift',
                        items: [
                            { label: 'Gerenciar Estoque', icon: 'pi pi-fw pi-id-card' },

                        ]
                    },
                ]
            },

            {
                label: 'Realizar Vendas',
                icon: this.iconRootMenu,
                items: [
                    { label: 'Caixa', icon: 'pi pi-fw pi-wallet', routerLink: ['/caixa'] },
                    { label: 'Abertura/Fechamento caixa', icon: 'pi pi-fw pi-dollar' , routerLink: ['/gerenciar-caixa/status']  },
                    { label: 'Estornar', icon: 'pi pi-fw pi-exclamation-triangle' },
                ]
            },
            {
                label: 'Relatórios',
                icon: this.iconRootMenu,
                items: [
                    {
                        label: 'Vendas', icon: 'pi pi-fw pi-chart-line',
                        items: [
                            {  label: 'Vendas detalhada', icon: 'pi pi-fw pi-bookmark', routerLink: ['/relatorios/venda']  },
                            {  label: 'Relatório mensal', icon: 'pi pi-fw pi-bookmark'                            },
                        ]
                    },
                    {
                        label: 'Estoque', icon: 'pi pi-fw pi-chart-line',
                        items: [
                            {
                                label: 'Submenu 2.1', icon: 'pi pi-fw pi-bookmark',
                                items: [
                                    { label: 'Submenu 2.1.1', icon: 'pi pi-fw pi-bookmark' },
                                    { label: 'Submenu 2.1.2', icon: 'pi pi-fw pi-bookmark' },
                                ]
                            },
                            {
                                label: 'Submenu 2.2', icon: 'pi pi-fw pi-bookmark',
                                items: [
                                    { label: 'Submenu 2.2.1', icon: 'pi pi-fw pi-bookmark' },
                                ]
                            },
                        ]
                    }
                ]
            },

            {
                label: 'Configurar Notificações',
                icon: this.iconRootMenu,
                items: [
                    { label: 'Notificações', icon: 'pi pi-fw pi-slack', routerLink: ['/notificacoes'] },
                ]
            },

            // {
            //     label: 'Cadastros',
            //     items: [
            //         { label: 'Fornecedor', icon: 'pi pi-fw pi-id-card', routerLink: ['../fornecedor'] },
            //         { label: 'Produto', icon: 'pi pi-fw pi-id-card', routerLink: ['/'] },
            //         ]
            // },

            // {
            //     label: 'Administração',
            //     items: [
            //         { label: 'Perfil', icon: 'pi pi-fw pi-id-card', routerLink: ['../perfis'] },
            //         { label: 'Usuários', icon: 'pi pi-fw pi-id-card', routerLink: ['../usuarios'] },
            //         { label: 'Form Layout', icon: 'pi pi-fw pi-id-card', routerLink: ['/uikit/formlayout'] },
            //         { label: 'Input', icon: 'pi pi-fw pi-check-square', routerLink: ['/uikit/input'] },
            //         { label: 'Float Label', icon: 'pi pi-fw pi-bookmark', routerLink: ['/uikit/floatlabel'] },
            //         { label: 'Invalid State', icon: 'pi pi-fw pi-exclamation-circle', routerLink: ['/uikit/invalidstate'] },
            //         { label: 'Button', icon: 'pi pi-fw pi-mobile', routerLink: ['/uikit/button'], class: 'rotated-icon' },
            //         { label: 'Table', icon: 'pi pi-fw pi-table', routerLink: ['/uikit/table'] },
            //         { label: 'List', icon: 'pi pi-fw pi-list', routerLink: ['/uikit/list'] },
            //         { label: 'Tree', icon: 'pi pi-fw pi-share-alt', routerLink: ['/uikit/tree'] },
            //         { label: 'Panel', icon: 'pi pi-fw pi-tablet', routerLink: ['/uikit/panel'] },
            //         { label: 'Overlay', icon: 'pi pi-fw pi-clone', routerLink: ['/uikit/overlay'] },
            //         { label: 'Media', icon: 'pi pi-fw pi-image', routerLink: ['/uikit/media'] },
            //         { label: 'Menu', icon: 'pi pi-fw pi-bars', routerLink: ['/uikit/menu'], preventExact: true },
            //         { label: 'Message', icon: 'pi pi-fw pi-comment', routerLink: ['/uikit/message'] },
            //         { label: 'File', icon: 'pi pi-fw pi-file', routerLink: ['/uikit/file'] },
            //         { label: 'Chart', icon: 'pi pi-fw pi-chart-bar', routerLink: ['/uikit/charts'] },
            //         { label: 'Misc', icon: 'pi pi-fw pi-circle', routerLink: ['/uikit/misc'] }
            //     ]
            // },

            // {
            //     label: 'Cadastros',
            //     items: [
            //         { label: 'Fornecedor', icon: 'pi pi-fw pi-id-card', routerLink: ['/'] },
            //         { label: 'Produto', icon: 'pi pi-fw pi-id-card', routerLink: ['/'] },
            //         ]
            // },          
            // {
            //     label: 'Relatórios',
            //     items: [
            //         { label: 'PrimeIcons', icon: 'pi pi-fw pi-prime', routerLink: ['/utilities/icons'] },
            //         { label: 'PrimeFlex', icon: 'pi pi-fw pi-desktop', url: ['https://www.primefaces.org/primeflex/'], target: '_blank' },
            //     ]
            // },
            // {
            //     label: 'Pages',
            //     icon: 'pi pi-fw pi-briefcase',
            //     items: [
            //         {
            //             label: 'Landing',
            //             icon: 'pi pi-fw pi-globe',
            //             routerLink: ['/landing']
            //         },
            //         {
            //             label: 'Auth',
            //             icon: 'pi pi-fw pi-user',
            //             items: [
            //                 {
            //                     label: 'Login',
            //                     icon: 'pi pi-fw pi-sign-in',
            //                     routerLink: ['/auth/login']
            //                 },
            //                 {
            //                     label: 'Error',
            //                     icon: 'pi pi-fw pi-times-circle',
            //                     routerLink: ['/auth/error']
            //                 },
            //                 {
            //                     label: 'Access Denied',
            //                     icon: 'pi pi-fw pi-lock',
            //                     routerLink: ['/auth/access']
            //                 }
            //             ]
            //         },
            //         {
            //             label: 'Crud',
            //             icon: 'pi pi-fw pi-pencil',
            //             routerLink: ['/pages/crud']
            //         },
            //         {
            //             label: 'Perfis',
            //             icon: 'pi pi-fw pi-pencil',
            //             routerLink: ['/pages/perfis']
            //         },
            //         {
            //             label: 'Timeline',
            //             icon: 'pi pi-fw pi-calendar',
            //             routerLink: ['/pages/timeline']
            //         },
            //         {
            //             label: 'Not Found',
            //             icon: 'pi pi-fw pi-exclamation-circle',
            //             routerLink: ['/pages/notfound']
            //         },
            //         {
            //             label: 'Empty',
            //             icon: 'pi pi-fw pi-circle-off',
            //             routerLink: ['/pages/empty']
            //         },
            //     ]
            // },
            //  {
            //     label: 'Hierarchy',
            //     items: [
            //         {
            //             label: 'Submenu 1', icon: 'pi pi-fw pi-bookmark',
            //             items: [
            //                 {
            //                     label: 'Submenu 1.1', icon: 'pi pi-fw pi-bookmark',
            //                     items: [
            //                         { label: 'Submenu 1.1.1', icon: 'pi pi-fw pi-bookmark' },
            //                         { label: 'Submenu 1.1.2', icon: 'pi pi-fw pi-bookmark' },
            //                         { label: 'Submenu 1.1.3', icon: 'pi pi-fw pi-bookmark' },
            //                     ]
            //                 },
            //                 {
            //                     label: 'Submenu 1.2', icon: 'pi pi-fw pi-bookmark',
            //                     items: [
            //                         { label: 'Submenu 1.2.1', icon: 'pi pi-fw pi-bookmark' }
            //                     ]
            //                 },
            //             ]
            //         },
            //         {
            //             label: 'Submenu 2', icon: 'pi pi-fw pi-bookmark',
            //             items: [
            //                 {
            //                     label: 'Submenu 2.1', icon: 'pi pi-fw pi-bookmark',
            //                     items: [
            //                         { label: 'Submenu 2.1.1', icon: 'pi pi-fw pi-bookmark' },
            //                         { label: 'Submenu 2.1.2', icon: 'pi pi-fw pi-bookmark' },
            //                     ]
            //                 },
            //                 {
            //                     label: 'Submenu 2.2', icon: 'pi pi-fw pi-bookmark',
            //                     items: [
            //                         { label: 'Submenu 2.2.1', icon: 'pi pi-fw pi-bookmark' },
            //                     ]
            //                 },
            //             ]
            //         }
            //     ]
            // },
            // {
            //     label: 'Get Started',
            //     items: [
            //         {
            //             label: 'Documentation', icon: 'pi pi-fw pi-question', routerLink: ['/documentation']
            //         },
            //         {
            //             label: 'View Source', icon: 'pi pi-fw pi-search', url: ['https://github.com/primefaces/sakai-ng'], target: '_blank'
            //         }
            //     ]
            // }
        ];
    }
}
