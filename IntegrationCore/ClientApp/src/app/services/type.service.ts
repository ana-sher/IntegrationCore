import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { TypeDefinition } from '../models/type-definition';

@Injectable({
  providedIn: 'root'
})
export class TypeService {
  private readonly url = 'api/TypeDefinitions/';
  constructor(private readonly api: ApiService) { }

  get(systemId: number, showBasic = false): Observable<TypeDefinition[]> {
    return this.api.getArg(this.url + systemId, [{name: 'showBasic', value: showBasic}]);
  }

  generate(systemId: number, data: string, name: string, addDefaultValues: boolean): Observable<TypeDefinition[]> {
    return this.api.post(this.url + 'GenerateTypes', {systemId, data, name, addDefaultValues});
  }

  post(obj: TypeDefinition): Observable<TypeDefinition> {
    return this.api.post(this.url, obj);
  }
  put(obj: TypeDefinition): Observable<TypeDefinition> {
    return this.api.put(this.url + obj.id, obj);
  }
  delete(id: number): Observable<TypeDefinition> {
    return this.api.delete(this.url + id);
  }
}
