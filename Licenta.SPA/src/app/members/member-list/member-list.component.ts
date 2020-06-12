import { User } from './../../_models/user';
import { PaginatedResult } from './../../_models/Pagination';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from './../../_services/alertify.service';
import { UserService } from './../../_services/user.service';
import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/_models/Pagination';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  users: User[];
  searchText;
  pagination: Pagination;
  search = new FormControl();
  user: User = JSON.parse(localStorage.getItem('user'));
  rolesList = [{value: 'student', display: 'Student'} , {value: 'profesor', display: 'Profesor'}]
  userParams: any = {};
  constructor(private userService: UserService,
              private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
    });

    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;

    this.userParams.orderBy = 'lastActive';

    console.log(this.users);
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  resetFilters() {
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
    this.userParams.role = "";
    this.loadUsers();
    this.search.reset();
  }

  loadUsers() {
    this.userService.getUsers(this.pagination.currentPage,
       this.pagination.itemsPerPage, this.userParams)
    .subscribe((res: PaginatedResult<User[]>) => {
      this.users = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }
}
