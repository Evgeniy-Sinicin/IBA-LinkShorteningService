import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LinkService } from '../services/link.service';
import { GroupsComponent } from './groups/groups.component';
import { BrowserModule } from '@angular/platform-browser';
import { NewGroupComponent } from './groups/new-group/new-group.component';
import { LinksComponent } from './links/links.component';
import { NewLinkComponent } from './links/new-link/new-link.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FormsModule  } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 

export const routes: Routes = [
  {
    // Путь, на который перешёл пользователь
    path: '',
    redirectTo: 'groups',
    pathMatch: 'full'
  },
  {
    path: 'groups',
    component: GroupsComponent
  },
  {
    path: 'links',
    component: LinksComponent
  },
  {
    path: 'new-group',
    component: NewGroupComponent
  },
  {
    path: 'new-link/:id',
    component: NewLinkComponent
  },
  {
    path: 'group/:id',
    component: LinksComponent
  }
];

@NgModule({
  declarations: [
    GroupsComponent,
    NewGroupComponent,
    LinksComponent,
    NewLinkComponent],
  exports: [],
  imports: [
    BrowserModule,
    RouterModule,
    RouterModule.forRoot(routes),
    CommonModule,
    NgxSpinnerModule,
    FormsModule,
    BrowserAnimationsModule
  ],
  providers: [
    Location,
    LinkService
  ]
})
export class DashboardModule { }