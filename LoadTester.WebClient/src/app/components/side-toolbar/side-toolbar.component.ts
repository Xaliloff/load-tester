import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TestConfigCreationRequest } from '../../interfaces/requests/test-config-creation-request';
import { LoadTestConfigService } from 'src/app/services/load-test-config.service';
import { TestConfigCreationFormComponent } from './test-config-creation-form/test-config-creation-form.component';
import { AuthService } from 'src/app/services/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-side-toolbar',
  templateUrl: './side-toolbar.component.html',
  styleUrls: ['./side-toolbar.component.scss'],
})
export class SideToolbarComponent implements OnInit {
  constructor(private dialog: MatDialog, private authService: AuthService) {
    this.isLoggedIn$ = this.authService.isLoggedIn;
  }
  public isLoggedIn$!: Observable<boolean>;
  ngOnInit(): void {}
  openTestConfigCreationDialog() {
    const dialogRef = this.dialog.open(TestConfigCreationFormComponent, {
      width: '700px',
    });
  }

  logout() {
    this.authService.logout();
  }
}
