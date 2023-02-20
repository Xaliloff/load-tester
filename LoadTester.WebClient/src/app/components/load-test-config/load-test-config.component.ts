import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { LoadTestConfig } from 'src/app/interfaces/models/load-test-config';
import { LoadTestConfigService } from 'src/app/services/load-test-config.service';
import { SignalrService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-load-test-config',
  templateUrl: './load-test-config.component.html',
  styleUrls: ['./load-test-config.component.scss'],
})
export class LoadTestConfigComponent implements OnInit {
  public loadTestConfig!: LoadTestConfig;
  public configId!: string;
  public selectedTest!: string | null;
  constructor(
    private route: ActivatedRoute,
    private loadTestConfigService: LoadTestConfigService
  ) {}
  private routeSub: Subscription = new Subscription();
  ngOnInit() {
    this.routeSub.add(
      this.route.params.subscribe((params) => {
        this.configId = params['id'];
        console.log('configId' + this.configId);
        this.routeSub.add(
          this.loadTestConfigService
            .getById(this.configId)
            .subscribe((ltc) => (this.loadTestConfig = ltc))
        );
      })
    );
  }

  ngOnDestroy() {
    //this.signalRService.removeTestRealDataListener(this.configId);
    this.routeSub.unsubscribe();
  }

  goBackFromTest() {
    this.selectedTest = null;
  }

  openTest(id: string) {
    this.selectedTest = id;
  }
}
