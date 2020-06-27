import { SemesterModalComponent } from './../semester-modal/semester-modal.component';
import { Component, OnInit } from '@angular/core';
import { Semester } from 'src/app/_models/semester';
import { BsModalService } from "ngx-bootstrap/modal/";
import { BsModalRef } from "ngx-bootstrap/modal";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-semester-management',
  templateUrl: './semester-management.component.html',
  styleUrls: ['./semester-management.component.css']
})
export class SemesterManagementComponent implements OnInit {

  semesters: Semester[];
  filteredSemesters: Semester[];
  searchText;
  bsModalRef: BsModalRef;

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService,
  ) {}

  ngOnInit() {
    this.getSemesters();
  }

  getSemesters() {
    this.route.data.subscribe((data) => {
      this.semesters = data["semesters"];
    });
  }

  updateSemester(semesterForUpdate: Semester) {
    const initialState = {
      semesterForUpdate,
    };
    this.bsModalRef = this.modalService.show(SemesterModalComponent, {
      initialState,
      backdrop: 'static'
    });
  }

}
