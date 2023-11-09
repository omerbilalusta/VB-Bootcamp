import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentEFTComponent } from './payment-eft.component';

describe('PaymentEFTComponent', () => {
  let component: PaymentEFTComponent;
  let fixture: ComponentFixture<PaymentEFTComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PaymentEFTComponent]
    });
    fixture = TestBed.createComponent(PaymentEFTComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
