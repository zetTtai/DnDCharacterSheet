import { Component, OnInit, OnDestroy, Type, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { SharedDataService } from '../../core/services/shared-data/shared-data.service';

@Component({
  selector: 'app-mobile-slider',
  templateUrl: './mobile-slider.component.html',
  styleUrls: ['./mobile-slider.component.scss']
})
export class MobileSliderComponent implements OnInit, OnDestroy, AfterViewInit {
  @ViewChild('sliderWrapper') sliderWrapper: ElementRef;

  private panSubject = new Subject<string>();
  public mobileComponents: { class: Type<any>, key: string }[] = [];

  constructor(private sharedDataService: SharedDataService) {
    this.mobileComponents = sharedDataService.mobileComponents;
  }

  ngOnInit() {
    this.panSubject.pipe(debounceTime(50)).subscribe(direction => {
      if (direction === 'left' && this.sharedDataService.currentIndex < this.mobileComponents.length - 1) {
        this.sharedDataService.currentIndex++;
      } else if (direction === 'right' && this.sharedDataService.currentIndex > 0) {
        this.sharedDataService.currentIndex--;
      }
    });
  }

  ngAfterViewInit() {
    this.sharedDataService.setSliderWrapper(this.sliderWrapper.nativeElement);
  }

  ngOnDestroy() {
    this.panSubject.unsubscribe();
  }

  get transform() {
    return `translateX(-${this.sharedDataService.currentIndex * 100}%)`;
  }

  onPan(event: any) {
    this.sharedDataService.addTransitionClass();
    if (event.deltaX < 0) {
      this.panSubject.next('left');
    } else if (event.deltaX > 0) {
      this.panSubject.next('right');
    }
  }
}
