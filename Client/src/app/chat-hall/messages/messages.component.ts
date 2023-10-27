import { Component, Input } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { IMessage } from 'src/app/shared/models/message';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss']
})
export class MessagesComponent {
@Input() messages: IMessage[] = [];

constructor(public accountService:AccountService) {
  this.accountService.getNumberOfOnlineUsers()
}

messageIsSeen(message: IMessage): boolean {
  const onlineUsers = this.accountService.getNumberOfOnlineUsers();
  console.log("Number of online users: " + onlineUsers);

  if (message.seen === true) {
    return true;
  } else if (onlineUsers > 1) {
    message.seen = true;
    return true;
  } else {
    return false;
  }
}

playAudio(audio:HTMLAudioElement){
   audio.play();
}

}
