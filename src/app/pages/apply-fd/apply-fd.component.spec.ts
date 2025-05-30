import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyFdComponent } from './apply-fd.component';

describe('ApplyFdComponent', () => {
  let component: ApplyFdComponent;
  let fixture: ComponentFixture<ApplyFdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApplyFdComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApplyFdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
