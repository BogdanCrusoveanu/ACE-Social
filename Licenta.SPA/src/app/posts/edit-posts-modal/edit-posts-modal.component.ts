import { PostService } from "./../../_services/post.service";
import { Post } from "./../../_models/post";
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { BsModalRef } from "ngx-bootstrap/modal";
import { AlertifyService } from "src/app/_services/alertify.service";

@Component({
  selector: "app-edit-posts-modal",
  templateUrl: "./edit-posts-modal.component.html",
  styleUrls: ["./edit-posts-modal.component.css"],
})
export class EditPostsModalComponent implements OnInit {
  post: Post;
  updateForm: FormGroup;

  constructor(
    public bsModalRef: BsModalRef,
    private fb: FormBuilder,
    private postService: PostService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.createUpdateForm();
  }

  createUpdateForm() {
    this.updateForm = this.fb.group({
      content: [this.post.content, Validators.required],
    });
  }

  updatePost() {
    this.post.content = this.updateForm.get("content").value;
    this.postService.updatePost(this.post).subscribe(
      () => {
        this.alertify.success("Post was updated");
      },
      (error) => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }
}
