/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DaySingleComponent } from './day-single.component';

describe('DaySingleComponent', () => {
  let component: DaySingleComponent;
  let fixture: ComponentFixture<DaySingleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DaySingleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DaySingleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
