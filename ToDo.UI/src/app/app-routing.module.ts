import { NgModule } from '@angular/core';
import { Routes, RouterModule, ExtraOptions } from '@angular/router';
import { AuthGuard } from './guard/auth.guard';
import { MauthComponent } from './pages/mauth/mauth.component';
import { TaskComponent } from './pages/task-component/task-component';


export const routes: Routes = [
  {
     path:'mauth',
     component: MauthComponent,
  },
  {
    path:'tasks',
    component: TaskComponent,
    canActivate:[AuthGuard]
  },
  { path: '', redirectTo: 'tasks', pathMatch: 'full' },
  { path: '**', redirectTo: 'tasks' },
];

const config: ExtraOptions = {
  useHash: false,
};

@NgModule({
  imports: [RouterModule.forRoot(routes,config)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
