import { GroupsModalComponent } from './../groups-modal/groups-modal.component';
import { Specialization } from './../../_models/specialization';
import { Group } from './../../_models/group';
import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
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
    private modalService: BsModalService
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

  insertGroups() {
    let insert = true;
    const initialState = {
      insert,
    };
    this.bsModalRef = this.modalService.show(GroupsModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendClasses.subscribe((values) => {
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

}
