import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PcFixedLayoutComponent } from './pc-fixed-layout.component';

describe('PcFixedLayoutComponent', () => {
  let component: PcFixedLayoutComponent;
  let fixture: ComponentFixture<PcFixedLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PcFixedLayoutComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PcFixedLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
