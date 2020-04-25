import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRouteSnapshot, ActivatedRoute } from '@angular/router';
import { PaginationModule } from 'ngx-bootstrap';
import { Pagination, PaginationResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  users: User[];
  userParams: any = {};
  pagination: Pagination;

  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
    });

    this.resetFilters();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  resetFilters() {
    this.userParams.orderBy = 'lastActive';
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
        .subscribe((res: PaginationResult<User[]>) => {
          this.users = res.result;
          this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

}
