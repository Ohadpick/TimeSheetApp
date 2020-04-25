import { Component, OnInit } from '@angular/core';
import { Day } from 'src/app/_models/Day';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { map } from 'rxjs/operators';
import { Pagination, PaginationResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-day-week',
  templateUrl: './day-week.component.html',
  styleUrls: ['./day-week.component.css']
})
export class DayWeekComponent implements OnInit {
  days: Day[];
  dayParams: any = {};
  weekStartDate: string;
  weekEndDate: string;
  pagination: Pagination;

  headers = ['Date', 'Time From', 'Time To'];
  columns = ['reportedDate', 'startDate', 'endDate'];

  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute,
              private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.days = data['Days'].result;
    });
  
    this.loadDays();
  }

  pageChanged(event: any): void {
    this.loadDays();
  }

  resetFilters() {
    //this.dayParams.orderBy = 'lastActive';
    this.loadDays();
  }

  loadDays() {
    this.userService.getDaysForUser(this.authService.decodedToken.nameid, this.weekStartDate, this.weekStartDate)
        .subscribe((res: PaginationResult<Day[]>) => {
          this.days = res.result;
          this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  calculateTotalHHours($event) {
    console.log ('event = ');
    console.log ($event);
  }
}
