import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { DaySingleComponent } from './days/day-single/day-single.component';
import { DaySingleResolver } from './_resolvers/day-single.resolver';
import { DayWeekComponent } from './days/day-week/day-week.component';
import { DayWeekResolver } from './_resolvers/day-week.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'Days', component: DayWeekComponent, resolve: {days: DayWeekResolver } },
            { path: 'members', component: MemberListComponent, resolve: {users: MemberListResolver } },
            { path: 'members/:id', component: MemberDetailComponent, resolve: {user: MemberDetailResolver} },
            { path: 'member/edit', component: MemberEditComponent, resolve: {user: MemberEditResolver},
                                   canDeactivate: [PreventUnsavedChanges] }
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
