import { Injectable } from '@angular/core';
import { Transaction } from '../models/transaction';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private readonly url = 'api/Transactions/';
  constructor(private readonly api: ApiService) { }

  get(id: number): Observable<Transaction[]> {
    return this.api.get(this.url + id);
  }
}
