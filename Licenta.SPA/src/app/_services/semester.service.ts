import { Semester } from './../_models/semester';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SemesterService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getSemesters() {
    return this.http.get<Semester[]>(this.baseUrl + "admin/getSemesters");
  }

  updateSemester(semester: Semester) {
    return this.http.post(this.baseUrl + "admin/updateSemesterDate", semester);
  }
}
