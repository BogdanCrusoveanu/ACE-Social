import { PresentationService } from './../../_services/presentation.service';
import { Presentation } from './../../_models/presentation';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Class } from 'src/app/_models/class';
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap/modal/';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-presentation-modal',
  templateUrl: './presentation-modal.component.html',
  styleUrls: ['./presentation-modal.component.css']
})
export class PresentationModalComponent implements OnInit {

  @Output() sendPresentation = new EventEmitter();
  presentationToAdd: Presentation;
  presentationForm: FormGroup;
  searchClass;
  presentationForUpdate: Presentation;
  classes: Class[];
  message: string;
  insert: boolean;
  classId;
  refresh: Subject<any> = new Subject();
  
  constructor(
    public bsModalRef: BsModalRef,
    private fb: FormBuilder,
    private presentationService: PresentationService,
    private alertify: AlertifyService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.createPresentationForm();
  }

  createPresentationForm() {
    if (this.insert) {
      this.presentationForm = this.fb.group({
        name: ["", Validators.required],
        className: ["", Validators.required],
        startDate: [null, Validators.required],
        endDate: [null, Validators.required]
      });
    } else {
      this.presentationForm = this.fb.group({
        name: [this.presentationForUpdate.name, Validators.required],

        className: [
          this.presentationForUpdate.className,
          Validators.required,
        ],

        startDate: [this.presentationForUpdate.startDate, Validators.required],
        endDate: [this.presentationForUpdate.endDate, Validators.required]
      });
      this.searchClass = this.presentationForUpdate.className;
    }
  }

  addPresentation() {
    this.presentationToAdd = Object.assign({}, this.presentationForm.value);
    this.presentationToAdd.type = "Prezentare";
    this.presentationToAdd.classId = this.classId;
    this.presentationService.addPresentation(this.presentationToAdd).subscribe(
      () => {
        this.alertify.success("Prezentarea a fost introdusa cu succes!");
        this.loadPresentations();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  updatepresentation() {
    this.presentationForUpdate.name = this.presentationForm.get("name").value;

    if (this.classId != null) {
      this.presentationForUpdate.classId = this.classId;
      this.presentationForUpdate.className = this.presentationForm.get("className").value;
    }

    this.presentationForUpdate.startDate = this.presentationForm.get('startDate').value
    this.presentationForUpdate.endDate = this.presentationForm.get('endDate').value

    this.presentationService.updatePresentation(this.presentationForUpdate).subscribe(
      () => {
        this.alertify.success("Prezentarea a fost modificata cu succes!");
        this.loadPresentations();
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  loadPresentations() {
    this.presentationService.getPresentation(this.authService.decodedToken.nameid).subscribe((data) => {
      this.sendPresentation.emit(data);
    });
  }

  onSelectionChangedClass(event: any) {
    this.classId = event.option.id;
  }
}
