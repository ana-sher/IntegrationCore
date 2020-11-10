import { Component, OnInit } from '@angular/core';
import { SystemDefinition, TransferType } from '../../models/system-definition';
import { SystemService } from '../../services/system.service';
import { ActivatedRoute } from '@angular/router';
import { ConnectionFieldDefinition } from '../../models/connection-field-definition';
import { ConnectionFieldValue } from '../../models/connection-field-value';

@Component({
  templateUrl: './system.component.html',
  styleUrls: ['./system.component.sass']
})
export class SystemComponent implements OnInit {
  system: SystemDefinition;
  isEditing = false;
  editIndex: number;
  newConnField: ConnectionFieldDefinition;
  constructor(private readonly systemService: SystemService, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.system = new SystemDefinition();
    this.system.connectionFields = [];
    this.route.paramMap.subscribe(params => {
      const id = Number(params.get('id'));
      if (id) {
        this.systemService.getById(id).subscribe(el => this.system = el);
      }
    });
  }

  addConnectionFiled() {
    this.newConnField = new ConnectionFieldDefinition();
    this.isEditing = true;
  }

  saveConnectionField() {
    this.system.connectionFields.push(this.newConnField);
    this.newConnField = null;
    this.isEditing = false;
  }

  startEditing(i: number) {
    this.isEditing = true;
    this.editIndex = i;
  }

  delete(i: number) {
    this.system.connectionFields.splice(i, 1);
  }

  saveField() {
    this.isEditing = false;
    this.editIndex = -1;
  }

  saveSystem() {
    if (this.system.id) {
      this.systemService.put(this.system).subscribe();
    } else {
      this.systemService.post(this.system).subscribe(data => this.system = data);
    }
  }

  deleteSystem() {
    this.systemService.delete(this.system.id).subscribe();
  }

  transferTypeChanged() {
    if (this.system.transferType === TransferType.Ftp) {
      if (this.system.connectionFields.filter(el => el.name === 'user').length === 0) {
        this.system.connectionFields
        .push({name: 'user', role: 0}, {name: 'password', role: 0}, {name: 'port', role: 0});
      }
    }
  }
}
