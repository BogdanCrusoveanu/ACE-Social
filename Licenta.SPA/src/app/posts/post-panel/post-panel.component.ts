import { Post } from "src/app/_models/post";
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-post-panel",
  templateUrl: "./post-panel.component.html",
  styleUrls: ["./post-panel.component.css"],
})
export class PostPanelComponent implements OnInit {
  posts: Post[];
  allPosts: boolean = true;
  userPosts: Post[];
  commentedPosts: Post[];
  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.getPosts();
  }

  getPosts() {
    this.route.data.subscribe((data) => {
      this.posts = data["posts"];
      this.userPosts = data["userPosts"];
      this.commentedPosts = data["commentedPosts"];
    });
  }
}
