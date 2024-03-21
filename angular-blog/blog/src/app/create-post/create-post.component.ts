import { Component } from '@angular/core';
import { PostService } from '../post.service';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrl: './create-post.component.css'
})
export class CreatePostComponent {
  postTitle: string = '';
  postContent: string = '';

  constructor(private postService: PostService) {}

  onSubmit() {
    if (!this.postTitle || !this.postContent) {
      // Handle empty fields error
      return;
    }
    
    this.postService.createPost({ PostTitle: this.postTitle, PostContent: this.postContent })
      .subscribe(
        response => {
          // Handle success response
          console.log('Post created successfully:', response);
          // Optionally, you can reset the form fields
          this.postTitle = '';
          this.postContent = '';
        },
        error => {
          // Handle error response
          console.error('Error creating post:', error);
        }
      );
  }
}
