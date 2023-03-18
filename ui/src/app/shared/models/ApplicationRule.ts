

export interface ApplicationRule {
  enableLog: boolean;
  lockFailsElements: boolean;
  tryLimits: number;
  deleteDocumentAfterSync: boolean;
  validateBatchConverttion: boolean;
  enableLocalConfig: boolean;
  enableConsole: boolean;
}
