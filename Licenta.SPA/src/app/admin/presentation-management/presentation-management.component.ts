import { PresentationService } from "./../../_services/presentation.service";
import { Component, OnInit } from "@angular/core";
import { Presentation } from "src/app/_models/presentation";
import { BsModalRef, BsModalService } from "ngx-bootstrap";
import { ActivatedRoute } from "@angular/router";
import { AlertifyService } from "src/app/_services/alertify.service";
import { PresentationModalComponent } from "../presentation-modal/presentation-modal.component";
import { Class } from 'src/app/_models/class';

@Component({
  selector: "app-presentation-management",
  templateUrl: "./presentation-management.component.html",
  styleUrls: ["./presentation-management.component.css"],
})
export class PresentationManagementComponent implements OnInit {
  presentations: Presentation[];
  filteredPresentations: Presentation[];
  classes: Class[];
  searchText;
  bsModalRef: BsModalRef;

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private presentationService: PresentationService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.getPresentations();
  }

  getPresentations() {
    this.route.data.subscribe((data) => {
      this.presentations = data["presentations"];
      this.classes = data["classes"];
    });
  }

  insertPresentation(classes: Class[]) {
    let insert = true;
    const initialState = {
      insert,
      classes
    };
    this.bsModalRef = this.modalService.show(PresentationModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendPresentation.subscribe((values) => {
      this.presentations = values;
    });
  }

  updateSpecialization(presentationForUpdate: Presentation, classes: Class[]) {
    let insert = false;
    const initialState = {
      insert,
      presentationForUpdate,
      classes
    };
    this.bsModalRef = this.modalService.show(PresentationModalComponent, {
      initialState,
    });
  }

  deleteSpecialization(presentation: Presentation) {
    this.alertify.confirm(
      "Sunteti sigur ca doriti sa stergeti aceasta prezentare?",
      () => {
        this.presentationService.deletePresentation(presentation).subscribe(
          () => {
            this.alertify.success("Prezentarea a fost stearsa!");
            const index: number = this.presentations.indexOf(presentation);
            this.presentations.splice(index, 1);
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      }
    );
  }
}
