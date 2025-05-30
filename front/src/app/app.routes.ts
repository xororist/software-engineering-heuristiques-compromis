import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { MapComponent } from './map/map.component';
import { SettingsComponent } from './settings/settings.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'home',
    component: HomeComponent,
    children: [
      { path: 'settings', component: SettingsComponent },
      { path: 'parking-map', component: MapComponent },
      { path: '', redirectTo: 'map', pathMatch: 'full' }
    ]
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];
