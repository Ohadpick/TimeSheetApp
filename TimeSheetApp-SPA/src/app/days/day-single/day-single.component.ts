import { Component, OnInit, ViewChild, HostListener, Input } from '@angular/core';
import { Pagination, PaginationResult } from 'src/app/_models/pagination';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Day } from 'src/app/_models/Day';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { BsDatepickerModule, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { AuthService } from 'src/app/_services/auth.service';
import { GeneralService } from 'src/app/_services/general.service';
import { datepickerAnimation } from 'ngx-bootstrap/datepicker/datepicker-animations';

@Component({
  selector: 'app-day-single',
  templateUrl: './day-single.component.html',
  styleUrls: ['./day-single.component.css']
})
export class DaySingleComponent implements OnInit {
  @Input() day: Day;

  constructor(private authService: AuthService, private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
  }
}
