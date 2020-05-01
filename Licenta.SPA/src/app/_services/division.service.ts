import { SubGroup } from './../_models/subGroup';
import { Group } from './../_models/group';
import { Specialization } from './../_models/specialization';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})

export class DivisionService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getSpecializations() : Observable<Specialization> {
    return this.http.get<Specialization>(this.baseUrl + 'divisions/specializations');
  }

  getGroups() : Observable<Group> {
    return this.http.get<Group>(this.baseUrl + 'divisions/groups');
  }

  getSubGroups() : Observable<SubGroup> {
    return this.http.get<SubGroup>(this.baseUrl + 'divisions/subgroups');
  }

}
