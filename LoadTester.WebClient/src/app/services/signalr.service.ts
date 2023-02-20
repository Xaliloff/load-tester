import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private hubConnection!: signalR.HubConnection;
  public testDataStream = new BehaviorSubject<any>(null);
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.baseUri}/hubs/testRealDataHub`, {
        accessTokenFactory: () => localStorage.getItem('token') || '',
      })
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((err) => console.log('Error while starting connection: ' + err));
  };

  public addTestRealDataListener = (testId: string) => {
    console.log(testId);
    this.hubConnection.on(testId, (data) => {
      this.testDataStream.next(data);
    });
  };

  public removeTestRealDataListener = (testId: string) => {
    this.hubConnection.off(testId);
  };
}
