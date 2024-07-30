import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandscapeWarningComponent } from './landscape-warning.component';

describe('LandscapeWarningComponent', () => {
  let component: LandscapeWarningComponent;
  let fixture: ComponentFixture<LandscapeWarningComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LandscapeWarningComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LandscapeWarningComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
