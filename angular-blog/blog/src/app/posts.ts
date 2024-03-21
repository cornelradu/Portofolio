export  interface User {
    userId: number;
    firstName: string;
    lastName: string;
    email: string;
    gender: string;
    active: boolean;
  }
  
  // Define Post interface
  export  interface Post {
    postId: number;
    userId: number; // Foreign key referencing User.userId
    postTitle: string;
    postContent: string;
    postCreated: Date;
    postUpdated: Date;
    user: User
  }

  export interface Commentary {
    id: number;
    postId: number;
    userId: number;
    commentText: string;
    commentCreated: Date;
    commentUpdated: Date;
    user: User,
    votes: Vote[]
}

  export interface PostFull {
    post: Post,
    commentaries: Commentary[],
    votes: Vote[]
  }

  export interface Vote{
    id: number,
    up: boolean
  }

