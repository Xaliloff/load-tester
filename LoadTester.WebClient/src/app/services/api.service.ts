import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {}

  public get<T>(url: string): Observable<T> {
    return this.http.get<T>(environment.baseUri + url);
  }

  public post<T>(url: string, body: any): Observable<T> {
    return this.http.post<T>(environment.baseUri + url, body);
  }

  public delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(environment.baseUri + url);
  }

  public put<T>(url: string, body: any): Observable<T> {
    return this.http.put<T>(environment.baseUri + url, body);
  }
}
