import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from 'src/app/shared/models/user';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.scss']
})
export class DashBoardComponent implements OnInit {
adminName!:IUser|null;
userNames: string[] = [];
users: IUser| null[]=[];
constructor(private accountService: AccountService) {

  
}
  ngOnInit(): void {
    this.adminName = this.accountService.getCurrentUserValue();
  }
  generateRegisteredUsersExcelFile() {
    this.accountService.getAllUsers().subscribe((response: any) => {
      const users = response as IUser[];
  
      // Modify user display names
      users.forEach(user => {
        user.displayName = user.displayName.split('@')[0];
      });
  
      // Prepare the data for the Excel file
      const data = [['Email', 'Name']];
      
      users.forEach(user => {
        data.push([user.email, user.displayName]);
      });
  
      const ws = XLSX.utils.aoa_to_sheet(data);
      const wb = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'RegisteredUsers');
      const excelBuffer = XLSX.write(wb, { bookType: 'xlsx', type: 'array' });
  
      // Create a Blob and open it in a new window/tab for download
      const blob = new Blob([excelBuffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      const objectURL = URL.createObjectURL(blob);
      window.open(objectURL);
    }, (error) => {
      console.log(error);
    });
  }

  generateExcelFile() {
    // Fetch user names from your service
    this.accountService.getAllUserNames().subscribe(names => {
      this.userNames = names;

      // Prepare the data for the Excel file
      const data = [['Name']];
      data.push(...this.userNames.map(name => [name]));

      const ws = XLSX.utils.aoa_to_sheet(data);
      const wb = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
      const excelBuffer = XLSX.write(wb, { bookType: 'xlsx', type: 'array' });

      // Create a Blob and open it in a new window/tab for download
      const blob = new Blob([excelBuffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      const objectURL = URL.createObjectURL(blob);
      window.open(objectURL);
    });
  }
}
