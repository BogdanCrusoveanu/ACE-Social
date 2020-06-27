import { AlertifyService } from "./../../_services/alertify.service";
import { ClassService } from "./../../_services/class.service";
import { Class } from "./../../_models/class";
import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal/";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";

@Component({
  selector: "app-class-modal",
  templateUrl: "./class-modal.component.html",
  styleUrls: ["./class-modal.component.css"],
})
export class ClassModalComponent implements OnInit {
  @Output() sendClasses = new EventEmitter();
  classToAdd: Class;
  classForm: FormGroup;
  classes: Class[];
  classForUpdate: Class;
  message: string;
  insert: boolean;

  constructor(
    public bsModalRef: BsModalRef,
    private fb: FormBuilder,
    private classService: ClassService,
    private alertify: AlertifyService
  ) {
    this.loadClasses();
  }

  ngOnInit() {
    this.createClassForm();
  }

  createClassForm() {
    if (this.insert) {
      this.classForm = this.fb.group({
        name: ["", Validators.required],
      });
    } else {
      this.classForm = this.fb.group({
        name: [this.classForUpdate.name, Validators.required],
      });
    }
  }

  insertClass() {
    this.classToAdd = Object.assign({}, this.classForm.value);
    this.classService.addClass(this.classToAdd).subscribe(
      () => {
        this.alertify.success("Registration successful");
        this.loadClasses();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  updateClass() {
    this.classForUpdate.name = this.classForm.get("name").value;
    this.classService.updateClass(this.classForUpdate).subscribe(
      () => {
        this.alertify.success("Class was updated");
        this.loadClasses();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  loadClasses() {
    this.classService.getClasses().subscribe((data) => {
      this.sendClasses.emit(data);
      this.classes = data;
    });
  }
}
