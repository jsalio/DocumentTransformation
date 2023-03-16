import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { id } from '@cds/core/internal';
import EngineService from 'src/app/services/engine.service';

@Component({
  selector: 'app-license-view',
  templateUrl: './license-view.component.html',
  styleUrls: ['./license-view.component.css']
})
export class LicenseViewComponent {
  basic = false;
  isEdit = false;
  removeDialog = false;
  engineId: number = 0;

  cols = [
    {
      header: 'License Type',
      field: 'key'
    },
    {
      header: 'License Value',
      field: 'value'
    }
  ]

  currentRow: {
    key: string,
    value: string
  } = {
      key: '',
      value: ''
    }

  jsonLicense = []

  /**
   *
   */
  constructor(private engineService: EngineService, private router: Router, private params: ActivatedRoute) {
    this.params.params.subscribe((data) => {
      this.engineId = Number.parseInt(data['id']);
      this.engineService.getLicenseByEngineId(this.engineId).then((res) => {
        this.jsonLicense = JSON.parse(res.licenseString);
      });
    });
  }

  edit = (row: any) => {
    this.currentRow = row;
    this.basic = true;
    this.isEdit = true;
  }

  addNew = () => {
    this.currentRow = {
      key: '',
      value: ''
    }
    this.isEdit = false;
    this.basic = true;
  }
  removeKey = (row: any) => {
    this.currentRow = row;
    this.removeDialog = true;
  }

  saveNewKey = () => {
    this.jsonLicense.push(this.currentRow);
    this.basic = false;
  }

  saveEditKey = () => {
    const index = this.jsonLicense.findIndex((item) => item.key === this.currentRow.key);
    this.jsonLicense[index] = this.currentRow;
    this.basic = false;
  }

  remove = () => {
    const index = this.jsonLicense.findIndex((item) => item.key === this.currentRow.key);
    this.jsonLicense.splice(index, 1);
    this.removeDialog = false;
  }

  saveLicenseChange = () => {
    this.engineService.updateLicense({
      engineId: this.engineId,
      licenseString: JSON.stringify(this.jsonLicense)
    }, this.engineId).then((res) => {
      console.log(res);
      this.router.navigate(['/config']);
    });
  }
}
