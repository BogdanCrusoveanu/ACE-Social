import { ClassService } from "./../../_services/class.service";
import { ClassModalComponent } from "./../class-modal/class-modal.component";
import { Class } from "./../../_models/class";
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
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.getClasses();
  }

  getClasses() {
    this.route.data.subscribe((data) => {
      this.classes = data["classes"];
      //this.pagination = data['users'].pagination;
      // console.log(this.pagination);
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

    // this.bsModalRef.content.updateSelectedRoles.subscribe((values) => {
    //   const rolesToUpdate = {
    //     roleNames: [...values.filter(el => el.checked === true).map(el => el.name)]
    //   };
    //   if(rolesToUpdate) {
    //     this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(() => {
    //       user.roles = [...rolesToUpdate.roleNames]
    //     }, error => {
    //       this.alertifyService.error(error);
    //     });
    //   }
    // });
  }
}
