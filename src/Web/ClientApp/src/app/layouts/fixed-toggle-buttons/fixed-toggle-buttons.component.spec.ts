import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FixedToggleButtonsComponent } from './fixed-toggle-buttons.component';

describe('FixedToggleButtonsComponent', () => {
  let component: FixedToggleButtonsComponent;
  let fixture: ComponentFixture<FixedToggleButtonsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FixedToggleButtonsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FixedToggleButtonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
