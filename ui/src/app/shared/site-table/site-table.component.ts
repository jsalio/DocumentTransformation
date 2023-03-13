import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-site-table',
  templateUrl: './site-table.component.html',
  styleUrls: ['./site-table.component.css']
})
export class SiteTableComponent {

  @Input() columns: SiteTableColumn[] = [];
  @Input() dataSet: {} extends Array<infer T> ? T : object[] = [];
  @Input() header: string = '';
  @Input() showHelp: boolean = false;
  @Input() helpText: string = '';
  @Input() includeActions: boolean = false;
}

export interface SiteTableColumn {
  header: string;
  field: string;
}
