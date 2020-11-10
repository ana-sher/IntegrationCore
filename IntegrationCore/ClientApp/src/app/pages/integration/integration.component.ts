import { Component, OnInit } from '@angular/core';
import { SystemDefinition } from '../../models/system-definition';
import { TypeDefinition } from '../../models/type-definition';
import { TypeService } from '../../services/type.service';
import { SystemService } from '../../services/system.service';
import { FieldDefinition } from '../../models/field-definition';
import { FieldConnection } from '../../models/field-connection';
import { ConnectionFieldValue } from '../../models/connection-field-value';
import { IntegrationService } from '../../services/integration.service';
import { Integration } from '../../models/integration';
import { NbDialogService } from '@nebular/theme';
import { GenerateConnectionsDialogComponent } from './generate-connections-dialog.component';

@Component({
  templateUrl: './integration.component.html',
  styleUrls: ['./integration.component.sass']
})
export class IntegrationComponent implements OnInit {
  systemFrom: SystemDefinition;
  systemTo: SystemDefinition;
  typeFrom: TypeDefinition;
  typeTo: TypeDefinition;

  name: string;

  selectedFieldFrom: FieldDefinition;
  selectedFieldTo: FieldDefinition;

  systems: SystemDefinition[];
  typesFrom: TypeDefinition[];
  typesTo: TypeDefinition[];

  connections: FieldConnection[] = [];
  newConnection: FieldConnection;

  connectionFromValues: ConnectionFieldValue[];
  connectionToValues: ConnectionFieldValue[];

  constructor(private readonly typeService: TypeService,
    private readonly systemService: SystemService,
    private dialogService: NbDialogService,
    private readonly integrationService: IntegrationService) {
      this.systemService.get().subscribe(data => {
        this.systems = data;
      });
     }

  ngOnInit() {
  }

  getTypes(isFrom: boolean) {
    const system = isFrom ? this.systemFrom : this.systemTo;
    this.typeService.get(system.id, true).subscribe(data => {
      if (isFrom) {
        this.typesFrom = data;
      } else {
        this.typesTo = data;
      }
    });
    if (isFrom) {
      this.connectionFromValues = [];
      this.systemFrom.connectionFields.forEach(el => {
        this.connectionFromValues.push({connectionField: el, connectionFieldId: el.id } as ConnectionFieldValue);
      });
    } else {
      this.connectionToValues = [];
      this.systemTo.connectionFields.forEach(el => {
        this.connectionToValues.push({connectionField: el, connectionFieldId: el.id } as ConnectionFieldValue);
      });
    }
  }


  addConnection() {
    this.newConnection = new FieldConnection();
    this.newConnection.firstField = this.selectedFieldFrom;
    this.newConnection.secondField = this.selectedFieldTo;
  }

  saveConnection() {
    this.newConnection = new FieldConnection();
    this.newConnection.firstField = this.selectedFieldFrom;
    this.newConnection.secondField = this.selectedFieldTo;
    this.connections.push(JSON.parse(JSON.stringify(this.newConnection)));
    this.newConnection = null;
    this.selectedFieldFrom = null;
    this.selectedFieldTo = null;
  }

  deleteConnection(i: number) {
    this.connections.splice(i, 1);
  }

  saveIntegration() {
    const integration = new Integration();
    integration.name = this.name;
    integration.typeFromId = this.typeFrom.id;
    integration.typeToId = this.typeTo.id;
    integration.connectionFieldValues = [
      ...this.mapConnValues(JSON.parse(JSON.stringify(this.connectionFromValues))),
      ...this.mapConnValues(JSON.parse(JSON.stringify(this.connectionToValues)))
    ];
    integration.fieldConnections = this.mapConnections(JSON.parse(JSON.stringify(this.connections)));
    this.integrationService.post(integration).subscribe();
  }

  private mapConnValues(values: ConnectionFieldValue[]): ConnectionFieldValue[] {
    values.forEach(el => {
      el.connectionFieldId = el.connectionField.id;
      el.connectionField = null;
    });
    return values;
  }

  private mapConnections(con: FieldConnection[]): FieldConnection[] {
    con.forEach(el => {
      el.firstFieldId = el.firstField.id;
      el.secondFieldId = el.secondField.id;
      el.firstField = null;
      el.secondField = null;
    });
    return con;
  }

  generate() {
    this.dialogService.open(GenerateConnectionsDialogComponent, {
      context: {
        typeFromId: this.typeFrom.id,
        typeToId: this.typeTo.id
      },
    }).onClose.subscribe((data) => {
      if (data) {
        this.connections.push(...data);
      }
    });
  }
}
