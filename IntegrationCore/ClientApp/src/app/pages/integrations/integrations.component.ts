import { Component, OnInit } from '@angular/core';
import { Integration } from '../../models/integration';
import { IntegrationService } from '../../services/integration.service';
import { Router } from '@angular/router';

@Component({
  templateUrl: './integrations.component.html',
  styleUrls: ['./integrations.component.sass']
})
export class IntegrationsComponent implements OnInit {
  integrations: Integration[];
  constructor(private readonly integrationService: IntegrationService, private router: Router) { }

  ngOnInit() {
    this.integrationService.get().subscribe(data => this.integrations = data);
  }

  create() {
    this.router.navigate(['/integration']);
  }

  viewResults(i: number) {
    this.router.navigate(['/results', this.integrations[i].id]);
  }

  run(i: number) {
    this.integrationService.run(this.integrations[i].id).subscribe();
  }

  delete(i: number) {
    this.integrationService.delete(this.integrations[i].id).subscribe();
  }
}
