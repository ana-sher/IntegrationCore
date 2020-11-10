import { Component, OnInit } from '@angular/core';
import { SystemService } from '../../services/system.service';
import { SystemDefinition } from '../../models/system-definition';
import { Router } from '@angular/router';

@Component({
  templateUrl: './systems.component.html',
  styleUrls: ['./systems.component.sass']
})
export class SystemsComponent implements OnInit {
  systems: SystemDefinition[];
  constructor(private readonly systemService: SystemService, private router: Router) { }

  ngOnInit() {
    this.systemService.get().subscribe(data => this.systems = data);
  }

  edit(i: number) {
    this.router.navigate(['/system', this.systems[i].id ]);
  }

  create() {
    this.router.navigate(['/system']);
  }

  viewTypes(i: number) {
    this.router.navigate(['/types', this.systems[i].id ]);
  }
}
