import { AlertifyService } from "src/app/_services/alertify.service";
import { SpecializationService } from "./../../_services/specialization.service";
import { Specialization } from "src/app/_models/specialization";
import { Component, OnInit } from "@angular/core";
import { BsModalService } from "ngx-bootstrap/modal/";
import { BsModalRef } from "ngx-bootstrap/modal";
import { ActivatedRoute } from "@angular/router";
import { SpecializationModalComponent } from "../specialization-modal/specialization-modal.component";

@Component({
  selector: "app-specialization-management",
  templateUrl: "./specialization-management.component.html",
  styleUrls: ["./specialization-management.component.css"],
})
export class SpecializationManagementComponent implements OnInit {
  specializations: Specialization[];
  filteredSpecializations: Specialization[];
  searchText;
  bsModalRef: BsModalRef;

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private specializationservice: SpecializationService,
    private alertify: AlertifyService
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
    this.bsModalRef.content.sendSpecialization.subscribe((values) => {
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

  deleteSpecialization(specialization: Specialization) {
    this.alertify.confirm(
      "Sunteti sigur ca doriti sa stergeti aceasta specializare? Toate cursurile, laboratoarele si seminarele care apartin acestei specializari vor fi sterse, impreuna cu grupele si subgrupele aferente Specializarii!",
      () => {
        this.specializationservice
          .deleteSpecialization(specialization)
          .subscribe(
            () => {
              this.alertify.success("Specializarea a fost stearsa!");
              const index: number = this.specializations.indexOf(
                specialization
              );
              this.specializations.splice(index, 1);
            },
            (error) => {
              this.alertify.error(error);
            }
          );
      }
    );
  }
}
