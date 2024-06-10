import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputTextModalComponent } from './input-text-modal.component';

describe('InputTextModalComponent', () => {
  let component: InputTextModalComponent;
  let fixture: ComponentFixture<InputTextModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InputTextModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InputTextModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
