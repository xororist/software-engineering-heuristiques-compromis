import {Component, OnInit} from '@angular/core';
import {ParkingMapService} from '../../service/parking-map.service';
import {ParkingLot} from '../../Model/parkingMap';
import {CommonModule} from '@angular/common';
import {MatIconModule} from '@angular/material/icon';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-map',
  standalone: true,
  imports: [CommonModule, MatIconModule, FormsModule],
  templateUrl: './map.component.html',
  styleUrl: './map.component.css'
})
export class MapComponent implements OnInit {
  selectedDate: string = '';
  parkingData: Record<string, ParkingLot[]> = {};

  constructor(private parkingMapService: ParkingMapService) {}

  ngOnInit(): void {
    // Initialiser avec la date du jour
    const today = new Date();
    // Format YYYY-MM-DD pour l'input date
    this.selectedDate = today.toISOString().split('T')[0];
    
    // Charger les places disponibles pour aujourd'hui
    this.loadAvailablePlacesForDate(this.selectedDate);
  }

  private loadAvailablePlacesForDate(date: string) {
    console.log('Chargement des places pour la date:', date);
    this.parkingMapService.getAvailablePlacesByDate(date).subscribe(
      (data: ParkingLot[]) => {
        console.log('Places disponibles reçues:', data);
        this.groupByRow(data);
      },
      (error) => {
        console.error('Error fetching available places for date:', error);
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

  onDateChange() {
    console.log('Date sélectionnée (format YYYY-MM-DD):', this.selectedDate);
  }

  validateDate() {
    if (!this.selectedDate) {
      alert('Veuillez sélectionner une date');
      return;
    }

    console.log('Validation de la date (format YYYY-MM-DD):', this.selectedDate);
    this.loadAvailablePlacesForDate(this.selectedDate);
  }
}
