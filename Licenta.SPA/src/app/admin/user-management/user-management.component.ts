import { ActivatedRoute } from '@angular/router';
import { RolesModalComponent } from './../roles-modal/roles-modal.component';
import { AlertifyService } from './../../_services/alertify.service';
import { AdminService } from './../../_services/admin.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { Pagination, PaginatedResult } from 'src/app/_models/Pagination';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: User[];
  filteredUsers: User[];
  searchText;
  bsModalRef: BsModalRef;
  pagination: Pagination;

  constructor(private adminService: AdminService,
              private alertifyService: AlertifyService,
              private modalService: BsModalService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    // this.adminService.getUsersWithRoles().subscribe((users : User[]) => {
    //   this.users = users;
    // }, error => {
    //   this.alertifyService.error(error);
    // })
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
     // console.log(this.pagination);
    })
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  loadUsers() {
    this.adminService.getUsersWithRoles(this.pagination.currentPage,
       this.pagination.itemsPerPage)
    .subscribe((res: PaginatedResult<User[]>) => {
      this.users = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertifyService.error(error);
    });
  }

  editRolesModal(user: User) {
    const initialState = {
      user,
      roles: this.getRolesArray(user)
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, {initialState});
    this.bsModalRef.content.updateSelectedRoles.subscribe((values) => {
      const rolesToUpdate = {
        roleNames: [...values.filter(el => el.checked === true).map(el => el.name)]
      };
      if(rolesToUpdate) {
        this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(() => {
          user.roles = [...rolesToUpdate.roleNames]
        }, error => {
          this.alertifyService.error(error);
        });
      }
    });
  }

  private getRolesArray(user) {
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any[] =[
    {name: 'Admin', value: 'Admin'},
    {name: 'Student', value: 'Student'},
    {name: 'Profesor', value: 'Profesor'}];

    for( let i = 0; i < availableRoles.length; i++) {
      let isMatch = false;
      for(let j = 0; j < userRoles.length; j++) {
        if(availableRoles[i].name === userRoles[j]) {
          isMatch = true;
          availableRoles[i].checked = true;
          roles.push(availableRoles[i]);
          break;
        }
      }
      if(!isMatch) {
        availableRoles[i].cheched = false;
        roles.push(availableRoles[i]);
      }
    }
    return roles;
  }
}
