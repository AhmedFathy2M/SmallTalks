import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home/home.component';
import { ChatComponent } from './chat-hall/chat/chat.component';
import { DashBoardComponent } from './core/dash-board/dash-board.component';
import { AuthGuard } from './account/guards/auth.guard';
import { UsersDataComponent } from './core/dash-board/users-data/users-data.component';
import { AboutUsComponent } from './core/dash-board/about-us/about-us.component';
import { AdminGuard } from './account/guards/admin.guard';
import { AdminComponent } from './core/dash-board/admin/admin.component';
const routes: Routes = 
[
  {path:'account', loadChildren:()=> import('./account/account.module').then(mod => mod.AccountModule)},
  { path: '', redirectTo: 'home', pathMatch: 'full' }, 
  { path: 'home', component: HomeComponent },
  {
    path: 'chat',
    component: ChatComponent,
    canActivate: [AuthGuard], 
  },
  { path: 'dashboard', component: DashBoardComponent,canActivate: [AdminGuard] },
  { path: 'users', component: UsersDataComponent,canActivate: [AdminGuard] },
  {path:'about-us', component:AboutUsComponent,canActivate: [AdminGuard]},
  {path:'admin', component:AdminComponent,canActivate: [AdminGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
