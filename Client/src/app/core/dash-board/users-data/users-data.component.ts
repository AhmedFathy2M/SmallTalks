import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-users-data',
  templateUrl: './users-data.component.html',
  styleUrls: ['./users-data.component.scss']
})
export class UsersDataComponent implements OnInit {
  users:IUser[] = [];
  number:number =  0;
  onlineUsers:string[] = [];
  constructor(private accountService:AccountService) {
    
    
  }

  ngOnInit(): void {
  this.getAllUsers();
  this.getAllOnlineUsers();
  }

  getAllUsers() {
    this.accountService.getAllUsers().subscribe((response) => {
      this.users = response as IUser[];
      this.users = this.users.map((user) => {
        user.displayName = user.displayName.split('@')[0]; // Remove everything after "@" symbol
        return user;
      });
      
    }, (error) => {
      console.log(error);
    });
  }

  getAllOnlineUsers()
  {
    this.onlineUsers =this.accountService.onlineUsers;
  }
}
