import { LaboratoryModalComponent } from './../laboratory-modal/laboratory-modal.component';
import { Laboratory } from './../../_models/laboratory';
import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-laboratory-management',
  templateUrl: './laboratory-management.component.html',
  styleUrls: ['./laboratory-management.component.css']
})
export class LaboratoryManagementComponent implements OnInit {

  laboratories: Laboratory[];
  filteredLaboratories: Laboratory[];
  searchText;
  bsModalRef: BsModalRef;

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.getLaboratories();
  }

  getLaboratories() {
    this.route.data.subscribe((data) => {
      this.laboratories = data["laboratories"];
      console.log(this.laboratories);
    });
  }

  insertLaboratory() {
    let insert = true;
    const initialState = {
      insert,
    };
    this.bsModalRef = this.modalService.show(LaboratoryModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendClasses.subscribe((values) => {
      this.laboratories = values;
    });
  }

  updateLaboratory(laboratoryForUpdate: Laboratory) {
    let insert = false;
    const initialState = {
      insert,
      laboratoryForUpdate,
    };
    this.bsModalRef = this.modalService.show(LaboratoryModalComponent, {
      initialState,
    });
  }
}
