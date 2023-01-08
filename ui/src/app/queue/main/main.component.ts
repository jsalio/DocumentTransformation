import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {


  showAdvance: boolean = true;
  queueForm: FormGroup;
  /**
   *
   */
  constructor(private fb: FormBuilder) {
    this.queueForm = this.fb.group({
      server: this.buildNewControl('Sample', [Validators.required, Validators.maxLength(15)]),
      queueName: this.buildNewControl('sample', [Validators.required, Validators.maxLength(15)]),
      exchange: this.buildNewControl('sample exchange', [Validators.required]),
      username: this.buildNewControl('sample user', [Validators.required]),
      password: this.buildNewControl('sample password', [Validators.required]),
      virtualhost: this.buildNewControl(5672, [Validators.required]),
      port: this.buildNewControl(5672, [Validators.required]),
      prefetchSize: this.buildNewControl(10, [Validators.required]),
      prefetch: this.buildNewControl(10, [Validators.required]),
      heartbeat: this.buildNewControl(1, [Validators.required]),
    })
  }

  buildNewControl = <T extends {}>(defaultValue: T, validator: ValidatorFn[]) => {
    return new FormControl(defaultValue, validator)
  }

  onShowAdvance = () => {
    this.showAdvance = !this.showAdvance;
  }

}
