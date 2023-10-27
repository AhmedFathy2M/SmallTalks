import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit{
  registerForm!:FormGroup;
 passwordPattern:RegExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,10}$/;
 emailErrorMessage: string = '';
  constructor(private fb: FormBuilder, private accountService:AccountService, private router:Router) {
  
  }
  ngOnInit(): void {
   this.createRegisterForm();
  }
  createRegisterForm() {
    this.registerForm = this.fb.group({
      displayName: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      password:[null, [Validators.required,  Validators.pattern(this.passwordPattern)]]
    });
  }

  onSubmit()
  {
    this.accountService.register(this.registerForm.value).subscribe
    (
      response=>{
        this.router.navigateByUrl('/chat');
      }, error=> {console.log(error);
        if (error.error) {
          this.emailErrorMessage = error.error;
          console.log(this.emailErrorMessage);
        }
      }
    );
  };

}
