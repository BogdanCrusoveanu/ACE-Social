import { LaboratoryService } from './../../_services/laboratory.service';
import { SubGroup } from './../../_models/subGroup';
import { Laboratory } from './../../_models/laboratory';
import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Course } from 'src/app/_models/course';
import { User } from 'src/app/_models/user';
import { Class } from 'src/app/_models/class';
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-laboratory-modal',
  templateUrl: './laboratory-modal.component.html',
  styleUrls: ['./laboratory-modal.component.css']
})
export class LaboratoryModalComponent implements OnInit {

  @Output() sendLaboratory = new EventEmitter();
  laboratoryToAdd: Laboratory;
  laboratoryForm: FormGroup;
  searchSubGroup;
  searchTeacher;
  searchClass;
  searchCourse;
  laboratoryForUpdate: Laboratory;
  courses: Course[];
  subGroups: SubGroup[];
  teachers: User[];
  classes: Class[];
  message: string;
  insert: boolean;
  subGroupId;
  teacherId;
  classId;
  courseId;
  refresh: Subject<any> = new Subject();
  
  constructor(
    public bsModalRef: BsModalRef,
    private fb: FormBuilder,
    private laboratoryService: LaboratoryService,
    private alertify: AlertifyService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.createLaboratoryForm();
  }

  createLaboratoryForm() {
    if (this.insert) {
      this.laboratoryForm = this.fb.group({
        name: ["", Validators.required],
        subGroupName: ["", Validators.required],
        teacherName: ["", Validators.required],
        className: ["", Validators.required],
        courseName: ["", Validators.required],
        startDate: [null, Validators.required],
        endDate: [null, Validators.required],
        semesterId: ["", Validators.required]
      });
    } else {
      this.laboratoryForm = this.fb.group({
        name: [this.laboratoryForUpdate.name, Validators.required],
        
        subGroupName: [
          this.laboratoryForUpdate.subGroupName,
          Validators.required,
        ],

        teacherName: [
          this.laboratoryForUpdate.teacherName,
          Validators.required,
        ],

        className: [
          this.laboratoryForUpdate.className,
          Validators.required,
        ],

        courseName: [
          this.laboratoryForUpdate.courseName,
          Validators.required,
        ],

        startDate: [this.laboratoryForUpdate.startDate, Validators.required],
        endDate: [this.laboratoryForUpdate.endDate, Validators.required],
        semesterId: [this.laboratoryForUpdate.semesterId, Validators.required]
      });
      this.searchSubGroup = this.laboratoryForUpdate.subGroupName;
      this.searchTeacher = this.laboratoryForUpdate.teacherName;
      this.searchClass = this.laboratoryForUpdate.className;
      this.searchCourse = this.laboratoryForUpdate.courseName;
    }
  }

  addLaboratory() {
    this.laboratoryToAdd = Object.assign({}, this.laboratoryForm.value);
    this.laboratoryToAdd.subGroupId = this.subGroupId;
    this.laboratoryToAdd.teacherId = this.teacherId;
    this.laboratoryToAdd.type = "Laborator";
    this.laboratoryToAdd.courseId = this.courseId;
    this.laboratoryToAdd.classId = this.classId;
    this.laboratoryService.addLaboratory(this.laboratoryToAdd).subscribe(
      () => {
        this.alertify.success("Laboratorul a fost introdus cu succes!");
        this.loadLaboratories();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  updateLaboratory() {
    this.laboratoryForUpdate.name = this.laboratoryForm.get("name").value;

    if (this.subGroupId != null) {
      this.laboratoryForUpdate.subGroupId = this.subGroupId;
      this.laboratoryForUpdate.subGroupName = this.laboratoryForm.get("subGroupName").value;
    }

    if (this.teacherId != null) {
      this.laboratoryForUpdate.teacherId = this.teacherId;
      this.laboratoryForUpdate.teacherName = this.laboratoryForm.get("teacherName").value;
    }

    if (this.classId != null) {
      this.laboratoryForUpdate.classId = this.classId;
      this.laboratoryForUpdate.className = this.laboratoryForm.get("className").value;
    }

    if (this.courseId != null) {
      this.laboratoryForUpdate.courseId = this.courseId;
      this.laboratoryForUpdate.courseName = this.laboratoryForm.get("courseName").value;
    }

    this.laboratoryForUpdate.startDate = this.laboratoryForm.get('startDate').value
    this.laboratoryForUpdate.endDate = this.laboratoryForm.get('endDate').value
    this.laboratoryForUpdate.semesterId = +this.laboratoryForm.get('semesterId').value

    this.laboratoryService.updateLaboratory(this.laboratoryForUpdate).subscribe(
      () => {
        this.alertify.success("Laboratorl a fost modificat cu succes!");
        this.loadLaboratories();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  loadLaboratories() {
    this.laboratoryService.getLaboratories(this.authService.decodedToken.nameid).subscribe((data) => {
      this.sendLaboratory.emit(data);
    });
  }

  onSelectionChangedSubGroup(event: any) {
    this.subGroupId = event.option.id;
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
