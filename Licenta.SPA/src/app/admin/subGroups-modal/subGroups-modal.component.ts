import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { SubGroup } from 'src/app/_models/subGroup';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap';
import { SubGroupService } from 'src/app/_services/subGroup-service.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Group } from 'src/app/_models/group';

@Component({
  selector: 'app-subGroups-modal',
  templateUrl: './subGroups-modal.component.html',
  styleUrls: ['./subGroups-modal.component.css']
})
export class SubGroupsModalComponent implements OnInit {

  @Output() sendSubGroup = new EventEmitter();
  subGroupToAdd: SubGroup;
  subGroupForm: FormGroup;
  subGroups: SubGroup[];
  searchgroup;
  subGroupForUpdate: SubGroup;
  groups: Group[];
  message: string;
  insert: boolean;
  groupId;

  constructor(
    public bsModalRef: BsModalRef,
    private fb: FormBuilder,
    private subGroupService: SubGroupService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.createSubGroupForm();
  }

  createSubGroupForm() {
    if (this.insert) {
      this.subGroupForm = this.fb.group({
        name: ["", Validators.required],
        specializationName: ["", Validators.required],
      });
    } else {
      this.subGroupForm = this.fb.group({
        name: [this.subGroupForUpdate.name, Validators.required],
        specializationName: [
          this.subGroupForUpdate.groupName,
          Validators.required,
        ],
      });
      this.searchgroup = this.subGroupForUpdate.groupName;
    }
  }

  addSubGroup() {
    this.subGroupToAdd = Object.assign({}, this.subGroupForm.value);
    this.subGroupToAdd.groupId = this.groupId;
    this.subGroupService.addSubGroups(this.subGroupToAdd).subscribe(
      () => {
        this.alertify.success("Sub-grupa a fost introdusa cu succes!");
        this.loadSubGroups();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  updateSubGroup() {
    this.subGroupForUpdate.name = this.subGroupForm.get("name").value;
    if (this.groupId != null) {
      this.subGroupForUpdate.groupId = this.groupId;
      this.subGroupForUpdate.groupName = this.subGroupForm.get("specializationName").value;
    }
    this.subGroupService.updateSubGroups(this.subGroupForUpdate).subscribe(
      () => {
        this.alertify.success("Sub-grupa a fost modificata cu succes!");
        this.loadSubGroups();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  loadSubGroups() {
    this.subGroupService.getSubGroups().subscribe((data) => {
      this.sendSubGroup.emit(data);
      this.groups = data;
    });
  }

  onSelectionChanged(event: any) {
    this.groupId = event.option.id;
  }

}
