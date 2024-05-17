import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileItemsComponent } from './mobile-items.component';

describe('MobileItemsComponent', () => {
  let component: MobileItemsComponent;
  let fixture: ComponentFixture<MobileItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MobileItemsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MobileItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
