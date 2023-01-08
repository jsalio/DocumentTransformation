import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ValidatorFn, FormControl } from '@angular/forms';
import { QueueConfiguration, QueueService } from 'src/app/services/queue.service';
import { LoaderService } from 'src/app/shared/loading-wrapper/loader.service';
import { NotificationService, ToastType } from 'src/app/shared/notification/notification.service';

@Component({
  selector: 'app-queue',
  templateUrl: './queue.component.html',
  styleUrls: ['./queue.component.css']
})
export class QueueComponent {
  showAdvance: boolean = false;
  queueForm: FormGroup;
  /**
   *
   */
  constructor(private fb: FormBuilder, private queueSrv: QueueService, private loader: LoaderService, private notify: NotificationService) {
    this.queueForm = this.fb.group({
      serverUrl: this.buildNewControl('', [Validators.required, Validators.maxLength(50)]),
      queueName: this.buildNewControl('', [Validators.required, Validators.maxLength(50)]),
      exchange: this.buildNewControl('', [Validators.required, Validators.maxLength(50)]),
      username: this.buildNewControl('', [Validators.required]),
      password: this.buildNewControl('', [Validators.required]),
      virtualHost: this.buildNewControl('', [Validators.required]),
      port: this.buildNewControl(0, [Validators.required]),
      prefetchSize: this.buildNewControl(0, [Validators.required]),
      prefetchCount: this.buildNewControl(0, [Validators.required]),
      heartbeat: this.buildNewControl(0, [Validators.required]),
    })
    this.loader.setLoading(true)
    this.queueSrv.getQueue().then((cfg) => {
      debugger
      this.queueForm.patchValue(cfg)
      this.loader.setLoading(false)
    })
  }

  buildNewControl = <T extends {}>(defaultValue: T, validator: ValidatorFn[]) => {
    return new FormControl(defaultValue, validator)
  }

  onShowAdvance = () => {
    this.showAdvance = !this.showAdvance;
  }

  onSave = () => {
    let queueCfg: QueueConfiguration = this.queueForm.getRawValue()
    this.loader.setLoading(true)
    debugger
    this.queueSrv.saveChanges(queueCfg)
      .then(() => {
        this.loader.setLoading(false)
        this.showNotification("ConfigurationSavesd", "Info", ToastType.Info)
      })
      .catch(() => {
        this.loader.setLoading(false)
        this.showNotification("ConfigurationSavesd", "Error", ToastType.Error)
      })
  }

  showNotification = (message: string, title: string, type: ToastType) => {
    this.notify.show({ message: message, title: title }, type)
  }
}
