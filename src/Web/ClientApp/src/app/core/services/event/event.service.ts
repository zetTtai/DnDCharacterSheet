import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { GlobalEvent } from 'src/app/shared/models/event.model';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  private eventSubject = new Subject<GlobalEvent>();
  event$ = this.eventSubject.asObservable();

  /**
   * Use to send event beyond the relationship parent-child
   * @param event
   */
  emit(event: GlobalEvent) {
    this.eventSubject.next(event);
  }
}
