import { User } from './_models/user';
import { AuthService } from './_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();
  title = "ACE Social"
  constructor(private authService: AuthService,
    private titleService:Title) {
      titleService.setTitle("ACE Social");
    }

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if(user) {
      this.authService.currentUser = user;
      this.authService.changeMemberPhoto(user.photoUrl);
    }
  }
}
