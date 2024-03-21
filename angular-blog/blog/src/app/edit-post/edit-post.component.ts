import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../post.service';

@Component({
  selector: 'app-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrl: './edit-post.component.css'
})
export class EditPostComponent {
  postId: number = 0;
  postTitle: string = "";
  postContent: string = "";

  constructor(private route: ActivatedRoute, private postService: PostService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.postId = parseInt(params.get('id')!);
      console.log(this.postId)
      this.fetchPost(this.postId);
    });
  }

  fetchPost(id: number): void {
    // Call your post service to fetch the post data by ID
    this.postService.getPost(id).subscribe(post => {
      console.log(post)
      this.postTitle = post.postTitle;
      this.postContent = post.postContent;
    });
  }

  onSubmit(): void {
    // Call your post service to update the post data
    const updatedPost = {
      postId: this.postId,
      postTitle: this.postTitle,
      postContent: this.postContent
    };
    this.postService.updatePost(updatedPost).subscribe(() => {
      // Handle successful update
    });
  }
}
