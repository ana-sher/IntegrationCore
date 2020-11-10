import { FieldDefinition } from './field-definition';
import { SystemDefinition } from './system-definition';

export class TypeDefinition {
  id: number;
  name: string;
  fields: FieldDefinition[];
  urlEnding: string;
  systemId: number;
  isBasic: boolean;
  getByIdFieldWrapper: string;
  getFieldWrapper: string;
  postFieldWrapper: string;
  putFieldWrapper: string;
  systemDefinition: SystemDefinition;
}
