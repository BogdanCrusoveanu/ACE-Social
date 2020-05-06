import { Specialization } from './../_models/specialization';
import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: "root",
})
export class SpecializationService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getSpecializations() {
    return this.http.get<Specialization[]>(this.baseUrl + "specializations/get");
  }

  addSpecialization(specialization: Specialization) {
    return this.http.post(this.baseUrl + "specializations/add", specialization);
  }

  UpdateSpecialization(specialization: Specialization) {
    return this.http.post(this.baseUrl + "specializations/update", specialization);
  }

  deleteSpecialization(specialization: Specialization) {
    return this.http.post(this.baseUrl + "specializations/delete", specialization);
  }
}
