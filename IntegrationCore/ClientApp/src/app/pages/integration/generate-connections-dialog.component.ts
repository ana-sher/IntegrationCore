import { Component, Input } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';
import { TypeDefinition } from '../../models/type-definition';
import { TypeService } from '../../services/type.service';
import { FieldConnectionService } from '../../services/field-connection.service';
import { FieldConnection } from '../../models/field-connection';

@Component({
  selector: 'app-generate-connections-dialog',
  templateUrl: './generate-connections-dialog.component.html',
})
export class GenerateConnectionsDialogComponent {
  @Input() typeFromId: number;
  @Input() typeToId: number;

  connections: FieldConnection[];
  objFrom: string;
  objTo: string;

  constructor(protected ref: NbDialogRef<GenerateConnectionsDialogComponent>, private readonly fcService: FieldConnectionService) {
  }

  generate() {
    this.fcService.generate(this.typeFromId, this.typeToId, this.objFrom, this.objTo).subscribe(el => {
      this.connections = el;
    });
  }

  save() {
    this.ref.close(this.connections);
  }

  dismiss() {
    this.ref.close();
  }
}
