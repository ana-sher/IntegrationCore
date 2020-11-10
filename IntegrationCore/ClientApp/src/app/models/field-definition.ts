import { TypeDefinition } from './type-definition';

export class FieldDefinition {
  id: number;
  name: string;
  typeOfFieldId: number;
  isArray: boolean;
  isBasicType: boolean;
  required: boolean;
  defaultValue: string;
  typeId: number;
  type: TypeDefinition;
}
