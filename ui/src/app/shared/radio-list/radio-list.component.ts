import { Component, forwardRef, ElementRef, Input, Self } from '@angular/core';
import { NgControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Option } from '../selector/selector.component';

@Component({
  selector: 'app-radio-list',
  templateUrl: './radio-list.component.html',
  styleUrls: ['./radio-list.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    multi: true,
    useExisting: forwardRef(() => RadioListComponent)
  }]
})
export class RadioListComponent {
  @Input() options: Option<{}>[] = [];
  @Input() label: string = "";
  _option: Option<{}> = {} as any

  constructor() {

  }

  ngOnInit(): void {

  }

  get option(): Option<{}> {
    return this.option
  }

  set option(value: Option<{}>) {
    this._option = value;
    this.propagateChange(this._option);
  }

  writeValue(value: Option<{}>) {
    if (value !== undefined) {
      this._option = value;
    }
  }

  propagateChange = (_: any) => { };
  propagateTouched = (_: any) => { };

  registerOnChange(fn: any) {
    this.propagateChange = fn;
  }

  registerOnTouched(fn: any) {
    this.propagateTouched = fn;
  }

  touched($event: any) {
    this.propagateTouched($event);
  }
}
