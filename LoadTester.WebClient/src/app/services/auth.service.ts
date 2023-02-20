import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(false);

  public get isLoggedIn(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  constructor(private router: Router, private http: HttpClient) {
    this.loggedIn.next(!!localStorage.getItem('token'));
  }

  public login(userName: string, password: string): Observable<any> {
    console.log(userName + ' ' + password);
    return this.http
      .post<any>(environment.baseUri + '/login', { userName, password })
      .pipe(
        map((res) => {
          if (res && res.token) {
            localStorage.setItem('token', res.token);
            this.loggedIn.next(true);
            this.router.navigate(['/']);
          }
        })
      );
  }

  logout() {
    localStorage.removeItem('token');
    this.loggedIn.next(false);
    this.router.navigate(['/login']);
  }
}
