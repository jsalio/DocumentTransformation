import { Component } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  menu = [
    {
      option: 'Dashboard',
      link: '/dashboard'
    },
    {
      option: 'Queue',
      link: '/queue'
    },
    {
      option: 'WorkflowSettings',
      link: '/document-types'
    },
    {
      option: 'Lock',
      link: '/lock-items'
    },
    {
      option: 'Configuration',
      link: '/config'
    }
  ]
}
