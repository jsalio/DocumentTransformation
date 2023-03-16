import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import EngineService from 'src/app/services/engine.service';
import { PDFEngine, EngineType, LicenseType, EngineNameType } from '../engine-register/engine-register.component';

@Component({
  selector: 'app-engine-form',
  templateUrl: './engine-form.component.html',
  styleUrls: ['./engine-form.component.css']
})
export class EngineFormComponent {

  currentId: number
  currentEngine: PDFEngine;
  engineForm: FormGroup;
  form: any;
  engineTypeName: EngineNameType.Abby;
  constructor(
    private routerParams: ActivatedRoute,
    private router: Router,
    private engineService: EngineService,
    private fb: FormBuilder
  ) {
    this.buildForm()
    this.routerParams.params.subscribe((params: Params) => {
      this.currentId = params['id'];
      engineService.getEngineList().then((data) => {
        data.forEach((item) => {
          if (item.id === Number.parseInt(this.currentId.toString())) {
            this.currentEngine = item;
            this.form.patchValue(this.currentEngine);
          }
        });
      });
    });

  }

  buildForm = () => {
    this.form = this.fb.group({
      engineTypeName: new FormControl(''),
      engineName: new FormControl('', Validators.required),
      engineDescription: new FormControl('', Validators.required),
      engineVersion: new FormControl('', Validators.required),
      engineType: new FormControl('', Validators.required),
      engineStatus: new FormControl(false, Validators.required),
      licenseType: new FormControl('', Validators.required),
      isDefault: new FormControl(false, Validators.required),
      supportOcr: new FormControl(false, Validators.required)
    });
  }

  goBack = () => {
    this.router.navigate(['/config']);
  }

  engineTypeToList = () => {
    return Object.keys(EngineType).map((key) => {
      return { label: key, value: EngineType[key] };
    });
  }

  engineNameTypeToList = () => {
    return Object.keys(EngineNameType).map((key) => {
      return { label: key, value: EngineNameType[key] };
    });
  }

  licenseTypeToList = () => {
    return Object.keys(LicenseType).map((key) => {
      return { label: key, value: LicenseType[key] };
    });
  }

  changeValue = (event: any, field: string) => {
    debugger
    this.form.get(field).setValue(event.value);
  }

  setEngineType = (event: any) => {
    this.form.get('engineType').setValue(event.value);
  }

  saveChanges = () => {
    if (this.currentId === 0) {
      this.engineService.addEngine(this.form.value).then(() => {
        this.router.navigate(['/config']);
      });
    } else {
      this.engineService.saveChanges(this.form.value, this.currentId).then(() => {
        this.router.navigate(['/config']);
      });
    }
  }

}
