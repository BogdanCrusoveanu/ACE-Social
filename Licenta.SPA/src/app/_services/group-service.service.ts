import { Group } from './../_models/group';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getGroups() {
    return this.http.get<Group[]>(this.baseUrl + "groups/get");
  }

  addGroups(group: Group) {
    return this.http.post(this.baseUrl + "groups/add", group);
  }

  updateGroups(group: Group) {
    return this.http.post(this.baseUrl + "groups/update", group);
  }

}
