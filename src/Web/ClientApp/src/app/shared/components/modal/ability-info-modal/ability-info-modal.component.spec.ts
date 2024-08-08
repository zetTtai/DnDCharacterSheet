import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AbilityInfoModalComponent } from './ability-info-modal.component';

describe('AbilityInfoModalComponent', () => {
  let component: AbilityInfoModalComponent;
  let fixture: ComponentFixture<AbilityInfoModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AbilityInfoModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AbilityInfoModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
