import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileAccountComponent } from './mobile-account.component';

describe('MobileAccountComponent', () => {
  let component: MobileAccountComponent;
  let fixture: ComponentFixture<MobileAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MobileAccountComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MobileAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
