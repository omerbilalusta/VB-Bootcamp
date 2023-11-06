import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListDealerComponent } from './list-dealer.component';

describe('ListDealerComponent', () => {
  let component: ListDealerComponent;
  let fixture: ComponentFixture<ListDealerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ListDealerComponent]
    });
    fixture = TestBed.createComponent(ListDealerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
