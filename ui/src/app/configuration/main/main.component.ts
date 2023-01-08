import { Component } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {
  scheduleActive: string | boolean = "";
  workfolowsActive: string | boolean = ""
  rulesActive: string | boolean = ""
  queueActive: string | boolean = ""
}
