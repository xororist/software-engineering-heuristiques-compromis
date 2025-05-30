import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from '../../environment/environment';
import {Reservation} from '../../Model/reservation';




@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private apiUrl = environment.apiUrl + '/reservations';

  constructor(private http: HttpClient) { }


  createReservation(reservationData: Reservation): Observable<Reservation> {
    return this.http.post<Reservation>(this.apiUrl, reservationData);
  }


  getAllReservations(): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(this.apiUrl);
  }


  getReservationById(id: string): Observable<Reservation> {
    return this.http.get<Reservation>(`${this.apiUrl}/${id}`);
  }


  updateReservation(id: string, reservationData: Partial<Reservation>): Observable<Reservation> {
    return this.http.put<Reservation>(`${this.apiUrl}/${id}`, reservationData);
  }


  cancelReservation(id: string): Observable<Reservation> {
    return this.http.delete<Reservation>(`${this.apiUrl}/${id}`);
  }


  getUserReservations(userId: string): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(`${this.apiUrl}/user/${userId}`);
  }

  checkAvailability(placeId: string, date: string): Observable<{ available: boolean }> {
    return this.http.get<{ available: boolean }>(`${this.apiUrl}/availability/${placeId}?date=${date}`);
  }
}
