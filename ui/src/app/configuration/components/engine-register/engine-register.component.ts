import { Component } from '@angular/core';

@Component({
  selector: 'app-engine-register',
  templateUrl: './engine-register.component.html',
  styleUrls: ['./engine-register.component.css']
})
export class EngineRegisterComponent {

  cols = [
    {
      header: 'Engine Name',
      field: 'engineName'
    },
    {
      header: 'Engine Version',
      field: 'engineVersion'
    },
    {
      header: 'Engine Type',
      field: 'engineType'
    },
    {
      header: 'Status',
      field: 'engineStatus'
    },
    {
      header: 'License Type',
      field: 'licenseType'
    },
    {
      header: 'Default',
      field: 'default'
    },
    {
      header: 'Support OCR',
      field: 'supportOCR'
    }
  ]

  dataSet: Array<PDFEngine> = [
    {
      engineNameType: 'Aspose',
      engineName: 'Aspose',
      engineVersion: '1.0.0',
      engineType: 'PDF',
      engineStatus: 'Active',
      engineDescription: 'Aspose PDF Engine',
      licenseType: 'Internal',
      default: true,
      supportOCR: false,
    },
    {
      engineNameType: 'Teseract',
      engineName: 'Teseract',
      engineVersion: '1.0.0',
      engineType: 'OCR',
      engineStatus: 'Active',
      engineDescription: 'Teseract OCR Engine',
      licenseType: 'Internal',
      default: false,
      supportOCR: true,
    },
    {
      engineNameType: 'Abby',
      engineName: 'Abby',
      engineVersion: '1.0.0',
      engineType: 'OCR',
      engineStatus: 'Active',
      engineDescription: 'Abby OCR Engine',
      licenseType: 'Internal',
      default: false,
      supportOCR: true,
    },
    {
      engineNameType: 'Iris',
      engineName: 'Iris',
      engineVersion: '1.0.0',
      engineType: 'OCR',
      engineStatus: 'Active',
      engineDescription: 'Iris OCR Engine',
      licenseType: 'JsonLicense',
      default: false,
      supportOCR: true,
    }
  ]

  /**
   *
   */
  constructor() {
    console.log(JSON.stringify(this.dataSet));

  }
}


export interface PDFEngine {
  engineNameType: 'Aspose' | 'Teseract' | 'Abby' | 'Iris';
  engineName: string;
  engineVersion: string;
  engineType: 'PDF' | 'OCR';
  engineStatus: string;
  engineDescription: string;
  licenseType: 'Internal' | 'JsonLicense';
  default: boolean;
  supportOCR: boolean;
}
