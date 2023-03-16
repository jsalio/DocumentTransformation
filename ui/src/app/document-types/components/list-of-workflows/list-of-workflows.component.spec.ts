import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListOfWorkflowsComponent } from './list-of-workflows.component';

describe('ListOfWorkflowsComponent', () => {
  let component: ListOfWorkflowsComponent;
  let fixture: ComponentFixture<ListOfWorkflowsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListOfWorkflowsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListOfWorkflowsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
