import { SubGroupService } from './../../_services/subGroup-service.service';
import { AlertifyService } from './../../_services/alertify.service';
import { Component, OnInit } from '@angular/core';
import { SubGroup } from 'src/app/_models/subGroup';
import { Group } from 'src/app/_models/group';
import { BsModalService } from "ngx-bootstrap/modal/";
import { BsModalRef } from "ngx-bootstrap/modal";
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
    private modalService: BsModalService,
    private alertify: AlertifyService,
    private subgroupService: SubGroupService
  ) {}

  ngOnInit() {
    this.getSubGroups();
  }

  getSubGroups() {
    this.route.data.subscribe((data) => {
      this.subGroups = data["subGroups"];
      this.groups = data['groups'];
    });
  }

  insertSubGroups(groups: Group[]) {
    let insert = true;
    const initialState = {
      insert,
      groups
    };
    this.bsModalRef = this.modalService.show(SubGroupsModalComponent, {
      initialState,
    });
    this.bsModalRef.content.sendSubGroup.subscribe((values) => {
      this.subGroups = values;
    });
  }

  updateSubGroup(subGroupForUpdate: SubGroup, groups: Group[]) {
    let insert = false;
    const initialState = {
      insert,
      subGroupForUpdate,
      groups,
    };
    this.bsModalRef = this.modalService.show(SubGroupsModalComponent, {
      initialState,
    });
  }

  deleteSubGroup(subGroup: SubGroup) {
    this.alertify.confirm(
      "Sunteti sigur ca doriti sa stergeti aceasta Sub-grupa? Toate laboratoarele aferente acestei subgrupe vor fi sterse!",
      () => {
        this.subgroupService.deteleSubGroup(subGroup).subscribe(
          () => {
            this.alertify.success("Subgrupa a fost stearsa!");
            const index: number = this.subGroups.indexOf(subGroup);
            this.subGroups.splice(index, 1);
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      }
    );
  }
}
