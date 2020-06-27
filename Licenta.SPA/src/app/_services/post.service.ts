import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { Post } from '../_models/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getAllPosts() {
    return this.http.get<Post[]>(this.baseUrl + "posts/get");
  }

  getUserPosts(id: number) {
    return this.http.get<Post[]>(this.baseUrl + "posts/get/" + id);
  }

  addPost(post: Post) {
    return this.http.post(this.baseUrl + "posts/add", post);
  }

  updatePost(post: Post) {
    return this.http.post(this.baseUrl + "posts/update", post);
  }

  deletePost(post: Post) {
    return this.http.post(this.baseUrl + "posts/delete", post);
  }

  getPostsWithCurrentUserComment(id: number) {
   return this.http.get<Post[]>(this.baseUrl + "posts/get/commented/" + id);
  }

  getTeachersPosts() {
    return this.http.get<Post[]>(this.baseUrl + "posts/get/teachers");
   }

   getStudentsPosts() {
    return this.http.get<Post[]>(this.baseUrl + "posts/get/students");
   }
}
