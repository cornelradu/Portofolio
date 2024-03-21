import { Component } from '@angular/core';
import { PostService } from '../post.service';
import { Post, PostFull, Commentary } from '../posts';

@Component({
  selector: 'app-list-posts',
  templateUrl: './list-posts.component.html',
  styleUrl: './list-posts.component.css'
})
export class ListPostsComponent {
  posts: PostFull[] = [];
  selectedFilter: string = 'my-posts'; // Default value
  
  constructor(private postService: PostService) { }

  
  ngOnInit(): void {
    this.fetchPosts();
  }

  fetchPosts(): void {
    this.postService.getPosts(false)
      .subscribe(posts => {
        this.posts = posts;
      });
  }

  getPosts(): void{
    if(this.selectedFilter != 'all'){
      this.postService.getPosts(false)
      .subscribe(posts => {
        this.posts = posts;
      });
    } else {
      this.postService.getPosts(true)
      .subscribe(posts => {
        this.posts = posts;
      });
    }
  }

  onFilterChange(): void {
    this.getPosts();
  }

  addComment(event: any, postId: number): void {
    this.postService.addComment(postId, event).subscribe(posts => {
      this.getPosts();
    });
  }

  deleteCommentary(commentaryId: number) {
    this.postService.deleteComment(commentaryId).subscribe(posts => {
      this.getPosts();
    });

    
  }

  getPostUpvotes(post: PostFull): number {
    return post.votes.reduce((totalUpvotes, vote) => totalUpvotes + (vote.up == false ? -1 : 1), 0);
  };

  getCommentUpvotes(comment: Commentary){
    return comment.votes.reduce((totalUpvotes, vote) => totalUpvotes + (vote.up == false ? -1 : 1), 0);
  }

  votePost(postId: number, upvote: boolean){
    this.postService.votePost(postId, upvote).subscribe(result => {
      this.getPosts();
    });
  }

  voteComment(commentId: number, upvote: boolean){
    this.postService.voteComment(commentId, upvote).subscribe(result => {
      this.getPosts();
    });
  }
}
