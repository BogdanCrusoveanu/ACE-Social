import { Course } from './../../_models/course';
import { Group } from './../../_models/group';
import { SeminarService } from './../../_services/seminar.service';
import { AlertifyService } from './../../_services/alertify.service';
import { SeminarModalComponent } from './../seminar-modal/seminar-modal.component';
import { Seminar } from './../../_models/seminar';
import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { Class } from 'src/app/_models/class';

@Component({
  selector: 'app-seminar-management',
  templateUrl: './seminar-management.component.html',
  styleUrls: ['./seminar-management.component.css']
})
export class SeminarManagementComponent implements OnInit {

  seminars: Seminar[];
  filteredSeminars: Seminar[];
  searchText;
  bsModalRef: BsModalRef;
  courses: Course[];
  teachers: User[];
  groups: Group[];
  classes: Class[];

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private alertify: AlertifyService,
    private seminarService: SeminarService
  ) {}

  ngOnInit() {
    this.getSeminars();
  }

  getSeminars() {
    this.route.data.subscribe((data) => {
      this.seminars = data["seminars"];
      this.courses = data["courses"];
      this.classes = data["classes"];
      this.groups = data["groups"];
      this.teachers = data["teachers"];
    });
  }

  InsertSeminar(classes: Class[], groups: Group[], teachers: User[], courses: Course[]) {
    let insert = true;
    const initialState = {
      insert,
      classes,
      groups,
      teachers,
      courses
    };
    this.bsModalRef = this.modalService.show(SeminarModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendSeminar.subscribe((values) => {
      this.seminars = values;
    });
  }

  updateSeminar(seminarForUpdate: Seminar,classes: Class[], groups: Group[], teachers: User[], courses: Course[]) {
    let insert = false;
    const initialState = {
      insert,
      classes,
      groups,
      teachers,
      courses,
      seminarForUpdate
    };
    this.bsModalRef = this.modalService.show(SeminarModalComponent, {
      initialState,
    });
  }

  deleteSeminar(seminar: Seminar) {
    this.alertify.confirm(
      "Sunteti sigur ca doriti sa stergeti acest seminar?",
      () => {
        this.seminarService.deleteSeminar(seminar).subscribe(
          () => {
            this.alertify.success("Seminarul a fost sters!");
            const index: number = this.seminars.indexOf(seminar);
            this.seminars.splice(index, 1);
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      }
    );
  }
}
