import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FdStatusComponent } from './fd-status.component';

describe('FdStatusComponent', () => {
  let component: FdStatusComponent;
  let fixture: ComponentFixture<FdStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FdStatusComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FdStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
