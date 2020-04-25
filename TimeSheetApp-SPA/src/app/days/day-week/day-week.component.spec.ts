/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DayWeekComponent } from './day-week.component';

describe('DayWeekComponent', () => {
  let component: DayWeekComponent;
  let fixture: ComponentFixture<DayWeekComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DayWeekComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DayWeekComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
