import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { RuleService } from 'src/app/services/rule.service';

@Component({
  selector: 'app-rules',
  templateUrl: './rules.component.html',
  styleUrls: ['./rules.component.css']
})
export class RulesComponent implements OnInit {
  enableBlockElements: boolean = false;
  rulesForm: FormGroup = undefined as any;

  /**
   *
   */
  constructor(private fb: FormBuilder, private ruleSvr: RuleService) {

  }

  ngOnInit(): void {
    this.rulesForm = this.fb.group({
      'enableLog': new FormControl(false),
      'lockFailsElements': new FormControl(false),
      'tryLimits': new FormControl(2),
      'deleteDocumentAfterSync': new FormControl(false),
      'validateBatchConverttion': new FormControl(false),
      'enableLocalConfig': new FormControl(false),
      'enableConsole': new FormControl(false),
    })
    this.ruleSvr.getRules().then((cfg) => {
      console.log(cfg)
      this.rulesForm.patchValue(cfg as any)
    }).catch(() => { })
  }


  onSave = () => {
    var cfgChanges = this.rulesForm.getRawValue()
    this.ruleSvr.saveChanges(cfgChanges).then(() => { }).catch(() => { })
  }



  translate = (word: string) => {
    return word
  }
}
