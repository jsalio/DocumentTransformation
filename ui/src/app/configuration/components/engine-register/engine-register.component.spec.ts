import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EngineRegisterComponent } from './engine-register.component';

describe('EngineRegisterComponent', () => {
  let component: EngineRegisterComponent;
  let fixture: ComponentFixture<EngineRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EngineRegisterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EngineRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
