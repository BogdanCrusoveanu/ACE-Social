import { Specialization } from './../../_models/specialization';
import { CourseService } from './../../_services/course.service';
import { AlertifyService } from './../../_services/alertify.service';
import { CourseModalComponent } from './../course-modal/course-modal.component';
import { Course } from './../../_models/course';
import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService, BsDatepickerConfig } from 'ngx-bootstrap';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { Class } from 'src/app/_models/class';

@Component({
  selector: 'app-course-management',
  templateUrl: './course-management.component.html',
  styleUrls: ['./course-management.component.css']
})
export class CourseManagementComponent implements OnInit {

  courses: Course[];
  teachers: User[];
  specializations: Specialization[];
  classes: Class[];
  filteredCourses: Course[];
  searchText;
  bsModalRef: BsModalRef;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private alertify: AlertifyService,
    private courseService: CourseService
  ) {}

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-red'
    },
    this.getCourses();
  }

  getCourses() {
    this.route.data.subscribe((data) => {
      this.courses = data["courses"];
      this.classes = data["classes"];
      this.specializations = data["specializations"];
      this.teachers = data["teachers"];
    });
  }

  insertCourses(classes: Class[], specializations: Specialization[], teachers: User[]) {
    let insert = true;
    const initialState = {
      insert,
      classes,
      specializations,
      teachers
    };
    this.bsModalRef = this.modalService.show(CourseModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendCourse.subscribe((values) => {
      this.courses = values;
    });
  }

  updateCourse(courseForUpdate: Course, classes: Class[], specializations: Specialization[], teachers: User[]) {
    let insert = false;
    const initialState = {
      insert,
      classes,
      specializations,
      teachers,
      courseForUpdate
    };
    this.bsModalRef = this.modalService.show(CourseModalComponent, {
      initialState,
    });
  }

  deleteCourse(course: Course) {
    this.alertify.confirm(
      "Sunteti sigur ca doriti sa stergeti acest curs?",
      () => {
        this.courseService.deleteCourse(course).subscribe(
          () => {
            this.alertify.success("Cursul a fost sters!");
            const index: number = this.courses.indexOf(course);
            this.courses.splice(index, 1);
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      }
    );
  }
}
