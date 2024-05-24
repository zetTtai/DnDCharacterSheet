import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoreComponent } from './lore.component';

describe('LoreComponent', () => {
  let component: LoreComponent;
  let fixture: ComponentFixture<LoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoreComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
