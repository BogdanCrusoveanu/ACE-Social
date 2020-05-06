import { SeminarService } from './../../_services/seminar.service';
import { Group } from './../../_models/group';
import { Seminar } from './../../_models/seminar';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Course } from 'src/app/_models/course';
import { User } from 'src/app/_models/user';
import { Class } from 'src/app/_models/class';
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-seminar-modal',
  templateUrl: './seminar-modal.component.html',
  styleUrls: ['./seminar-modal.component.css']
})
export class SeminarModalComponent implements OnInit {
  
  @Output() sendSeminar = new EventEmitter();
  seminarToAdd: Seminar;
  seminarForm: FormGroup;
  searchGroup;
  searchTeacher;
  searchClass;
  searchCourse;
  seminarForUpdate: Seminar;
  courses: Course[];
  groups: Group[];
  teachers: User[];
  classes: Class[];
  message: string;
  insert: boolean;
  groupId;
  teacherId;
  classId;
  courseId;
  refresh: Subject<any> = new Subject();
  
  constructor(
    public bsModalRef: BsModalRef,
    private fb: FormBuilder,
    private seminarService: SeminarService,
    private alertify: AlertifyService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.createCourse();
  }

  createCourse() {
    if (this.insert) {
      this.seminarForm = this.fb.group({
        name: ["", Validators.required],
        groupName: ["", Validators.required],
        teacherName: ["", Validators.required],
        className: ["", Validators.required],
        courseName: ["", Validators.required],
        startDate: [null, Validators.required],
        endDate: [null, Validators.required],
        semesterId: ["", Validators.required]
      });
    } else {
      this.seminarForm = this.fb.group({
        name: [this.seminarForUpdate.name, Validators.required],
        
        groupName: [
          this.seminarForUpdate.groupName,
          Validators.required,
        ],

        teacherName: [
          this.seminarForUpdate.teacherName,
          Validators.required,
        ],

        className: [
          this.seminarForUpdate.className,
          Validators.required,
        ],

        courseName: [
          this.seminarForUpdate.courseName,
          Validators.required,
        ],

        startDate: [this.seminarForUpdate.startDate, Validators.required],
        endDate: [this.seminarForUpdate.endDate, Validators.required],
        semesterId: [this.seminarForUpdate.semesterId, Validators.required]
      });
      this.searchGroup = this.seminarForUpdate.groupName;
      this.searchTeacher = this.seminarForUpdate.teacherName;
      this.searchClass = this.seminarForUpdate.className;
      this.searchCourse = this.seminarForUpdate.courseName;
    }
  }

  addSeminar() {
    this.seminarToAdd = Object.assign({}, this.seminarForm.value);
    this.seminarToAdd.groupId = this.groupId;
    this.seminarToAdd.teacherId = this.teacherId;
    this.seminarToAdd.type = "Seminar";
    this.seminarToAdd.courseId = this.courseId;
    this.seminarToAdd.classId = this.classId;
    this.seminarService.addSeminar(this.seminarToAdd).subscribe(
      () => {
        this.alertify.success("Seminarul a fost introdus cu succes!");
        this.loadSeminars();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  updateSeminar() {
    this.seminarForUpdate.name = this.seminarForm.get("name").value;

    if (this.groupId != null) {
      this.seminarForUpdate.groupId = this.groupId;
      this.seminarForUpdate.groupName = this.seminarForm.get("groupName").value;
    }

    if (this.teacherId != null) {
      this.seminarForUpdate.teacherId = this.teacherId;
      this.seminarForUpdate.teacherName = this.seminarForm.get("teacherName").value;
    }

    if (this.classId != null) {
      this.seminarForUpdate.classId = this.classId;
      this.seminarForUpdate.className = this.seminarForm.get("className").value;
    }

    if (this.courseId != null) {
      this.seminarForUpdate.courseId = this.courseId;
      this.seminarForUpdate.courseName = this.seminarForm.get("courseName").value;
    }

    this.seminarForUpdate.startDate = this.seminarForm.get('startDate').value
    this.seminarForUpdate.endDate = this.seminarForm.get('endDate').value
    this.seminarForUpdate.semesterId = +this.seminarForm.get('semesterId').value

    this.seminarService.updateSeminar(this.seminarForUpdate).subscribe(
      () => {
        this.alertify.success("Seminarul a fost modificat cu succes!");
        this.loadSeminars();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  loadSeminars() {
    this.seminarService.getSeminars(this.authService.decodedToken.nameid).subscribe((data) => {
      this.sendSeminar.emit(data);
    });
  }

  onSelectionChangedGroup(event: any) {
    this.groupId = event.option.id;
  }

  onSelectionChangedTeacher(event: any) {
    this.teacherId = event.option.id;
  }

  onSelectionChangedClass(event: any) {
    this.classId = event.option.id;
  }

  onSelectionChangedCourse(event: any) {
    this.courseId = event.option.id;
  }

  getTeacherFullName(firstName: string, lastNmae: string) {
    return firstName + " " + lastNmae;
  }
}
