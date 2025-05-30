import {Component, OnInit} from '@angular/core';
import {ParkingMapService} from '../../service/parking-map.service';
import {ParkingLot} from '../../Model/parkingMap';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-map',
  imports: [CommonModule],
  templateUrl: './map.component.html',
  styleUrl: './map.component.css'
})
export class MapComponent  implements OnInit{
  parkingData: Record<string, ParkingLot[]> = {};

  constructor(private parkingMapService: ParkingMapService) { }

  ngOnInit(): void {
    this.parkingMapService.getParkingMap().subscribe(
      (data:ParkingLot[]) => {
        console.log('Parking map data:', data);
        this.groupByRow(data);
      },
      (error) => {
        console.error('Error fetching parking map:', error);
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





}
