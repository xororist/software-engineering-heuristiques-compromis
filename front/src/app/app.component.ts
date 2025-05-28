import {Component, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HttpClient} from '@angular/common/http';
import {TestService} from '../service/test.service';
import {Test} from '../model/test';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  standalone: true,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'SoftWare Heuristics';


  protected testMessage: Test  | undefined;
  constructor(private http: HttpClient, private testService: TestService) {

  }

  ngOnInit() {
    this.testService.getTest().subscribe({
      next: (test) => {
        this.testMessage = test;
      },
      error: (error) => {

      },})
  }

}
