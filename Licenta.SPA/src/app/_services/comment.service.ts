import { Comment } from './../_models/comment';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getComments(id: number) {
    return this.http.get<Comment[]>(this.baseUrl + "comments/get/" + id);
  }

  addComment(comment: Comment) {
    return this.http.post(this.baseUrl + "comments/add", comment);
  }

  updateComment(comment: Comment) {
    return this.http.post(this.baseUrl + "comments/update", comment);
  }

  deleteComment(comment: Comment) {
    return this.http.post(this.baseUrl + "comments/delete", comment);
  }

}
