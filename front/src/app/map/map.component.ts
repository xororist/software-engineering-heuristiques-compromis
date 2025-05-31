import { Component, OnInit } from '@angular/core';
import { ParkingMapService } from '../../service/parking-map.service';
import { ReservationService, ReservationRequest } from '../../service/reservation.service';
import { ParkingLot } from '../../Model/parkingMap';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-map',
  standalone: true,
  imports: [CommonModule, MatIconModule, FormsModule],
  templateUrl: './map.component.html',
  styleUrl: './map.component.css'
})
export class MapComponent implements OnInit {
  selectedDate: string = '';
  currentDate: string = '';
  parkingData: Record<string, ParkingLot[]> = {};

  showReservationPopup = false;
  selectedSpot: { row: string; column: number } | null = null;
  reservationData = {
    userId: '',
    beginningDate: '',
    endDate: ''
  };

  constructor(
    private parkingMapService: ParkingMapService,
    private reservationService: ReservationService
  ) {}

  ngOnInit(): void {
    const today = new Date();
    this.selectedDate = today.toISOString().split('T')[0];
    const formattedDate = this.formatDateWithHours(this.selectedDate, true);
    this.loadAvailablePlacesForDate(formattedDate);
  }

  private loadAvailablePlacesForDate(date: string) {
    console.log('Chargement des places pour la date :', date);
    this.currentDate = date;

    this.parkingMapService.getAvailablePlacesByDate(date).subscribe(
      (data: ParkingLot[]) => {
        console.log('Places disponibles reçues:', data);
        this.groupByRow(data);
      },
      (error) => {
        console.error('Erreur lors de la récupération des places disponibles:', error);
        alert('Une erreur est survenue lors de la récupération des places disponibles');
      }
    );
  }

  private groupByRow(data: ParkingLot[]) {
    this.parkingData = data.reduce((acc, spot) => {
      if (!acc[spot.row]) {
        acc[spot.row] = [];
      }
      acc[spot.row].push(spot);
      return acc;
    }, {} as Record<string, ParkingLot[]>);
  }

  onSpotClick(row: string, column: number) {
    if (!this.selectedDate) {
      alert('Veuillez d\'abord sélectionner une date');
      return;
    }

    this.selectedSpot = { row, column };
    this.reservationData = {
      userId: '',
      beginningDate: this.selectedDate,
      endDate: this.selectedDate
    };
    this.showReservationPopup = true;
  }

  closeReservationPopup() {
    this.showReservationPopup = false;
    this.selectedSpot = null;
    this.reservationData = {
      userId: '',
      beginningDate: '',
      endDate: ''
    };
  }

  private formatDateWithHours(date: string, isNoon: boolean): string {
    const hours = isNoon ? '12:00:00.000' : '23:59:59.999';
    return `${date}T${hours}Z`;
  }

  submitReservation() {
    if (!this.selectedSpot || !this.reservationData.userId || !this.reservationData.beginningDate || !this.reservationData.endDate) {
      alert('Veuillez remplir tous les champs');
      return;
    }

    const reservation: ReservationRequest = {
      userId: this.reservationData.userId,
      row: this.selectedSpot.row,
      column: this.selectedSpot.column,
      beginningOfReservation: this.reservationData.beginningDate+ 'T00:00:00.000Z',
      endOfReservation: this.reservationData.endDate+ 'T23:59:59.999Z'
    };

    console.log('Envoi de la réservation:', reservation);

    this.reservationService.makeReservation(reservation).subscribe(
      (response) => {
        console.log('Réservation réussie:', response);
        alert('Réservation effectuée avec succès');
        this.closeReservationPopup();

        // ✅ Toujours recharger les places avec la date sélectionnée à 12h00
        const formattedDate = this.formatDateWithHours(this.selectedDate, true);
        this.loadAvailablePlacesForDate(formattedDate);
      },
      (error) => {
        console.error('Erreur lors de la réservation:', error);
        alert('Une erreur est survenue lors de la réservation');
      }
    );
  }

  onDateChange() {
    console.log('Date sélectionnée (format YYYY-MM-DD):', this.selectedDate);
  }

  validateDate() {
    if (!this.selectedDate) {
      alert('Veuillez sélectionner une date');
      return;
    }

    const formattedDate = this.formatDateWithHours(this.selectedDate, true);
    console.log('Validation de la date à 12h00 (format pour l\'API) :', formattedDate);

    this.loadAvailablePlacesForDate(formattedDate);
  }
}
