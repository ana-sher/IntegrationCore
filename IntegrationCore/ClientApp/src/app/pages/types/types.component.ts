import { Component, OnInit } from '@angular/core';
import { TypeDefinition } from '../../models/type-definition';
import { TypeService } from '../../services/type.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NbDialogService } from '@nebular/theme';
import { GenerateTypesDialogComponent } from './generate-types-dialog.component';

@Component({
  templateUrl: './types.component.html',
  styleUrls: ['./types.component.sass']
})
export class TypesComponent implements OnInit {
  types: TypeDefinition[];
  systemId: number;
  constructor(private readonly typeService: TypeService, private router: Router,
    private route: ActivatedRoute, private dialogService: NbDialogService) { }


  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = Number(params.get('id'));
      this.systemId = id;
      this.typeService.get(id).subscribe(data => {
        this.types = data;
      });
    });
  }

  edit(i: number) {
    this.router.navigate(['/type', this.systemId, this.types[i].id]);
  }

  create() {
    this.router.navigate(['/type', this.systemId]);
  }

  generate() {
    this.dialogService.open(GenerateTypesDialogComponent, {
      context: {
        systemId: this.systemId,
      },
    }).onClose.subscribe(() => {
      this.typeService.get(this.systemId).subscribe(data => {
        this.types = data;
      });
    });
  }

}
