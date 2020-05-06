import { TeachersResolver } from './_resolvers/teachers.resolver';
import { SemesterResolver } from './_resolvers/semester.resolver';
import { PresentationResolver } from './_resolvers/presentation.resolver';
import { LaboratoryResolver } from './_resolvers/laboratory.resolver';
import { SeminarResolver } from './_resolvers/seminar.resolver';
import { CourseResolver } from './_resolvers/course.resolver';
import { SubGroupResolver } from './_resolvers/subGroup.resolver';
import { GroupResolver } from './_resolvers/group.resolver';
import { SpecializationResolver } from "./_resolvers/specialization.resolver";
import { ClassesResolver } from "./_resolvers/class.resolver";
import { CalendarComponent } from "./calendar/calendar";
import { AdminPanelComponent } from "./admin/admin-panel/admin-panel.component";
import { MessagesResolver } from "./_resolvers/messages.resolver";
import { ListsResolver } from "./_resolvers/lists.resolver";
import { PreventUnsavedChanges } from "./_guards/prevent-unsaved-changes.guard";
import { MemberEditResolver } from "./_resolvers/member-edit.resolver";
import { MemberEditComponent } from "./members/member-edit/member-edit.component";
import { MemberDetailResolver } from "./_resolvers/member-detail.resolver";
import { MemberDetailComponent } from "./members/member-detail/member-detail.component";
import { ListsComponent } from "./lists/lists.component";
import { MessagesComponent } from "./messages/messages.component";
import { MemberListComponent } from "./members/member-list/member-list.component";
import { HomeComponent } from "./home/home.component";
import { Routes } from "@angular/router";
import { AuthGuard } from "./_guards/auth.guard";
import { MemberListResolver } from "./_resolvers/member-list.resolver";
import { ActivitiesResolver } from "./_resolvers/calendar.resolver";
import { UserRolesResolver } from "./_resolvers/users.roles.resolver";
import { ClassManagementComponent } from "./admin/class-management/class-management.component";

export const appRoutes: Routes = [
  { path: "", component: HomeComponent },
  {
    path: "",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      {
        path: "members",
        component: MemberListComponent,
        resolve: { users: MemberListResolver },
      },
      {
        path: "members/:userId",
        component: MemberDetailComponent,
        resolve: { user: MemberDetailResolver },
      },
      {
        path: "member/edit",
        component: MemberEditComponent,
        resolve: { user: MemberEditResolver },
        canDeactivate: [PreventUnsavedChanges],
      },
      {
        path: "messages",
        component: MessagesComponent,
        resolve: { messages: MessagesResolver },
      },
      {
        path: "lists",
        component: ListsComponent,
        resolve: { users: ListsResolver },
      },
      {
        path: "admin",
        component: AdminPanelComponent,
        data: { roles: ["Admin"] },
        resolve: {
          users: UserRolesResolver,
          classes: ClassesResolver,
          specializations: SpecializationResolver,
          groups: GroupResolver,
          subGroups: SubGroupResolver,
          courses: CourseResolver,
          seminars: SeminarResolver,
          laboratories: LaboratoryResolver,
          presentations: PresentationResolver,
          semesters: SemesterResolver,
          teachers: TeachersResolver
        },
      },
      {
        path: "calendar",
        component: CalendarComponent,
        resolve: { activities: ActivitiesResolver },
      },
      {
        path: "classes",
        component: ClassManagementComponent,
        resolve: { classes: ClassesResolver },
      },
    ],
  },
  { path: "**", redirectTo: "", pathMatch: "full" },
];
