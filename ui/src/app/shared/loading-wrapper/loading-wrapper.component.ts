import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { LoaderService } from './loader.service';

@Component({
  selector: 'app-loading-wrapper',
  templateUrl: './loading-wrapper.component.html',
  styleUrls: ['./loading-wrapper.component.css']
})
export class LoadingWrapperComponent implements OnInit, OnDestroy {
  loaderSubscription: Subscription;
  loading: boolean = false

  /**
   *
   */
  constructor(private loader: LoaderService) {
    this.loaderSubscription = this.loader.loadingState().subscribe((state) => {
      this.loading = state;
    })
  }
  ngOnInit(): void {
    // debugger

  }
  ngOnDestroy(): void {
    // this.loaderSubscription.unsubscribe()
  }
}
