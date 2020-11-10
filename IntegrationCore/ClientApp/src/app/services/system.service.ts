import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { SystemDefinition } from '../models/system-definition';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SystemService {
  private readonly url = 'api/SystemDefinitions/';
  constructor(private readonly api: ApiService) { }

  get(): Observable<SystemDefinition[]> {
    return this.api.get(this.url);
  }

  getById(id: number): Observable<SystemDefinition> {
    return this.api.get(this.url + id);
  }
  post(obj: SystemDefinition): Observable<SystemDefinition> {
    return this.api.post(this.url, obj);
  }
  put(obj: SystemDefinition): Observable<SystemDefinition> {
    return this.api.put(this.url + obj.id, obj);
  }
  delete(id: number): Observable<SystemDefinition> {
    return this.api.delete(this.url + id);
  }
}
