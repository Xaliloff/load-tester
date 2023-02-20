import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { TestConfigCreationRequest } from 'src/app/interfaces/requests/test-config-creation-request';
import { LoadTestConfigService } from 'src/app/services/load-test-config.service';

@Component({
  selector: 'app-test-config-creation-form',
  templateUrl: './test-config-creation-form.component.html',
  styleUrls: ['./test-config-creation-form.component.scss'],
})
export class TestConfigCreationFormComponent implements OnInit {
  public testCreationForm = this.fb.group({
    name: ['', [Validators.required]],
    url: ['', [Validators.required]],
    headers: this.fb.array([]),
    body: [''],
    concurrentUsers: [null, [Validators.required]],
    newUsersCreationIntervalInSec: [null],
    increaseUsersBy: [''],
    durationInSeconds: [null, [Validators.required]],
    requestDelayInMs: [null, [Validators.required]],
  });

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<TestConfigCreationFormComponent>,
    private loadTestConfigService: LoadTestConfigService
  ) {}

  ngOnInit(): void {}

  public save() {}

  onSubmit() {
    this.loadTestConfigService
      .create(this.testCreationForm.value as TestConfigCreationRequest)
      .subscribe((id) => {
        this.dialogRef.close();
      });
  }
}
