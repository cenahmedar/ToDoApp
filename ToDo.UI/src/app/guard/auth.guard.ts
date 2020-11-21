import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate  {
/**
 *
 */
constructor(private router:Router) { 
}
  canActivate(
    next:ActivatedRouteSnapshot,
    state:RouterStateSnapshot):boolean{
      if(localStorage.getItem('token')!=null)
      return true;
      else{
        this.router.navigate(['/mauth']);
        return false;
      }
     
    } 
  }
  

