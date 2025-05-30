import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    FormsModule
  ]
})
export class LoginComponent {
  constructor(private router: Router) {}

  onLogin() {
    // Ici, vous pouvez ajouter la logique d'authentification
    // rediriger l'utilisateur vers la page en fonction de l'utilisateur connecté
    this.router.navigate(['/home/parking-map']);
  }
}
