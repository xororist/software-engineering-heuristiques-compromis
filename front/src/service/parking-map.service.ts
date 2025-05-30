import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../environment/environment';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ParkingMapService {
  private API_URL = environment.apiUrl

  constructor(private http: HttpClient, ) {

  }

  getParkingMap() : Observable<any> {
    return this.http.get(`${this.API_URL}/available-places`);
  }
}
