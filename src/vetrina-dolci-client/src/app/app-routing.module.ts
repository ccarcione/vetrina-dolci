import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BackofficeComponent } from './components/backoffice/backoffice.component';
import { DolciInVenditaComponent } from './components/dolci-in-vendita/dolci-in-vendita.component';

const routes: Routes = [
  {
    path:'backoffice',
    component: BackofficeComponent
  },
  {
    path: 'dolci-in-vendita',
    component: DolciInVenditaComponent,
  },
  {
    path: '',
    component: DolciInVenditaComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
