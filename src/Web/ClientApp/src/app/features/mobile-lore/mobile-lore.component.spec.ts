import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileLoreComponent } from './mobile-lore.component';

describe('MobileLoreComponent', () => {
  let component: MobileLoreComponent;
  let fixture: ComponentFixture<MobileLoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MobileLoreComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MobileLoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
