import { AlertifyService } from 'src/app/_services/alertify.service';
import { PostService } from './../../_services/post.service';
import { AuthService } from 'src/app/_services/auth.service';
import { FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/_models/post';

@Component({
  selector: 'app-add-post',
  templateUrl: './add-post.component.html',
  styleUrls: ['./add-post.component.css']
})
export class AddPostComponent implements OnInit {
  addedText: FormControl = new FormControl();
  post: Post = new Post();
  constructor(private authService: AuthService,
              private postService: PostService,
              private alertify: AlertifyService) { }

  ngOnInit() {
  }

  addPost(content: string) {
    this.post.content = content;
    this.post.userId = this.authService.decodedToken.nameid;
    this.postService.addPost(this.post).subscribe(
      () => {
        this.alertify.success("Postarea a fost adăugată!");
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.addedText.reset();
  }

}
