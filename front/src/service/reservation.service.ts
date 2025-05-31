import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';

export interface ReservationRequest {
  userId: string;
  row: string;
  column: number;
  beginningOfReservation: string;
  endOfReservation: string;
}

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private API_URL = environment.apiUrl;

  constructor(private http: HttpClient) {}

  makeReservation(reservation: ReservationRequest): Observable<any> {
    return this.http.post(`${this.API_URL}/make-reservation`, reservation);
  }
} 