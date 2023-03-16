import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AttemptService } from 'src/app/services/attempt.service';
import { CaseDetails, CaseStatus } from '../main/main.component';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  caseId = 0;
  caseDetails: Array<CaseDetails> = [];

  constructor(private router: Router, private ActivatedRoute: ActivatedRoute, private attemptService: AttemptService) {
    this.ActivatedRoute.params.subscribe((data) => {
      this.caseId = Number.parseInt(data['id']);
    });
  }

  ngOnInit(): void {
    this.attemptService.getCaseDetails(this.caseId).then((data) => {
      console.log(data);
      this.caseDetails = data;
    });
  }

  getBadgeClass = (status: CaseDetails) => {
    if (status.status === CaseStatus.Active) {
      return 'badge-danger';
    } else if (status.status === CaseStatus.Inactive) {
      return 'badge-1';
    } else {
      return 'badge-info';
    }
  }

  getCaseDetails = () => {
    return this.caseDetails;
  }

  goBack = () => {
    this.router.navigate(['lock-items']);
  }
}
