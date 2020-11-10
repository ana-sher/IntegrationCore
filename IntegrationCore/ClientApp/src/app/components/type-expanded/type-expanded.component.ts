import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { TypeDefinition } from '../../models/type-definition';
import { FieldDefinition } from '../../models/field-definition';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-type-expanded',
  templateUrl: './type-expanded.component.html',
  styleUrls: ['./type-expanded.component.sass'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TypeExpandedComponent),
      multi: true
    }
  ]
})
export class TypeExpandedComponent implements OnInit, ControlValueAccessor {
  @Input()
  type: TypeDefinition;

  selectedField: FieldDefinition;
  selectedFieldChange: EventEmitter<FieldDefinition>;
  @Input()
  types: TypeDefinition[];
  basicTypes = ['string', 'number', 'boolean'];
  typesDictionary: {[id: number]: TypeDefinition};
  isDisabled: boolean;
  constructor() { }

  onChange: any = () => {};
  onTouched: any = () => {};

  writeValue(selectedField: FieldDefinition): void {
    this.selectedField = selectedField;
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  ngOnInit() {
    this.typesDictionary = this.types.reduce((pv, cv, ci) => { pv[cv.id] = cv; return pv; }, {});
  }

  isBasicType(name: string): boolean {
    return this.basicTypes.includes(name);
  }

  selectField(field: FieldDefinition) {
    if (this.isBasicType(this.typesDictionary[field.typeOfFieldId].name)) {
      this.selectedField = field;
      this.onChange(this.selectedField);
      this.onTouched();
    }
  }
}
