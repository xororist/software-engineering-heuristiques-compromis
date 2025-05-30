import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { MapComponent } from './map/map.component';
import { SettingsComponent } from './settings/settings.component';
import { ManageUserComponent } from './manage-user/manage-user.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'home',
    component: HomeComponent,
    children: [

      { path: 'parking-map', component: MapComponent },
      { path: 'settings', component: ManageUserComponent },
      { path: '', redirectTo: 'parking-map', pathMatch: 'full' }
    ]
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];
