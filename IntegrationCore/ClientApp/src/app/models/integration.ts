import { TypeDefinition } from './type-definition';
import { ConnectionFieldValue } from './connection-field-value';
import { FieldConnection } from './field-connection';

export class Integration {
  id: number;
  name: string;
  connectionFieldValues: ConnectionFieldValue[];
  fieldConnections: FieldConnection[];
  typeFromId: number;
  typeFrom: TypeDefinition;
  typeToId: number;
  typeTo: TypeDefinition;
}
