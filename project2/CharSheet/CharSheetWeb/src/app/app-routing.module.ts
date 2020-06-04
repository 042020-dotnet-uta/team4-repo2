import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreateFormComponent } from './create-form/create-form.component';
import { AccessCharSheetsComponent } from './access-char-sheets/access-char-sheets.component';
import { SelectedCharSheetComponent } from './selected-char-sheet/selected-char-sheet.component';


const routes: Routes = [
  {path:'selectedSheet/:p1',component:SelectedCharSheetComponent},
  { path: 'createForm', component: CreateFormComponent },
  { path: 'accessForm', component: AccessCharSheetsComponent },
  { path: '', redirectTo: 'accessForm', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
