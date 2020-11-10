import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { FieldConnection } from '../models/field-connection';

@Injectable({
  providedIn: 'root'
})
export class FieldConnectionService {
  private readonly url = 'api/FieldConnections/';
  constructor(private readonly api: ApiService) { }

  generate(typeFromId: number, typeToId: number, objFrom: string, objTo: string): Observable<FieldConnection[]>  {
    return this.api.post(this.url + 'GenerateConnections', {typeFromId, typeToId, objFrom, objTo});
  }

  get(): Observable<FieldConnection[]> {
    return this.api.get(this.url);
  }

  getById(id: number): Observable<FieldConnection> {
    return this.api.get(this.url + id);
  }
  post(obj: FieldConnection): Observable<FieldConnection> {
    return this.api.post(this.url, obj);
  }
  put(obj: FieldConnection): Observable<FieldConnection> {
    return this.api.put(this.url + obj.id, obj);
  }
  delete(id: number): Observable<FieldConnection> {
    return this.api.delete(this.url + id);
  }
}
