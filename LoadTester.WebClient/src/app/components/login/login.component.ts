import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit, OnDestroy {
  public subs = new Subscription();
  public loginForm: FormGroup = this.fb.group({
    userName: ['', Validators.required],
    password: ['', Validators.required],
  });
  constructor(private fb: FormBuilder, private authService: AuthService) {}

  ngOnInit(): void {}
  login() {
    this.subs.add(
      this.authService
        .login(
          this.loginForm.controls['userName'].value,
          this.loginForm.controls['password'].value
        )
        .subscribe()
    );
  }
  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }
}
