import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { Subscription, timer } from 'rxjs';
import { LoadTest } from 'src/app/interfaces/models/load-test';
import { LoadTestConfig } from 'src/app/interfaces/models/load-test-config';

@Component({
  selector: 'app-load-test-config-tests',
  templateUrl: './load-test-config-tests.component.html',
  styleUrls: ['./load-test-config-tests.component.scss'],
})
export class LoadTestConfigTestsComponent implements OnInit, OnDestroy {
  public displayedColumns = ['name', 'startTime', 'status'];
  @Input() loadTestConfig!: LoadTestConfig;
  @Output() testSelectedEvent = new EventEmitter<string>();
  public subscription: Subscription = new Subscription();
  constructor() {}

  ngOnInit(): void {
    this.calculateDurations(this.loadTestConfig.tests);
  }
  calculateDurations(tests: LoadTest[]) {
    tests
      .filter((x) => x.status === 'Completed')
      .forEach((x) => {
        x.runningSeconds =
          (new Date(x.endTime).getTime() - new Date(x.startDate).getTime()) /
          1000;
      });
    this.subscription.add(
      timer(0, 500).subscribe((time) => {
        tests
          .filter((x) => x.status === 'Running')
          .forEach((x) => {
            x.runningSeconds =
              (new Date().getTime() - new Date(x.startDate).getTime()) / 1000;
          });
      })
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
