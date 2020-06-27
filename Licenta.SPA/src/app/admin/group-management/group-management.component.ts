import { GroupService } from './../../_services/group-service.service';
import { AlertifyService } from './../../_services/alertify.service';
import { GroupsModalComponent } from './../groups-modal/groups-modal.component';
import { Specialization } from './../../_models/specialization';
import { Group } from './../../_models/group';
import { Component, OnInit } from '@angular/core';
import { BsModalService } from "ngx-bootstrap/modal/";
import { BsModalRef } from "ngx-bootstrap/modal";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-group-management',
  templateUrl: './group-management.component.html',
  styleUrls: ['./group-management.component.css']
})
export class GroupManagementComponent implements OnInit {

  specializations: Specialization[];
  groups: Group[];
  filteredGroups: Group[];
  searchText;
  bsModalRef: BsModalRef;

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private alertify: AlertifyService,
    private groupService: GroupService
  ) {}

  ngOnInit() {
    this.getGroups();
    console.log(this.groups);
    console.log(this.specializations);
  }

  getGroups() {
    this.route.data.subscribe((data) => {
      this.specializations = data["specializations"];
      this.groups = data['groups'];
    });
  }

  insertGroups(specializations) {
    let insert = true;
    const initialState = {
      insert,
      specializations
    };
    this.bsModalRef = this.modalService.show(GroupsModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendGroup.subscribe((values) => {
      this.groups = values;
    });
  }

  updateGroup(groupForUpdate: Group, specializations: Specialization[]) {
    let insert = false;
    const initialState = {
      insert,
      groupForUpdate,
      specializations,
    };
    this.bsModalRef = this.modalService.show(GroupsModalComponent, {
      initialState,
    });
  }

  deleteGroup(group: Group) {
    this.alertify.confirm(
      "Sunteti sigur ca doriti sa stergeti aceasta grupa? Toate sub-grupele si seminarele aferente acestei grupe vor fi sterse!",
      () => {
        this.groupService.deleteGroup(group).subscribe(
          () => {
            this.alertify.success("Grupa a fost stearsa!");
            const index: number = this.groups.indexOf(group);
            this.groups.splice(index, 1);
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      }
    );
  }
}
