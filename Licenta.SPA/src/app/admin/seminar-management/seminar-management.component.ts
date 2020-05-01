import { SeminarModalComponent } from './../seminar-modal/seminar-modal.component';
import { Seminar } from './../../_models/seminar';
import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute } from '@angular/router';

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

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.getSeminars();
  }

  getSeminars() {
    this.route.data.subscribe((data) => {
      this.seminars = data["seminars"];
      console.log(this.seminars);
    });
  }

  InsertSeminar() {
    let insert = true;
    const initialState = {
      insert,
    };
    this.bsModalRef = this.modalService.show(SeminarModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendClasses.subscribe((values) => {
      this.seminars = values;
    });
  }

  updateCourse(seminarForUpdate: Seminar) {
    let insert = false;
    const initialState = {
      insert,
      seminarForUpdate,
    };
    this.bsModalRef = this.modalService.show(SeminarModalComponent, {
      initialState,
    });
  }


}
