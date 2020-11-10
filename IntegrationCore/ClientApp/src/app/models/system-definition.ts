import { ConnectionFieldDefinition } from './connection-field-definition';
import { TypeDefinition } from './type-definition';

export class SystemDefinition {
  id: number;
  name: string;
  url: string;
  transferType: TransferType;
  dataType: DataType;
  connectionFields: ConnectionFieldDefinition[];
  typeDefinitions: TypeDefinition[];
  getUrlEnding: string;
  postUrlEnding: string;
  putUrlEnding: string;
  deleteUrlEnding: string;
  getByIdentifierUrlEnding: string;
}

export enum TransferType {
  Ftp,
  Api,
}

export enum DataType {
  Json,
  Xml,
}
