import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { MauthComponent } from './mauth.component';
  

const routes: Routes = [{
  path: '',
  component: MauthComponent,

}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MauthModule {}

