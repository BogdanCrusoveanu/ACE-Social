import { Course } from './../_models/course';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  gerCourses(id: number) {
    return this.http.get<Course[]>(this.baseUrl + "courses/get/" + id);
  }

  addCourse(course: Course) {
    return this.http.post(this.baseUrl + "courses/add", course);
  }

  updateCourse(course: Course) {
    return this.http.post(this.baseUrl + "courses/update", course);
  }

  deleteCourse(course: Course) {
    return this.http.post(this.baseUrl + "courses/delete", course);
  }
}
