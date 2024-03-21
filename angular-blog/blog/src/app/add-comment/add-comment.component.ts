import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrls: ['./add-comment.component.css']
})
export class AddCommentComponent implements OnInit {
  newCommentText: string = '';

  @Output() addCommentEvent = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  addComment() {
    this.addCommentEvent.emit(this.newCommentText);
    this.newCommentText = ''; // Clear the textarea after adding comment
  }
}