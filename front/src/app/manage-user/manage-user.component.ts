import { Component, OnInit } from '@angular/core';
import { UserService, User } from '../services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-manage-user',
  templateUrl: './manage-user.component.html',
  styleUrls: ['./manage-user.component.css'],
  standalone: true,
  imports: [
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule,
    DatePipe
  ]
})
export class ManageUserComponent implements OnInit {
  users: User[] = [];
  displayedColumns: string[] = ['username', 'email', 'role', 'createdAt', 'actions'];

  constructor(
    private userService: UserService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getAllUsers().subscribe({
      next: (users) => {
        this.users = users;
      },
      error: (error) => {
        this.showMessage('Erreur lors du chargement des utilisateurs');
      }
    });
  }

  deleteUser(userId: string): void {
    if (confirm('Êtes-vous sûr de vouloir supprimer cet utilisateur ?')) {
      this.userService.deleteUser(userId).subscribe({
        next: () => {
          this.loadUsers();
          this.showMessage('Utilisateur supprimé avec succès');
        },
        error: (error) => {
          this.showMessage('Erreur lors de la suppression de l\'utilisateur');
        }
      });
    }
  }

  updateUser(user: User): void {
    // TODO: Implémenter la logique de mise à jour
    console.log('Mise à jour de l\'utilisateur:', user);
  }

  makeReservation(user: User): void {
    // TODO: Implémenter la logique de réservation
    console.log('Création d\'une réservation pour:', user);
  }

  private showMessage(message: string): void {
    this.snackBar.open(message, 'Fermer', {
      duration: 3000,
      horizontalPosition: 'end',
      verticalPosition: 'top'
    });
  }
}
