import { Component, Input, OnInit } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';
import { TypeDefinition } from '../../models/type-definition';
import { TypeService } from '../../services/type.service';

@Component({
  selector: 'app-generate-types-dialog',
  templateUrl: './generate-types-dialog.component.html',
})
export class GenerateTypesDialogComponent implements OnInit {
  @Input() systemId: number;

  types: TypeDefinition[];
  data: string;
  name: string;
  typesDictionary: {[id: number]: TypeDefinition};
  addDefaultValues = false;

  constructor(protected ref: NbDialogRef<GenerateTypesDialogComponent>, private readonly typeService: TypeService) {
  }

  ngOnInit(): void {
  }

  generate() {
    this.typeService.generate(this.systemId, this.data, this.name, this.addDefaultValues).subscribe(el => {
      this.types = el.filter(a => !a.isBasic);
      this.typesDictionary = el.reduce((pv, cv, ci) => { pv[cv.id] = cv; return pv; }, {});
    });
  }

  save() {
    this.runSaving();
  }

  private async runSaving() {
    const typesReversed = this.types.reverse();
    await Promise.all(typesReversed.map(async (el) => {
      const oldId = el.id;
      el.id = 0;
      el.systemId = this.systemId;
      el.fields.map(d => {
        d.id = 0;
        return d;
      });
      const newId = await this.saveReturnId(el);
      typesReversed.forEach(d => {
        d.fields.map(g => {
          if (g.typeOfFieldId === oldId) {
            g.typeOfFieldId = newId;
          }
          return g;
        });
      });
    }));
    this.ref.close();
  }

  private async saveReturnId(type: TypeDefinition): Promise<number> {
    const created = await this.typeService.post(type).toPromise();
    return created.id;
  }

  dismiss() {
    this.ref.close();
  }
}
