import { Specialization } from './../../_models/specialization';
import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute } from '@angular/router';
import { SpecializationModalComponent } from '../specialization-modal/specialization-modal.component';

@Component({
  selector: 'app-specialization-management',
  templateUrl: './specialization-management.component.html',
  styleUrls: ['./specialization-management.component.css']
})
export class SpecializationManagementComponent implements OnInit {
  specializations: Specialization[];
  filteredSpecializations: Specialization[];
  searchText;
  bsModalRef: BsModalRef;

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.getSpecializations();
  }

  getSpecializations() {
    this.route.data.subscribe((data) => {
      this.specializations = data["specializations"];
    });
  }

  insertSpecialization() {
    let insert = true;
    const initialState = {
      insert,
    };
    this.bsModalRef = this.modalService.show(SpecializationModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendClasses.subscribe((values) => {
      this.specializations = values;
    });
  }

  updateSpecialization(specializationForUpdate: Specialization) {
    let insert = false;
    const initialState = {
      insert,
      specializationForUpdate,
    };
    this.bsModalRef = this.modalService.show(SpecializationModalComponent, {
      initialState,
    });
  }
}
