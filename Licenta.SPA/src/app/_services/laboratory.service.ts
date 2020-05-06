import { Laboratory } from './../_models/laboratory';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LaboratoryService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getLaboratories(id: number) {
    return this.http.get<Laboratory[]>(this.baseUrl + "laboratories/get/" + id);
  }

  addLaboratory(laboratory: Laboratory) {
    return this.http.post(this.baseUrl + "laboratories/add", laboratory);
  }

  updateLaboratory(laboratory: Laboratory) {
    return this.http.post(this.baseUrl + "laboratories/update", laboratory);
  }

  deleteLaboratory(laboratory: Laboratory) {
    return this.http.post(this.baseUrl + "laboratories/delete", laboratory);
  }

}
