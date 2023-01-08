import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { TimeUnit } from 'src/app/shared/models/TimeUnit';
import { WorkMode } from "src/app/shared/models/WorkMode";
import { TimerOption } from "src/app/shared/models/TimerOption";
import { Option } from 'src/app/shared/selector/selector.component';
import { EnumToList } from 'src/app/shared/utils/enumToList';
import { LoaderService } from 'src/app/shared/loading-wrapper/loader.service';
import { AppConfigService, AppSetting } from 'src/app/services/app-config.service';
import { NotificationService, ToastType } from 'src/app/shared/notification/notification.service';
import { HttpErrorResponse } from '@angular/common/http';
import { buildErrorResponse } from 'src/app/shared/models/error-handler';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {
  selectorOptions: Option<string>[] = [];
  timerOptions: Option<string>[] = [];
  workModeOptions: Option<string>[] = [];
  scheduleForm: FormGroup = undefined as any;
  selectedWorkMode = WorkMode.Queue;
  selectedWatchMode = TimerOption.Stopwatch;
  timeUnit = TimeUnit.None;
  touched: boolean = false;

  /**
   *
   */
  constructor(private fb: FormBuilder, private loader: LoaderService, private settingsServ: AppConfigService, private notify: NotificationService) {
  }


  ngOnInit(): void {
    this.buildForm()
    this.loader.setLoading(true)
    this.settingsServ.getConfig().then((payload) => {
      this.loader.setLoading(false)
      this.timeUnit = payload.timeUnit;
      this.scheduleForm.patchValue(payload)
    })
      .catch((message) => {
        console.log(message)
        this.loader.setLoading(false)
        this.showNotification("Error when try to save", "", ToastType.Error)
      })

  }

  buildForm = () => {
    this.scheduleForm = this.fb.group({
      workMode: new FormControl(WorkMode.Queue),
      timeUnit: new FormControl(TimeUnit.None),
      timerWorkMode: new FormControl(TimerOption.Stopwatch),
      interval: new FormControl(10),
      startDate: new FormControl(new Date().toLocaleDateString()),
      timeInit: new FormControl('00:00'),
      timeEnd: new FormControl('23:59'),
      enableSecondQueue: new FormControl(true)
    })
    this.selectorOptions = EnumToList(TimeUnit) as unknown as Option<string>[];
    this.timerOptions = EnumToList(TimerOption) as unknown as Option<string>[];
    this.workModeOptions = EnumToList(WorkMode) as unknown as Option<string>[];
    this.scheduleForm.statusChanges.subscribe((p) => {
      if (this.scheduleForm.dirty) {
        this.touched = true;
      }
    })
  }

  isStoptWatchMode = () => {
    return this.selectedWorkMode === WorkMode.Timer
  }

  isStopWatchModeTimer = () => {
    return this.selectedWatchMode === TimerOption.Stopwatch && this.selectedWorkMode === WorkMode.Timer;
  }

  onChanges(): void {
    this.scheduleForm.get('workMode')?.valueChanges.subscribe(val => {
      console.log(val)
    });
  }

  listenerChange = (e: any) => {
    if ((e.target.value as WorkMode) === WorkMode.Queue) {

    }
  }

  IsSchedule = () => {
    return this.selectedWatchMode === TimerOption.Schedule && this.selectedWorkMode === WorkMode.Timer;
  }

  onSave = () => {
    let updateSettings: AppSetting = this.scheduleForm.getRawValue()
    this.loader.setLoading(true);
    this.settingsServ.saveChange(updateSettings).then(() => {
      this.loader.setLoading(false)
      this.touched = false;
      this.showNotification("ConfigurationSavesd", "Info", ToastType.Info)
    })
      .catch((error: HttpErrorResponse) => {
        this.loader.setLoading(false)
        let details = buildErrorResponse(error)
        this.showNotification(details.message as any, error.status.toString(), ToastType.Error)
      })
  }

  onCancel = () => {
    this.touched = false
  }

  showNotification = (message: string, title: string, type: ToastType) => {
    this.notify.show({ message: message, title: title }, type)
  }

}