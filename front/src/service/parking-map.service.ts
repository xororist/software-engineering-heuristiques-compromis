import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../environment/environment';
import {Observable} from 'rxjs';
import {ParkingLot} from '../Model/parkingMap';

@Injectable({
  providedIn: 'root'
})
export class ParkingMapService {
  private API_URL = environment.apiUrl

  constructor(private http: HttpClient) {}

  getParkingMap(): Observable<ParkingLot[]> {
    return this.http.get<ParkingLot[]>(`${this.API_URL}/available-places`);
  }

  getAvailablePlacesByDate(date: string): Observable<ParkingLot[]> {
    const formattedDate = `${date}T00:00:00`;
    return this.http.get<ParkingLot[]>(`${this.API_URL}/available-place`, {
      params: { date: formattedDate }
    });
  }
}
