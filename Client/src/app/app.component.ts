import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'SmallTalk';

constructor(private accountService: AccountService) {
}

loadCurrentUser()
{
  const token = localStorage.getItem('token');
  if(token){
    
    this.accountService.loadCurrentUser(token).subscribe(()=>{
      
    },(error)=>{console.log(error)})
  }
}

  ngOnInit(): void {
this.loadCurrentUser();
  }

 
  
}
