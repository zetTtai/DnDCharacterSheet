import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeaturesFeatsComponent } from './features-feats.component';

describe('FeaturesFeatsComponent', () => {
  let component: FeaturesFeatsComponent;
  let fixture: ComponentFixture<FeaturesFeatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FeaturesFeatsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FeaturesFeatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
