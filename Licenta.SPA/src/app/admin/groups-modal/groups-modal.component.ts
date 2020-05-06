import { GroupService } from "./../../_services/group-service.service";
import { Specialization } from "./../../_models/specialization";
import { Group } from "./../../_models/group";
import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { BsModalRef } from "ngx-bootstrap";
import { AlertifyService } from "src/app/_services/alertify.service";

@Component({
  selector: "app-groups-modal",
  templateUrl: "./groups-modal.component.html",
  styleUrls: ["./groups-modal.component.css"],
})
export class GroupsModalComponent implements OnInit {
  @Output() sendGroup = new EventEmitter();
  groupToAdd: Group;
  groupForm: FormGroup;
  groups: Group[];
  searchSpecialization;
  groupForUpdate: Group;
  specializations: Specialization[];
  message: string;
  insert: boolean;
  specializationId;

  constructor(
    public bsModalRef: BsModalRef,
    private fb: FormBuilder,
    private groupService: GroupService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.createSpecializationForm();
    console.log(this.specializations);
  }

  createSpecializationForm() {
    if (this.insert) {
      this.groupForm = this.fb.group({
        name: ["", Validators.required],
        specializationName: ["", Validators.required],
      });
    } else {
      this.groupForm = this.fb.group({
        name: [this.groupForUpdate.name, Validators.required],
        specializationName: [
          this.groupForUpdate.specializationName,
          Validators.required,
        ],
      });
      this.searchSpecialization = this.groupForUpdate.specializationName;
    }
  }

  addSpecialization() {
    this.groupToAdd = Object.assign({}, this.groupForm.value);
    this.groupToAdd.specializationId = this.specializationId;
    this.groupService.addGroups(this.groupToAdd).subscribe(
      () => {
        this.alertify.success("Grupua a fost introdusa cu succes!");
        this.loadGroups();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  updateSpecialization() {
    this.groupForUpdate.name = this.groupForm.get("name").value;
    if (this.specializationId != null) {
      this.groupForUpdate.specializationId = this.specializationId;
      this.groupForUpdate.specializationName = this.groupForm.get("specializationName").value;
    }
    this.groupService.updateGroups(this.groupForUpdate).subscribe(
      () => {
        this.alertify.success("Grupa a fost modificata cu succes!");
        this.loadGroups();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  loadGroups() {
    this.groupService.getGroups().subscribe((data) => {
      this.sendGroup.emit(data);
      this.specializations = data;
    });
  }

  onSelectionChanged(event: any) {
    this.specializationId = event.option.id;
  }
}
