import { SemesterService } from './../../_services/semester.service';
import { Semester } from './../../_models/semester';
import { Component, OnInit} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-semester-modal',
  templateUrl: './semester-modal.component.html',
  styleUrls: ['./semester-modal.component.css']
})
export class SemesterModalComponent implements OnInit {

  semesterForm: FormGroup;
  semesterForUpdate: Semester;
  message: string;
  refresh: Subject<any> = new Subject();
  
  constructor(
    public bsModalRef: BsModalRef,
    private fb: FormBuilder,
    private semesterService: SemesterService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.createSemesterForm();
  }

  createSemesterForm() {
      this.semesterForm = this.fb.group({
        startDate: [this.semesterForUpdate.startDate, Validators.required],
        endDate: [this.semesterForUpdate.endDate, Validators.required]
      });   
  }

  updateSemester() {
    this.semesterForUpdate.startDate = this.semesterForm.get('startDate').value
    this.semesterForUpdate.endDate = this.semesterForm.get('endDate').value
    this.semesterService.updateSemester(this.semesterForUpdate).subscribe(
      () => {
        this.alertify.success("Smestrul a fost modificat cu succes!");
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }
}
