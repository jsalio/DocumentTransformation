import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';


@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  loaderSubject = new Subject<boolean>

  setLoading = (loading: boolean) => {
    this.loaderSubject.next(loading)
  }

  loadingState = () => {
    return this.loaderSubject;
  }
}
