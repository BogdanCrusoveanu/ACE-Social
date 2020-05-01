import { Activity } from './../_models/activity';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {
  baseUrl = environment.apiUrl

constructor(private http: HttpClient) { }

getActivitiesForUser(id) {
  return this.http.get<Activity[]>(this.baseUrl + 'activities/' + id);
}
}
