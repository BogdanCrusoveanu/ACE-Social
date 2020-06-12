import { element } from 'protractor';
import { PostService } from './../../_services/post.service';
import { CommentService } from "./../../_services/comment.service";
import { AuthService } from "./../../_services/auth.service";
import { Comment } from "./../../_models/comment";
import { FormControl } from "@angular/forms";
import {
  Component,
  OnInit,
  ViewChild,
  NgZone,
  Input,
} from "@angular/core";
import { CdkTextareaAutosize } from "@angular/cdk/text-field";
import { take } from "rxjs/operators";
import { Post } from "src/app/_models/post";
import { ActivatedRoute } from "@angular/router";
import { BsModalService } from "ngx-bootstrap";
import { AlertifyService } from "src/app/_services/alertify.service";
import { THIS_EXPR } from "@angular/compiler/src/output/output_ast";

@Component({
  selector: "app-posts-list",
  templateUrl: "./posts-list.component.html",
  styleUrls: ["./posts-list.component.css"],
})
export class PostsListComponent implements OnInit {
  @Input() posts: Post[];
  @Input() allPosts: boolean;
  addedText = new FormControl();
  comment: Comment = new Comment();
  commentsShow: boolean = false;

  constructor(
    private _ngZone: NgZone,
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    public authService: AuthService,
    private commentService: CommentService,
    private postsService: PostService
  ) {}

  @ViewChild("autosize") autosize: CdkTextareaAutosize;

  triggerResize() {
    this._ngZone.onStable
      .pipe(take(1))
      .subscribe(() => this.autosize.resizeToFitContent(true));
  }
  ngOnInit() {
  }

  print() {
    console.log("work");
  }

  toggleComments(post: Post) {
    post.showComments
      ? (post.showComments = false)
      : (post.showComments = true);
  }

  addComment(content: string, postId: number) {
    this.comment.content = content;
    this.comment.postId = postId;
    this.comment.userId = this.authService.decodedToken.nameid;
    this.commentService.addComment(this.comment).subscribe(
      () => {
        this.loadPosts();
        this.alertify.success("Comentariul a fost introdus cu succes!");
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.addedText.reset();
  }

  deletePost(postToDelete: Post) {
    this.alertify.confirm(
      "Sunteti sigur ca doriti sa stergeti aceasta clasa? Toate cursurile care se tin in aceasta sala vor fi sterse!",
      () => {
        this.postsService.deletePost(postToDelete).subscribe(
          (data) => {
            console.log("Clasa a fost adaugata cu succes!");
            const index: number = this.posts.indexOf(postToDelete);
            if (index != -1) this.posts.splice(index, 1);
            this.loadPosts();
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      }
    );
  }

  deleteComment(commentToDelete: Comment) {
    this.alertify.confirm(
      "Sunteti sigur ca doriti sa stergeti aceasta clasa? Toate cursurile care se tin in aceasta sala vor fi sterse!",
      () => {
        this.commentService.deleteComment(commentToDelete).subscribe(
          (data) => {
            console.log("Clasa a fost adaugata cu succes!");
            let index: number = -1;
            this.posts.forEach(element => {
              index = element.comments.indexOf(commentToDelete);
                if (index != -1) element.comments.splice(index, 1);
                this.loadPosts();
            });
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      }
    );
  }

  loadPosts() {
    this.postsService.getAllPosts().subscribe((data) => {
      this.posts = data;
    });
  }
}
