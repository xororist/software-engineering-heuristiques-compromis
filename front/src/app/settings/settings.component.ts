import { Component } from '@angular/core';
import {CancelReservationRequest, Reservation} from '../Model/reservation';
import {ReservationManagementService} from '../../service/reservation-management.service';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {MatIconModule} from '@angular/material/icon';

@Component({
  selector: 'app-settings',
  imports: [CommonModule,FormsModule, MatIconModule],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {
  userIdInput: string = '';
  reservations: Reservation[] = [];
  selectedReservation: Reservation | null = null;
  showConfirmDialog: boolean = false;

  constructor(private reservationService: ReservationManagementService) {}

  fetchReservations(): void {
    if (!this.userIdInput) {
      alert('Veuillez entrer un User ID');
      return;
    }

    this.reservationService.getOngoingReservations(this.userIdInput).subscribe(
      (data: Reservation[]) => {
        this.reservations = data || [];
      },
      (error: any) => {
        console.error('Erreur lors de la récupération des réservations', error);
        this.reservations = [];
      }
    );
  }

  confirmDelete(reservation: Reservation): void {
    this.selectedReservation = reservation;
    this.showConfirmDialog = true;
  }

  cancelDelete(): void {
    this.selectedReservation = null;
    this.showConfirmDialog = false;
  }

  deleteReservation(): void {
    if (!this.selectedReservation || !this.userIdInput) {
      alert('Erreur : données manquantes');
      return;
    }

    const request: CancelReservationRequest = {
      reservationId: this.selectedReservation.id,
      userId: this.userIdInput
    };

    this.reservationService.cancelReservation(request).subscribe(
      (): void => {
        alert('Réservation annulée avec succès');
        this.fetchReservations();
        this.cancelDelete();
      },
      (error: any): void => {
        console.error('Erreur lors de l\'annulation de la réservation', error);
        alert('Erreur lors de l\'annulation');
        this.cancelDelete();
      }
    );
  }

}
