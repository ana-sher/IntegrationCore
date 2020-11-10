import { Component, OnInit } from '@angular/core';
import { TypeDefinition } from '../../models/type-definition';
import { TypeService } from '../../services/type.service';
import { ActivatedRoute } from '@angular/router';
import { FieldDefinition } from '../../models/field-definition';

@Component({
  templateUrl: './type.component.html',
  styleUrls: ['./type.component.sass']
})
export class TypeComponent implements OnInit {
  type: TypeDefinition;
  isEditing = false;
  editIndex: number;
  typesDictionary: {[id: number]: TypeDefinition};
  types: TypeDefinition[];
  newField: FieldDefinition;
  systemId: number;

  constructor(private readonly typeService: TypeService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.type = new TypeDefinition();
    this.route.paramMap.subscribe(params => {
      const id = Number(params.get('id'));
      this.systemId = id;
      const typeId = Number(params.get('typeId'));
      this.typeService.get(id, true).subscribe(data => {
        this.types = data;
        this.typesDictionary = data.reduce((pv, cv, ci) => { pv[cv.id] = cv; return pv; }, {});
        this.type = this.types.find(el => el.id === typeId) || new TypeDefinition();
      });
    });
  }

  addField() {
    this.newField = new FieldDefinition();
    this.isEditing = true;
  }

  saveNewField() {
    if (this.type.fields == null) {
      this.type.fields = [];
    }
    this.type.fields.push(this.newField);
    this.newField = null;
    this.isEditing = false;
  }

  startEditing(i: number) {
    this.isEditing = true;
    this.editIndex = i;
  }

  delete(i: number) {
    this.type.fields.splice(i, 1);
  }

  saveField() {
    this.isEditing = false;
    this.editIndex = -1;
  }

  saveType() {
    if (this.type.id) {
      this.typeService.put(this.type).subscribe();
    } else {
      this.type.systemId = this.systemId;
      this.typeService.post(this.type).subscribe((data) => this.type = data);
    }
  }

  deleteType() {
    this.typeService.delete(this.type.id).subscribe();
  }
}
