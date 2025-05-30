import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanStatusComponent } from './loan-status.component';

describe('LoanStatusComponent', () => {
  let component: LoanStatusComponent;
  let fixture: ComponentFixture<LoanStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoanStatusComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoanStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
