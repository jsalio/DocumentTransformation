import { Component, Input, forwardRef, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-selector',
  templateUrl: './selector.component.html',
  styleUrls: ['./selector.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    multi: true,
    useExisting: forwardRef(() => SelectorComponent)
  }]
})
export class SelectorComponent implements OnInit, ControlValueAccessor {
  _option: Option<{}> = {} as any
  @Input() options: Option<{}>[] = []
  @Input() label: string = ''

  constructor() { }

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

export interface Option<T> {
  label: string,
  value: T
}
