import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { Observable } from 'rxjs';
import { SignalrService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-load-test',
  templateUrl: './load-test.component.html',
  styleUrls: ['./load-test.component.scss'],
})
export class LoadTestComponent implements OnInit, OnDestroy {
  @Output() goBackEvent = new EventEmitter<string>();
  @Input() public loadTestId!: string | null;
  constructor(private signalRService: SignalrService) {}
  public testDataStream: Observable<any> = this.signalRService.testDataStream;
  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addTestRealDataListener(this.loadTestId as string);
  }

  ngOnDestroy(): void {
    this.signalRService.removeTestRealDataListener(this.loadTestId as string);
  }
}
