import { ConnectionFieldDefinition } from './connection-field-definition';
import { Integration } from './integration';

export class ConnectionFieldValue {
  id: number;
  value: string;
  connectionFieldId: number;
  connectionField: ConnectionFieldDefinition;
  integrationId: number;
  integration: Integration;
}
