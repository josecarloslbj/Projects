import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'crud', loadChildren: () => import('./crud/crud.module').then(m => m.CrudModule) },
        { path: 'empty', loadChildren: () => import('./empty/emptydemo.module').then(m => m.EmptyDemoModule) },
        { path: 'timeline', loadChildren: () => import('./timeline/timelinedemo.module').then(m => m.TimelineDemoModule) },

        {
            path: 'perfis',
            loadChildren: () => import('../../../../app/components/perfis/perfis.module').then(x => x.PerfisModule)
            // canActivate: [AuthGuard],
            // canLoad: [AuthGuard]
          },

        //   {
        //     path: 'usuarios',
        //     loadChildren: () => import('../../../../app/components/usuarios/usuario.module').then(x => x.UsuarioModule)
        //     // canActivate: [AuthGuard],
        //     // canLoad: [AuthGuard]
        //   },
    ])],
    exports: [RouterModule]
})
export class PagesRoutingModule { }
