import { SpecializationService } from './../../_services/specialization.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Specialization } from 'src/app/_models/specialization';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal/';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-specialization-modal',
  templateUrl: './specialization-modal.component.html',
  styleUrls: ['./specialization-modal.component.css']
})
export class SpecializationModalComponent implements OnInit {
  @Output() sendSpecialization = new EventEmitter();
  specializationToAdd: Specialization;
  specializationForm: FormGroup;
  specializations: Specialization[];
  specializationForUpdate: Specialization;
  message: string;
  insert: boolean;

  constructor(
    public bsModalRef: BsModalRef,
    private fb: FormBuilder,
    private specializationService: SpecializationService,
    private alertify: AlertifyService
  ) {
  }

  ngOnInit() {
    this.createSpecializationForm();
  }

  createSpecializationForm() {
    if (this.insert) {
      this.specializationForm = this.fb.group({
        name: ["", Validators.required],
      });
    } else {
      this.specializationForm = this.fb.group({
        name: [this.specializationForUpdate.name, Validators.required],
      });
    }
  }

  addSpecialization() {
    this.specializationToAdd = Object.assign({}, this.specializationForm.value);
    this.specializationService.addSpecialization(this.specializationToAdd).subscribe(
      () => {
        this.alertify.success("Specializarea a fost introdusa cu succes!");
        this.loadSpecializations();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  updateSpecialization() {
    this.specializationForUpdate.name = this.specializationForm.get("name").value;
    this.specializationService.UpdateSpecialization(this.specializationForUpdate).subscribe(
      () => {
        this.alertify.success("Specializarea a fost modificata cu succes!");
        this.loadSpecializations();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  loadSpecializations() {
    this.specializationService.getSpecializations().subscribe((data) => {
      this.sendSpecialization.emit(data);
      this.specializations = data;
    });
  }
}
