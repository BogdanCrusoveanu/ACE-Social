import { Class } from "src/app/_models/class";
import { AlertifyService } from "src/app/_services/alertify.service";
import { ClassService } from "./../../_services/class.service";
import { ClassModalComponent } from "./../class-modal/class-modal.component";
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { BsModalRef, BsModalService } from "ngx-bootstrap";

@Component({
  selector: "app-class-management",
  templateUrl: "./class-management.component.html",
  styleUrls: ["./class-management.component.css"],
})
export class ClassManagementComponent implements OnInit {
  classes: Class[];
  filteredClasses: Class[];
  searchText;
  bsModalRef: BsModalRef;
  //pagination: Pagination;

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private classService: ClassService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.getClasses();
  }

  getClasses() {
    this.route.data.subscribe((data) => {
      this.classes = data["classes"];
    });
  }

  insertClass() {
    let insert = true;
    const initialState = {
      insert,
    };
    this.bsModalRef = this.modalService.show(ClassModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendClasses.subscribe((values) => {
      this.classes = values;
    });
  }

  updateClass(classForUpdate: Class) {
    let insert = false;
    const initialState = {
      insert,
      classForUpdate,
    };
    this.bsModalRef = this.modalService.show(ClassModalComponent, {
      initialState,
    });
  }

  deleteClass(classToDelete: Class) {
    this.alertify.confirm(
      "Sunteti sigur ca doriti sa stergeti aceasta clasa? Toate cursurile care se tin in aceasta sala vor fi sterse!",
      () => {
        this.classService.deleteClass(classToDelete.id).subscribe(
          (data) => {
            console.log("Clasa a fost adaugata cu succes!");
            const index: number = this.classes.indexOf(classToDelete);
            if (index != -1) this.classes.splice(index, 1);
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      }
    );
  }
}
