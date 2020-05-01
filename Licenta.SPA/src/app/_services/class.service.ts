import { Class } from "./../_models/class";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment.prod";

@Injectable({
  providedIn: "root",
})
export class ClassService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getClasses() {
    return this.http.get<Class[]>(this.baseUrl + "admin/getClasses");
  }

  addClass(insertedClass: Class) {
    return this.http.post(this.baseUrl + 'admin/addClass', insertedClass);
 }

  updateClass(updatedClass: Class) {
  return this.http.post(this.baseUrl + 'admin/updateClass', updatedClass);
}
}
