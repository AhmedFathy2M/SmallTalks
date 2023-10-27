import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AccountService } from '../account.service';


@Injectable({
  providedIn: 'root',
})
export class AdminGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const userEmail = this.accountService.getCurrentUserValue()?.email;

    if (userEmail === 'ahmedfathymohamed1998@gmail.com') {
      return true; 
    } else {

      this.router.navigate(['/denied']); 
      return false;
    }
  }
}