import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PcSliderComponent } from './pc-slider.component';

describe('PcSliderComponent', () => {
  let component: PcSliderComponent;
  let fixture: ComponentFixture<PcSliderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PcSliderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PcSliderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
