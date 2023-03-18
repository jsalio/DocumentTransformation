import { TimerOption } from "./TimerOption";
import { TimeUnit } from "./TimeUnit";
import { WorkMode } from "./WorkMode";

export interface AppSetting {
  workMode: WorkMode;
  enableSecondQueue: boolean;
  timerWorkMode: TimerOption;
  timeUnit: TimeUnit;
  interval: number;
  startDate: string;
  timeInit: string | '00:00';
  timeEnd: string | '00:00';
}
