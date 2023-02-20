import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { map, Observable, take } from 'rxjs';
import { AuthService } from './services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.authService.isLoggedIn // {1}
      .pipe(
        take(1), // {2}
        map((isLoggedIn: boolean) => {
          // {3}
          if (!isLoggedIn) {
            this.router.navigate(['/login']); // {4}
            return false;
          }
          return true;
        })
      );
  }
}
