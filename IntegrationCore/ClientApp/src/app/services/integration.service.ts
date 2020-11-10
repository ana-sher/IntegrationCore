import { Injectable } from '@angular/core';
import { Integration } from '../models/integration';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IntegrationService {
  private readonly url = 'api/Integrations/';
  constructor(private readonly api: ApiService) { }
  get(): Observable<Integration[]> {
    return this.api.get(this.url);
  }
  run(id: number): Observable<any> {
    return this.api.get(this.url + 'Run/' + id);
  }
  getById(id: number): Observable<Integration[]> {
    return this.api.get(this.url + id);
  }
  post(obj: Integration): Observable<Integration> {
    return this.api.post(this.url, obj);
  }
  put(obj: Integration): Observable<Integration> {
    return this.api.put(this.url + obj.id, obj);
  }
  delete(id: number): Observable<Integration> {
    return this.api.delete(this.url + id);
  }
}
