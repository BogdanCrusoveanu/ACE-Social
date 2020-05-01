import { CourseModalComponent } from './../course-modal/course-modal.component';
import { Course } from './../../_models/course';
import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-course-management',
  templateUrl: './course-management.component.html',
  styleUrls: ['./course-management.component.css']
})
export class CourseManagementComponent implements OnInit {

  courses: Course[];
  filteredCourses: Course[];
  searchText;
  bsModalRef: BsModalRef;

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.getCourses();
  }

  getCourses() {
    this.route.data.subscribe((data) => {
      this.courses = data["courses"];
      console.log(this.courses);
    });
  }

  insertCourses() {
    let insert = true;
    const initialState = {
      insert,
    };
    this.bsModalRef = this.modalService.show(CourseModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendClasses.subscribe((values) => {
      this.courses = values;
    });
  }

  updateCourse(courseForUpdate: Course) {
    let insert = false;
    const initialState = {
      insert,
      courseForUpdate,
    };
    this.bsModalRef = this.modalService.show(CourseModalComponent, {
      initialState,
    });
  }

}
