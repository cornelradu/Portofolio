import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { AuthService } from './auth/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Post, PostFull } from './posts';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  
  private baseUrl = 'http://localhost:5160'; // Change this to your backend URL

  constructor(private http: HttpClient, private authService: AuthService, private toastr: ToastrService) {}

  createPost(postData: { PostTitle: string, PostContent: string }): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`, // Add JWT token to Authorization header
      'Content-Type': 'application/json' // Assuming JSON content type
    });
    return this.http.post(`${this.baseUrl}/Post/add_post`, postData, { headers })
  }
  
  getPosts(getAll: boolean): Observable<PostFull[]> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`, 
      'Content-Type': 'application/json' 
    });

    if(!getAll){
      return this.http.get<PostFull[]>(`${this.baseUrl}/Post/list_posts`,  { headers });
    } else {
      return this.http.get<PostFull[]>(`${this.baseUrl}/Post/list_posts?getAll=true`,  { headers });

    }
  }

  getPost(id: number): Observable<Post> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    });

    return this.http.get<Post>(`${this.baseUrl}/Post/get_post/${id}`,  { headers });

  }

  updatePost(updatedPost: { postId: number; postTitle: string; postContent: string; }) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json' 
    });

    return this.http.put<void>(`${this.baseUrl}/Post/edit_post/${updatedPost.postId}`, {postTitle: updatedPost.postTitle, postContent: updatedPost.postContent},   { headers });
  }

  addComment(postId: number, commentText: string): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`, 
      'Content-Type': 'application/json'
    });
    return this.http.post(`${this.baseUrl}/Commentary/add_commentary`, {PostId: postId, CommentText: commentText}, { headers })
  }

  deleteComment(commentaryId: number): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`, 
      'Content-Type': 'application/json'
    });
    return this.http.delete(`${this.baseUrl}/Commentary/delete_commentary/${commentaryId}`, { headers })
  }

  votePost(postId: number, upvote: boolean){
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`, 
      'Content-Type': 'application/json'
    });
    return this.http.post(`${this.baseUrl}/Post/add_vote/${postId}?up=${upvote}`, {}, { headers })
  }

  voteComment(commentId: number, upvote: boolean){
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`, 
      'Content-Type': 'application/json'
    });
    return this.http.post(`${this.baseUrl}/Commentary/add_vote/${commentId}?up=${upvote}`, {}, { headers })
  }
}
