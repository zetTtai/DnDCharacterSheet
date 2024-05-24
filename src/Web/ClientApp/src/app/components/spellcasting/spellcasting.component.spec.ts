import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpellcastingComponent } from './spellcasting.component';

describe('SpellcastingComponent', () => {
  let component: SpellcastingComponent;
  let fixture: ComponentFixture<SpellcastingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SpellcastingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SpellcastingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
