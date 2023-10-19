import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VendaCaixaComponent } from './components/venda/realizar/caixa.component';
import { NotfoundComponent } from './demo/components/notfound/notfound.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { AppLayoutComponent } from './layout/app.layout.component';
import { LoginComponent } from './login/login.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      {
        path: '', component: AppLayoutComponent,
        children: [
          { path: '', loadChildren: () => import('./demo/components/dashboard/dashboard.module').then(m => m.DashboardModule) },
          { path: 'uikit', loadChildren: () => import('./demo/components/uikit/uikit.module').then(m => m.UIkitModule) },
          { path: 'utilities', loadChildren: () => import('./demo/components/utilities/utilities.module').then(m => m.UtilitiesModule) },
          //{ path: 'documentation', loadChildren: () => import('./demo/components/documentation/documentation.module').then(m => m.DocumentationModule) },
          { path: 'blocks', loadChildren: () => import('./demo/components/primeblocks/primeblocks.module').then(m => m.PrimeBlocksModule) },
          { path: 'pages', loadChildren: () => import('./demo/components/pages/pages.module').then(m => m.PagesModule) },

          {
            path: 'perfis',
            loadChildren: () => import('../app/components/perfis/perfis.module').then(x => x.PerfisModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
          {
            path: 'usuarios',
            loadChildren: () => import('../app/components/usuarios/usuario.module').then(x => x.UsuarioModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
          {
            path: 'cliente',
            loadChildren: () => import('../app/components/cliente/cliente.module').then(x => x.ClienteModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
          {
            path: 'pais',
            loadChildren: () => import('../app/components/localizacao/pais/pais.module').then(x => x.PaisModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
          {
            path: 'estado',
            loadChildren: () => import('../app/components/localizacao/estado/estado.module').then(x => x.EstadoModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
          {
            path: 'cidade',
            loadChildren: () => import('../app/components/localizacao/cidade/cidade.module').then(x => x.CidadeModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
          {
            path: 'bairro',
            loadChildren: () => import('../app/components/localizacao/bairro/bairro.module').then(x => x.BairroModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
          {
            path: 'fabricante',
            loadChildren: () => import('../app/components/fabricante/fabricante.module').then(x => x.FabricanteModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
          {
            path: 'fornecedor',
            loadChildren: () => import('../app/components/fornecedor/fornecedor.module').then(x => x.FornecedorModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
          {
            path: 'grupoproduto',
            loadChildren: () => import('../app/components/grupoproduto/grupo-produto.module').then(x => x.GrupoProdutoModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
          {
            path: 'produto',
            loadChildren: () => import('../app/components/produto/produto.module').then(x => x.ProdutoModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
          {
            path: 'relatorios',
            loadChildren: () => import('../app/components/relatorios/relatorio.module').then(x => x.RelatorioModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },

          {
            path: 'notificacoes',
            loadChildren: () => import('../app/components/notificaoes/notificacao.module').then(x => x.NotificacaoModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
         
          {
            path: 'gerenciar-caixa',
            loadChildren: () => import('../app/components/venda/venda.module').then(x => x.VendaModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },
            {
            path: 'unidades',
            loadChildren: () => import('../app/components/unidades/unidade.module').then(x => x.UnidadeModule),
            canActivate: [AuthGuard],
            canLoad: [AuthGuard]
          },

        ],
        canActivate: [AuthGuard],
        canLoad: [AuthGuard]
      },

      { path: 'login', component: LoginComponent },
      { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
      // { path: 'caixa', component: VendaCaixaComponent, canActivate: [AuthGuard] },
      {
        path: 'caixa',
        loadChildren: () => import('../app/components/venda/venda.module').then(x => x.VendaModule),
        canActivate: [AuthGuard],
        canLoad: [AuthGuard]
      },
      
      { path: 'auth', loadChildren: () => import('./demo/components/auth/auth.module').then(m => m.AuthModule) },
      { path: 'pages/notfound', component: NotfoundComponent },
      { path: '**', redirectTo: 'pages/notfound' },
    ], { scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload' })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}