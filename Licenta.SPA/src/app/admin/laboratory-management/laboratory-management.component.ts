import { SubGroup } from './../../_models/subGroup';
import { LaboratoryService } from './../../_services/laboratory.service';
import { AlertifyService } from './../../_services/alertify.service';
import { LaboratoryModalComponent } from './../laboratory-modal/laboratory-modal.component';
import { Laboratory } from './../../_models/laboratory';
import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute } from '@angular/router';
import { Course } from 'src/app/_models/course';
import { User } from 'src/app/_models/user';
import { Class } from 'src/app/_models/class';

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
  courses: Course[];
  teachers: User[];
  subGroups: SubGroup[];
  classes: Class[];


  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private alertify: AlertifyService,
    private laboratoryService: LaboratoryService
  ) {}

  ngOnInit() {
    this.getLaboratories();
  }

  getLaboratories() {
    this.route.data.subscribe((data) => {
      this.laboratories = data["laboratories"];
      this.courses = data["courses"];
      this.classes = data["classes"];
      this.subGroups = data["subGroups"];
      this.teachers = data["teachers"];
    });
  }

  insertLaboratory(classes: Class[], subGroups: SubGroup[], teachers: User[], courses: Course[]) {
    let insert = true;
    const initialState = {
      insert,
      classes,
      subGroups,
      teachers,
      courses
    };
    this.bsModalRef = this.modalService.show(LaboratoryModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendLaboratory.subscribe((values) => {
      this.laboratories = values;
    });
  }

  updateLaboratory(laboratoryForUpdate: Laboratory,classes: Class[], subGroups: SubGroup[], teachers: User[], courses: Course[]) {
    let insert = false;
    const initialState = {
      insert,
      laboratoryForUpdate,
      classes,
      subGroups,
      teachers,
      courses
    };
    this.bsModalRef = this.modalService.show(LaboratoryModalComponent, {
      initialState,
    });
  }

  deleteLaboratory(laboratory: Laboratory) {
    this.alertify.confirm(
      "Sunteti sigur ca doriti sa stergeti acest laborator?",
      () => {
        this.laboratoryService.deleteLaboratory(laboratory).subscribe(
          () => {
            this.alertify.success("Laboratorul a fost sters!");
            const index: number = this.laboratories.indexOf(laboratory);
            this.laboratories.splice(index, 1);
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      }
    );
  }

}
