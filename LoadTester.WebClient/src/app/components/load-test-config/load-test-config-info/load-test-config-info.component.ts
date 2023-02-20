import { Component, Input, OnInit } from '@angular/core';
import { LoadTestConfig } from 'src/app/interfaces/models/load-test-config';

@Component({
  selector: 'app-load-test-config-info',
  templateUrl: './load-test-config-info.component.html',
  styleUrls: ['./load-test-config-info.component.scss'],
})
export class LoadTestConfigInfoComponent implements OnInit {
  @Input() loadTestConfig!: LoadTestConfig;
  constructor() {}

  ngOnInit(): void {}
}
