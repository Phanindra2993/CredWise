import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RepaymentHistotyComponent } from './repayment-histoty.component';

describe('RepaymentHistotyComponent', () => {
  let component: RepaymentHistotyComponent;
  let fixture: ComponentFixture<RepaymentHistotyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RepaymentHistotyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RepaymentHistotyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
