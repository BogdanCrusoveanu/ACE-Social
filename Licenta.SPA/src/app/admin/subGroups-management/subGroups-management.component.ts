import { Component, OnInit } from '@angular/core';
import { SubGroup } from 'src/app/_models/subGroup';
import { Group } from 'src/app/_models/group';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute } from '@angular/router';
import { SubGroupsModalComponent } from '../subGroups-modal/subGroups-modal.component';

@Component({
  selector: 'app-subGroups-management',
  templateUrl: './subGroups-management.component.html',
  styleUrls: ['./subGroups-management.component.css']
})
export class SubGroupsManagementComponent implements OnInit {

  subGroups: SubGroup[];
  groups: Group[];
  filteredSubGroups: SubGroup[];
  searchText;
  bsModalRef: BsModalRef;

  constructor(
    private route: ActivatedRoute,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.getSubGroups();
    console.log(this.groups);
    console.log(this.subGroups);
  }

  getSubGroups() {
    this.route.data.subscribe((data) => {
      this.subGroups = data["subGroups"];
      this.groups = data['groups'];
    });
  }

  insertSubGroups() {
    let insert = true;
    const initialState = {
      insert,
    };
    this.bsModalRef = this.modalService.show(SubGroupsModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendClasses.subscribe((values) => {
      this.groups = values;
    });
  }

  updateSubGroup(subgroupForUpdate: SubGroup, groups: Group[]) {
    let insert = false;
    const initialState = {
      insert,
      subgroupForUpdate,
      groups,
    };
    this.bsModalRef = this.modalService.show(SubGroupsModalComponent, {
      initialState,
    });
  }
}
