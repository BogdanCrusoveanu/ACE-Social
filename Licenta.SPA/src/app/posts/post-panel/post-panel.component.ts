import { AuthService } from './../../_services/auth.service';
import { Post } from "src/app/_models/post";
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-post-panel",
  templateUrl: "./post-panel.component.html",
  styleUrls: ["./post-panel.component.css"],
})
export class PostPanelComponent implements OnInit {
  photoUrl: string;
  posts: Post[];
  allPosts: boolean = true;
  userPosts: Post[];
  commentedPosts: Post[];
  teachersPosts: Post[];
  studentsPosts: Post[];
  constructor(private route: ActivatedRoute,
              private authService: AuthService) {}

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
    this.getPosts();
  }

  getPosts() {
    this.route.data.subscribe((data) => {
      this.posts = data["posts"];
      this.userPosts = data["userPosts"];
      this.commentedPosts = data["commentedPosts"];
      this.teachersPosts = data["teachersPosts"]
      this.studentsPosts = data["studentsPosts"];
    });
  }
}
