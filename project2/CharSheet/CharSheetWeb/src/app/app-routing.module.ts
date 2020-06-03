import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateFormComponent } from './create-form/create-form.component';
import { AccessCharSheetsComponent } from './access-char-sheets/access-char-sheets.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'createForm', component: CreateFormComponent },
  { path: 'accessForm', component: AccessCharSheetsComponent },
  { path: '', redirectTo: 'accessForm', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
