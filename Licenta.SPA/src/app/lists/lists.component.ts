import { AlertifyService } from './../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { UserService } from './../_services/user.service';
import { AuthService } from './../_services/auth.service';
import { Pagination, PaginatedResult } from './../_models/Pagination';
import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  users: User[];
  recommendations: User[];
  pagination: Pagination;
  likesParam: string;
  showRecommendations: boolean;

  constructor(private userService: UserService,
              private route: ActivatedRoute,
              private alertify: AlertifyService,
              private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
    });
    this.likesParam = 'Likers';
    console.log(this.users);
    this.showRecommendations = false;
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers(this.pagination.currentPage,
       this.pagination.itemsPerPage, null, this.likesParam)
    .subscribe((res: PaginatedResult<User[]>) => {
      this.users = res.result;
      this.pagination = res.pagination;
      this.showRecommendations = false;
    }, error => {
      this.alertify.error(error);
    });
  }

  loadRecommendations() {
    this.userService.gerRecommendedUsers(this.authService.decodedToken.nameid)
    .subscribe( data => {
      this.recommendations = data;
      this.showRecommendations = true;
    }, error => {
      this.alertify.error(error);
    });
  }

}
