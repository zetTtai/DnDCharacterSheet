import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileSliderComponent } from './mobile-slider.component';

describe('MobileSliderComponent', () => {
  let component: MobileSliderComponent;
  let fixture: ComponentFixture<MobileSliderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MobileSliderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MobileSliderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
