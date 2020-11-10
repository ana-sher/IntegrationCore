import { Component, OnInit } from '@angular/core';
import { Transaction } from '../../models/transaction';
import { TransactionService } from '../../services/transaction.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './integration-results.component.html',
  styleUrls: ['./integration-results.component.sass']
})
export class IntegrationResultsComponent implements OnInit {
  transactions: Transaction[];
  integrationId: number;
  constructor(private readonly transactionService: TransactionService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.integrationId = Number(params.get('id'));
      this.getTransactions();
    });
  }

  refresh() {
    this.getTransactions();
  }

  private getTransactions() {
    this.transactionService.get(this.integrationId)
    .subscribe(data => this.transactions = data);
  }

}
