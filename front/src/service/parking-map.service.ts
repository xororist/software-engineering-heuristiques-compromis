import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from '../environment/environment';
import {ParkingLot} from '../Model/parkingMap';

@Injectable({
  providedIn: 'root'
})
export class ParkingMapService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAvailablePlacesByDate(date: string): Observable<ParkingLot[]> {
    return this.http.get<ParkingLot[]>(`${this.apiUrl}/available-places?date=${date}`);
  }
}
