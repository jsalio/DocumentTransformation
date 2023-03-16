import { Component } from '@angular/core';
import { Router } from '@angular/router';
import EngineService from 'src/app/services/engine.service';

@Component({
  selector: 'app-engine-register',
  templateUrl: './engine-register.component.html',
  styleUrls: ['./engine-register.component.css']
})
export class EngineRegisterComponent {

  cols = [
    {
      header: 'EngineName',
      field: 'engineName'
    },
    {
      header: 'EngineVersion',
      field: 'engineVersion'
    },
    {
      header: 'EngineType',
      field: 'engineType'
    },
    {
      header: 'Status',
      field: 'engineStatus'
    },
    {
      header: 'LicenseType',
      field: 'licenseType'
    },
    {
      header: 'Default',
      field: 'isDefault'
    },
    {
      header: 'SupportOcr',
      field: 'supportOcr'
    }
  ]

  dataSet: Array<PDFEngine> = []

  /**
   *
   */
  constructor(private engineService: EngineService, private router: Router) {
    engineService.getEngineList().then((data) => {
      this.dataSet = data;

    });
  }

  edit = (row: PDFEngine) => {
    this.router.navigate(['config/engine', row.id]);
  }

  viewLicense = (row: PDFEngine) => {
    this.router.navigate(['config/engine/license', row.id]);
  }

  addNew = () => {
    this.router.navigate(['config/engine', 0]);
  }
}


export interface PDFEngine {
  id: number;
  engineNameType: EngineNameType;
  engineName: string;
  engineVersion: string;
  engineType: EngineType;
  engineStatus: string;
  engineDescription: string;
  licenseType: 'Internal' | 'JsonLicense';
  isDefault: boolean;
  supportOcr: boolean;
}

export enum EngineNameType {
  Aspose = 'Aspose',
  Teseract = 'Teseract',
  Abby = 'Abby',
  Iris = 'Iris'
}

export enum EngineType {
  PDF = 'PDF',
  OCR = 'OCR'
}

export enum LicenseType {
  Internal = 'Internal',
  JsonLicense = 'JsonLicense'
}

export interface EngineLicense {
  engineId: number
  licenseString: string
}
