import { SystemDefinition } from './system-definition';
import { ConnectionFieldValue } from './connection-field-value';

export class ConnectionFieldDefinition {
  id?: number;
  name: string;
  role: FieldTransferRole;
  systemId?: number;
  systemDefinition?: SystemDefinition;
  connectionFieldValues?: ConnectionFieldValue[];
}

export enum FieldTransferRole {
  Header,
  JsonBody,
  Query
}
