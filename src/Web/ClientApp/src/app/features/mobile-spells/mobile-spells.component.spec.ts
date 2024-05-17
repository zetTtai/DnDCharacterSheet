import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileSpellsComponent } from './mobile-spells.component';

describe('MobileSpellsComponent', () => {
  let component: MobileSpellsComponent;
  let fixture: ComponentFixture<MobileSpellsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MobileSpellsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MobileSpellsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
