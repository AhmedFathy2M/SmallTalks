import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-chat-input',
  templateUrl: './chat-input.component.html',
  styleUrls: ['./chat-input.component.scss']
})
export class ChatInputComponent implements OnInit {
  maxMessageLength!: number;
  content:string = "";
  @Output() contentEmitter = new EventEmitter();
  constructor(private accountService:AccountService) {
    
  }
  ngOnInit(): void {
    const userRole = this.accountService.getCurrentUserValue();
    if (userRole?.role === 'admin') {
      this.maxMessageLength = Number.MAX_SAFE_INTEGER;// Set the maximum length for 'user' role
    } else{
      this.maxMessageLength = 40;
    }
  }

 
  sendMessage()
  {
if(this.content.trim() !== "")
{
 this.contentEmitter.emit(this.content);
}

this.content= '';
  }

}
