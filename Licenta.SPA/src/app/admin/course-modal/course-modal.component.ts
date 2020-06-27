import { AuthService } from './../../_services/auth.service';
import { CourseService } from './../../_services/course.service';
import { Class } from './../../_models/class';
import { User } from './../../_models/user';
import { Course } from './../../_models/course';
import { Component, OnInit, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Specialization } from 'src/app/_models/specialization';
import { BsModalRef } from 'ngx-bootstrap/modal/';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-course-modal',
  templateUrl: './course-modal.component.html',
  styleUrls: ['./course-modal.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class CourseModalComponent implements OnInit {

  @Output() sendCourse = new EventEmitter();
  courseToAdd: Course;
  courseForm: FormGroup;
  courses: Course[];
  searchSpecialization;
  searchTeacher;
  searchClass;
  courseForUpdate: Course;
  specializations: Specialization[];
  teachers: User[];
  classes: Class[];
  message: string;
  insert: boolean;
  specializationId;
  teacherId;
  classId;
  refresh: Subject<any> = new Subject();
  
  constructor(
    public bsModalRef: BsModalRef,
    private fb: FormBuilder,
    private courseService: CourseService,
    private alertify: AlertifyService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.createCourse();
    console.log("Cursuri:")
    console.log(this.specializations);
    console.log("Profesori:")
    console.log(this.teachers);
    console.log("Clase:")
    console.log(this.classes);
    console.log("Course for update:")
    console.log(this.courseForUpdate)
  }

  createCourse() {
    if (this.insert) {
      this.courseForm = this.fb.group({
        name: ["", Validators.required],
        specializationName: ["", Validators.required],
        teacherName: ["", Validators.required],
        className: ["", Validators.required],
        startDate: [null, Validators.required],
        endDate: [null, Validators.required],
        semesterId: ["", Validators.required]
      });
    } else {
      this.courseForm = this.fb.group({
        name: [this.courseForUpdate.name, Validators.required],
        
        specializationName: [
          this.courseForUpdate.specializationName,
          Validators.required,
        ],

        teacherName: [
          this.courseForUpdate.teacherName,
          Validators.required,
        ],

        className: [
          this.courseForUpdate.className,
          Validators.required,
        ],

        startDate: [this.courseForUpdate.startDate, Validators.required],
        endDate: [this.courseForUpdate.endDate, Validators.required],
        semesterId: [this.courseForUpdate.semesterId, Validators.required]
      });
      this.searchSpecialization = this.courseForUpdate.specializationName;
      this.searchTeacher = this.courseForUpdate.teacherName;
      this.searchClass = this.courseForUpdate.className;
    }
  }

  addCourse() {
    this.courseToAdd = Object.assign({}, this.courseForm.value);
    this.courseToAdd.specializationId = this.specializationId;
    this.courseToAdd.teacherId = this.teacherId;
    this.courseToAdd.type = "Curs";
    this.courseToAdd.classId = this.classId;
    this.courseService.addCourse(this.courseToAdd).subscribe(
      () => {
        this.alertify.success("Cursul a fost introdus cu succes!");
        this.loadGroups();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  updateCourse() {
    this.courseForUpdate.name = this.courseForm.get("name").value;

    if (this.specializationId != null) {
      this.courseForUpdate.specializationId = this.specializationId;
      this.courseForUpdate.specializationName = this.courseForm.get("specializationName").value;
    }

    if (this.teacherId != null) {
      this.courseForUpdate.teacherId = this.teacherId;
      this.courseForUpdate.teacherName = this.courseForm.get("teacherName").value;
    }

    if (this.classId != null) {
      this.courseForUpdate.classId = this.classId;
      this.courseForUpdate.className = this.courseForm.get("className").value;
    }

    this.courseForUpdate.startDate = this.courseForm.get('startDate').value
    this.courseForUpdate.endDate = this.courseForm.get('endDate').value
    this.courseForUpdate.semesterId = +this.courseForm.get('semesterId').value

    this.courseService.updateCourse(this.courseForUpdate).subscribe(
      () => {
        this.alertify.success("Cursul a fost modificat cu succes!");
        this.loadGroups();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  loadGroups() {
    this.courseService.gerCourses(this.authService.decodedToken.nameid).subscribe((data) => {
      this.sendCourse.emit(data);
      this.specializations = data;
    });
  }

  onSelectionChangedSpecialization(event: any) {
    this.specializationId = event.option.id;
  }

  onSelectionChangedTeacher(event: any) {
    this.teacherId = event.option.id;
  }

  onSelectionChangedClass(event: any) {
    this.classId = event.option.id;
  }

  getTeacherFullName(firstName: string, lastNmae: string) {
    return firstName + " " + lastNmae;
  }

}
