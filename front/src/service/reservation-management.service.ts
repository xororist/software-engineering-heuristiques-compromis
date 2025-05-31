import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Reservation, CancelReservationRequest } from '../app/Model/reservation';
import {environment} from '../environment/environment';


@Injectable({
  providedIn: 'root'
})
export class ReservationManagementService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getOngoingReservations(userId: string): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(`${this.apiUrl}/ongoing-reservations/${userId}`);
  }

  cancelReservation(request: CancelReservationRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/cancel-reservation`, request);
  }
}
