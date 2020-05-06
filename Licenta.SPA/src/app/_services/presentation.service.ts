import { Presentation } from './../_models/presentation';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PresentationService {
  
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getPresentation(id: number) {
    return this.http.get<Presentation[]>(this.baseUrl + "companyPresentations/get/" + id);
  }

  addPresentation(presentation: Presentation) {
    return this.http.post(this.baseUrl + "companyPresentations/add", presentation);
  }

  updatePresentation(presentation: Presentation) {
    return this.http.post(this.baseUrl + "companyPresentations/update", presentation);
  }

  deletePresentation(presentation: Presentation) {
    return this.http.post(this.baseUrl + "companyPresentations/delete", presentation);
  }

}
