import { AlertifyService } from './../../_services/alertify.service';
import { UserService } from './../../_services/user.service';
import { AuthService } from './../../_services/auth.service';
import { User } from './../../_models/user';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() user: User;

  constructor(public authService: AuthService,
              private userService: UserService,
              private alertify: AlertifyService) { }

  ngOnInit() {
  }

  sendLike(id: number) {
    if(this.user.isFriend == this.authService.decodedToken.nameid) {
      this.user.isFriend = 0
    } else {
      this.user.isFriend = this.authService.decodedToken.nameid
    }

    this.userService.sendLike(this.authService.decodedToken.nameid, id).subscribe(data => {
      this.alertify.success('Ai adăugat utilizatorul ' + this.user.firstName + " " + this.user.lastName + 'în lista de prieteni!');
    }, error => {
      this.alertify.error(error)
    })
  }

}
