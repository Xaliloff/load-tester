import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoadTestConfig } from '../interfaces/models/load-test-config';
import { TestConfigCreationRequest } from '../interfaces/requests/test-config-creation-request';

@Injectable({
  providedIn: 'root',
})
export class LoadTestConfigService {
  loadTestConfigApiUrl: string;
  constructor(private http: HttpClient) {
    this.loadTestConfigApiUrl = environment.baseUri + '/test-configuration/';
  }

  public loadTestConfigCreated: Subject<string> = new Subject();

  getList(): Observable<LoadTestConfig[]> {
    return this.http.get<LoadTestConfig[]>(this.loadTestConfigApiUrl);
  }

  getById(id: string): Observable<LoadTestConfig> {
    return this.http.get<LoadTestConfig>(this.loadTestConfigApiUrl + id);
  }

  create(config: TestConfigCreationRequest): Observable<string> {
    return this.http
      .post<LoadTestConfig>(this.loadTestConfigApiUrl, config)
      .pipe(
        map((loadTestConfig) => {
          this.loadTestConfigCreated.next(loadTestConfig.id);
          return loadTestConfig.id;
        })
      );
  }

  remove(id: string): Observable<any> {
    return this.http.delete<any>(this.loadTestConfigApiUrl + id);
  }

  stop(id: string): Observable<any> {
    return this.http.post<any>(this.loadTestConfigApiUrl + id + '/stop', {});
  }

  run(id: string): Observable<any> {
    return this.http.post<any>(this.loadTestConfigApiUrl + id + '/execute', {});
  }
}
