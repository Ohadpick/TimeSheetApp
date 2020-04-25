import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';

@Injectable({
    providedIn: 'root'
  })
  export class GeneralService {
    date: Date;
    defaultDateFormat = 'yyyy/MM/dd';

    constructor(private datePipe: DatePipe) { }

    generalDateFormat(date: Date, format: string = this.defaultDateFormat): string {
        const formattedDate = this.datePipe.transform(date, format);

        return formattedDate;
    }

    dateNow(): string {
        this.date = new Date();

        const currentDate = this.generalDateFormat(this.date);

        return currentDate;
    }

    dateFromString(date: string): Date {
      const newDate = new Date(date);

      return newDate;
    }
  }
