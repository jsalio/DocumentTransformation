import { Component, OnInit } from '@angular/core';
import { WorkflowService } from 'src/app/services/workflow.service';
import { Workflow } from 'src/app/shared/models/Workflow';
import { search } from 'src/app/shared/utils/search';

@Component({
  selector: 'app-workflows',
  templateUrl: './workflows.component.html',
  styleUrls: ['./workflows.component.css']
})
export class WorkflowsComponent implements OnInit {

  /**
   *
   */
  constructor(private workflowSvr: WorkflowService) {

  }

  ngOnInit(): void {
    this.workflowSvr.loadActiveElements().then((list) => {
      this.workflows = list
      this.workflowSvr.loadAvailable().then((dataSet) => {
        this.available = dataSet.filter(x => !list.map(y => y.name).includes(x.name))
      }).catch(() => { })
    }).catch(() => { })
  }

  workflows: Array<Workflow> = []
  available: Array<Workflow> = [];
  loading = false

  filteredLeft: Array<Workflow> = [];
  filteredRight: Array<Workflow> = [];

  selectedRigth: Array<Workflow> = []
  selectedLeft: Array<Workflow> = []
  filterLeft: string = ""
  filterRight: string = ""

  assignMarkedElements = (action: 'single right' | 'single left' | 'all rigth' | 'all left') => {
    let actions: Record<'single right' | 'single left' | 'all rigth' | 'all left', () => void> = {
      'all left': () => {
        if (this.workflows.length > 0) {
          let current: Array<Workflow> = this.workflows;
          this.workflows = [];
          this.available = this.available.concat(current)
        }
        this.selectedRigth = [];
      },
      'all rigth': () => {
        if (this.available.length > 0) {
          let current: Array<Workflow> = this.available;
          this.available = [];
          this.workflows = this.workflows.concat(current)
        }
        this.selectedLeft = [];
      },
      'single left': () => {
        if (this.selectedRigth.length > 0) {
          this.workflows = this.workflows.filter(x => !this.selectedRigth.includes(x))
          this.available = this.available.concat(this.selectedRigth)
        }
      },
      'single right': () => {
        if (this.selectedLeft.length > 0) {
          this.available = this.available.filter(x => !this.selectedLeft.includes(x))
          this.workflows = this.workflows.concat(this.selectedLeft)
        }
      }
    };
    actions[action]();
  }

  clearFilterField = (fieldName: string) => {
    let cleanField: Record<string, () => void> = {
      'filterLeft': () => this.filterLeft = "",
      'filterRight': () => this.filterRight = ""
    }
    cleanField[fieldName]();
  }

  onFilter = (field: string) => {
    let filterField: Record<string, () => void> = {
      'filterLeft': () => {
        this.filteredLeft = search(this.available, ['name'], this.filterLeft)
      },
      'filterRight': () => {
        this.filteredRight = search(this.workflows, ['name'], this.filterRight)
      }
    }
    filterField[field]();
  }

  onSaveChange = () => {
    this.workflowSvr.saveChanges(this.workflows).then(() => { }).catch(() => { })
  }

}


