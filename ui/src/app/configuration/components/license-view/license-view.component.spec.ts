import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseViewComponent } from './license-view.component';

describe('LicenseViewComponent', () => {
  let component: LicenseViewComponent;
  let fixture: ComponentFixture<LicenseViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LicenseViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LicenseViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
