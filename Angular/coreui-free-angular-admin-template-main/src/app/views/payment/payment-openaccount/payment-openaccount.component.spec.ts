import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentOpenaccountComponent } from './payment-openaccount.component';

describe('PaymentOpenaccountComponent', () => {
  let component: PaymentOpenaccountComponent;
  let fixture: ComponentFixture<PaymentOpenaccountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PaymentOpenaccountComponent]
    });
    fixture = TestBed.createComponent(PaymentOpenaccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
