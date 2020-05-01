import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { Seminar } from '../_models/seminar';

@Injectable({
  providedIn: 'root'
})
export class SeminarService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getSeminars(id:number) {
    return this.http.get<Seminar[]>(this.baseUrl + "seminars/get/" + id);
  }

  addSeminar(seminar: Seminar) {
    return this.http.post(this.baseUrl + "seminars/add", seminar);
  }

  updateSeminar(seminar: Seminar) {
    return this.http.post(this.baseUrl + "seminars/update", seminar);
  }

}
