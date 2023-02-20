import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainComponent } from './components/main/main.component';
import { LoadTestConfigsListComponent } from './components/main/load-test-configs-list/load-test-configs-list.component';
import { LoadTestConfigComponent } from './components/load-test-config/load-test-config.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatTabsModule } from '@angular/material/tabs';
import { SideToolbarComponent } from './components/side-toolbar/side-toolbar.component';
import { TestConfigCreationFormComponent } from './components/side-toolbar/test-config-creation-form/test-config-creation-form.component';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { SecondsToTimePipe } from './pipes/seconds-to-time.pipe';
import { RunningTimePipe } from './pipes/running-time.pipe';
import { TitleStrategy } from '@angular/router';
import { TemplatePageTitleStrategy } from './strategy/app-title-strategy';
import { LoadTestConfigInfoComponent } from './components/load-test-config/load-test-config-info/load-test-config-info.component';
import { LoadTestConfigTestsComponent } from './components/load-test-config/load-test-config-tests/load-test-config-tests.component';
import { LoadTestComponent } from './components/load-test-config/load-test/load-test.component';
import { LoginComponent } from './components/login/login.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { NgChartsModule } from 'ng2-charts';
import { MyLineChartComponent } from './my-line-chart/my-line-chart.component';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    TestConfigCreationFormComponent,
    LoadTestConfigsListComponent,
    LoadTestConfigComponent,
    SideToolbarComponent,
    ConfirmationDialogComponent,
    SecondsToTimePipe,
    RunningTimePipe,
    LoadTestConfigInfoComponent,
    LoadTestConfigTestsComponent,
    LoadTestComponent,
    LoginComponent,
    MyLineChartComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatTableModule,
    HttpClientModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    FlexLayoutModule,
    MatCardModule,
    MatDividerModule,
    MatTabsModule,
    NgChartsModule,
  ],
  providers: [
    {
      provide: TitleStrategy,
      useClass: TemplatePageTitleStrategy,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
