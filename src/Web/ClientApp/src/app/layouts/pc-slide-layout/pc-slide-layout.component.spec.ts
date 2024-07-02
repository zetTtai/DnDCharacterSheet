import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PcSlideLayoutComponent } from './pc-slide-layout.component';

describe('PcSlideLayoutComponent', () => {
  let component: PcSlideLayoutComponent;
  let fixture: ComponentFixture<PcSlideLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PcSlideLayoutComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PcSlideLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
