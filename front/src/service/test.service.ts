import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Test} from '../model/test';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  private API_URL = 'http://localhost:8080';

  constructor(private http: HttpClient) { }

  getTest(): Observable<Test> {
    return this.http.get<Test>(this.API_URL);
  }
}
