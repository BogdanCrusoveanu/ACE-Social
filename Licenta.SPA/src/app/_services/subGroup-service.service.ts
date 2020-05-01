import { SubGroup } from './../_models/subGroup';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SubGroupService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getSubGroups() {
    return this.http.get<SubGroup[]>(this.baseUrl + "subGroups/get");
  }

  addSubGroups(subGroup: SubGroup) {
    return this.http.post(this.baseUrl + "subGroups/add", subGroup);
  }

  updateSubGroups(subGroup: SubGroup) {
    return this.http.post(this.baseUrl + "subGroups/update", subGroup);
  }
}
