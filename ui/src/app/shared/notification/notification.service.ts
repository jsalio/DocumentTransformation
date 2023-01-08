import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';


@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private methods: any = [];
  defaultTime = 3000;
  constructor(private toastr: ToastrService, private translate: TranslateService) {
    this.methods = [
      this.showSuccess.bind(this),
      this.showInfo.bind(this),
      this.showWarning.bind(this),
      this.showError.bind(this),
      this.showCustom.bind(this)
    ];
  }

  /**
   * Performs the action to show a message through a toast component with given characteristics
   *
   * @param {ToastModel} toast toast main information
   * @param {ToastType} type type of notification
   * @memberof ToastNotificationsService
   */
  show(toast: ToastModel, type: ToastType, durationTime?: number) {
    toast.options = {
      positionClass: 'toast-bottom-left',
      closeButton: true,
      timeOut: durationTime === undefined ? this.defaultTime : durationTime
    };
    this.toastr.toastrConfig.maxOpened = 1;
    this.toastr.toastrConfig.preventDuplicates = true;
    this.methods[type](toast);
  }

  private showSuccess = (toast: ToastModel) => {
    this.toastr.success(this.translateKey(toast.message), this.translateKey(toast.title), toast.options);
  }

  private showError(toast: ToastModel) {
    this.toastr.error(this.translateKey(toast.message), this.translateKey(toast.title), toast.options);
  }

  private showWarning(toast: ToastModel) {
    this.toastr.warning(this.translateKey(toast.message), this.translateKey(toast.title), toast.options);
  }

  private showInfo(toast: ToastModel) {
    this.toastr.info(this.translateKey(toast.message), this.translateKey(toast.title), toast.options);
  }

  private showCustom(toast: ToastModel) {
    this.toastr.show(this.translateKey(toast.message), this.translateKey(toast.title), toast.options);
  }

  translateKey = (key: string) => {
    return this.translate.instant(key)
  }

}

/**
 * Represents toast message types
 *
 * @export
 * @enum {number}
 */
export enum ToastType {
  Success = 0,
  Info,
  Warning,
  Error,
  Custom
}



/**
 * Represents the data to be send to the toastcontrol
 *
 * @export
 * @class ToastModel
 */
export interface ToastModel {
  title: string;
  message: string;
  options?: any | null | undefined;
}