import { StudentsPostsResolver } from './_resolvers/posts.students.resolver';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { CommentedPostsResolver } from './_resolvers/posts.commented.resolver';
import { AddPostComponent } from './posts/add-post/add-post.component';
import { UserPostsResolver } from './_resolvers/posts.user.resolver';
import { EditPostsModalComponent } from './posts/edit-posts-modal/edit-posts-modal.component';
import { PostsListComponent } from './posts/posts-list/posts-list.component';
import { TeachersResolver } from './_resolvers/teachers.resolver';
import { AngularMaterialModule } from './angular-material.module';
import { SemesterModalComponent } from './admin/semester-modal/semester-modal.component';
import { SemesterManagementComponent } from './admin/semester-management/semester-management.component';
import { SemesterResolver } from './_resolvers/semester.resolver';
import { PresentationModalComponent } from './admin/presentation-modal/presentation-modal.component';
import { PresentationManagementComponent } from './admin/presentation-management/presentation-management.component';
import { PresentationResolver } from './_resolvers/presentation.resolver';
import { PresentationService } from './_services/presentation.service';
import { LaboratoryModalComponent } from './admin/laboratory-modal/laboratory-modal.component';
import { SeminarModalComponent } from './admin/seminar-modal/seminar-modal.component';
import { CourseModalComponent } from './admin/course-modal/course-modal.component';
import { SeminarManagementComponent } from './admin/seminar-management/seminar-management.component';
import { LaboratoryManagementComponent } from './admin/laboratory-management/laboratory-management.component';
import { CourseManagementComponent } from './admin/course-management/course-management.component';
import { SeminarResolver } from './_resolvers/seminar.resolver';
import { CourseResolver } from './_resolvers/course.resolver';
import { LaboratoryResolver } from './_resolvers/laboratory.resolver';
import { SubGroupsModalComponent } from './admin/subGroups-modal/subGroups-modal.component';
import { SubGroupsManagementComponent } from './admin/subGroups-management/subGroups-management.component';
import { GroupManagementComponent } from './admin/group-management/group-management.component';
import 'flatpickr/dist/flatpickr.css';

import { GroupsModalComponent } from './admin/groups-modal/groups-modal.component';
import { SubGroupResolver } from './_resolvers/subGroup.resolver';
import { GroupResolver } from './_resolvers/group.resolver';
import { SpecializationModalComponent } from './admin/specialization-modal/specialization-modal.component';
import { SpecializationManagementComponent } from './admin/specialization-management/specialization-management.component';
import { SpecializationResolver } from './_resolvers/specialization.resolver';
import { ClassesResolver } from './_resolvers/class.resolver';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { UserRolesResolver } from './_resolvers/users.roles.resolver';
import { ActivitiesResolver } from './_resolvers/calendar.resolver';
import { CalendarComponent } from './calendar/calendar';
import { CalendarHeaderComponent } from './calendar-utils/calendar-header.component';
import { RolesModalComponent } from './admin/roles-modal/roles-modal.component';
import { AdminService } from './_services/admin.service';
import { PhotoManagementComponent } from './admin/photo-management/photo-management.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { NgModule, Pipe } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { FlatpickrModule } from 'angularx-flatpickr';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { AnimateOnScrollModule } from 'ng2-animate-on-scroll';
import { ListsResolver } from './_resolvers/lists.resolver';
import { appRoutes } from './routes';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { UserService } from './_services/user.service';
import { AuthGuard } from './_guards/auth.guard';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { FileUploadModule } from 'ng2-file-upload';
export function tokenGetter() {
   return localStorage.getItem('token');
}

import { TimeAgoPipe } from 'time-ago-pipe';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { ClassManagementComponent } from './admin/class-management/class-management.component';
import { ClassModalComponent } from './admin/class-modal/class-modal.component';
import { PostsResolver } from './_resolvers/posts.resolver';
import { PostPanelComponent } from './posts/post-panel/post-panel.component';
import { TeachersPostsResolver } from './_resolvers/posts.teacher.resolver';
import { RecommendationResolver } from './_resolvers/recommendations.resolver';

@Pipe({
    name: 'timeAgo',
    pure: false
})
export class TimeAgoExtendsPipe extends TimeAgoPipe {}

export class CustomHammerConfig extends HammerGestureConfig  {
   overrides = {
       pinch: { enable: false },
       rotate: { enable: false }
   };
}

@NgModule({
   declarations: [
      AppComponent,
      SubGroupsManagementComponent,
      SubGroupsModalComponent,
      NavbarComponent,
      CourseManagementComponent,
      LaboratoryManagementComponent,
      SeminarManagementComponent,
      HomeComponent,
      ClassModalComponent,
      CourseModalComponent,
      PresentationManagementComponent,
      PresentationModalComponent,
      SeminarModalComponent,
      LaboratoryModalComponent,
      PostsListComponent,
      SpecializationModalComponent,
      RegisterComponent,
      SpecializationManagementComponent,
      MemberListComponent,
      ListsComponent,
      MessagesComponent,
      MemberCardComponent,
      EditPostsModalComponent,
      MemberDetailComponent,
      MemberEditComponent,
      PhotoEditorComponent,
      AddPostComponent,
      SemesterManagementComponent,
      SemesterModalComponent,
      TimeAgoExtendsPipe,
      MemberMessagesComponent,
      AdminPanelComponent,
      PostPanelComponent,
      HasRoleDirective,
      GroupManagementComponent,
      GroupsModalComponent,
      UserManagementComponent,
      PhotoManagementComponent,
      RolesModalComponent,
      CalendarHeaderComponent,
      CalendarComponent,
      ClassManagementComponent
   ],
   imports: [
      BrowserModule,
      AngularMaterialModule,
      Ng2SearchPipeModule,
      FlatpickrModule.forRoot(),
      HttpClientModule,
      ButtonsModule.forRoot(),
      CarouselModule.forRoot(),
      FormsModule,
      AnimateOnScrollModule.forRoot(),
      PaginationModule.forRoot(),
      BsDatepickerModule.forRoot(),
      ReactiveFormsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      BrowserAnimationsModule,
      RouterModule.forRoot(appRoutes),
      NgxGalleryModule,
      ModalModule.forRoot(),
      FileUploadModule,
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:44383'],
            blacklistedRoutes: ['localhost:44383/auth']
         }
      }),
      CalendarModule.forRoot({ provide: DateAdapter, useFactory: adapterFactory }),
   ],
   exports: [
      CalendarHeaderComponent,
      CalendarComponent
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      MemberEditResolver,
      GroupResolver,
      CourseResolver,
      SemesterResolver,
      StudentsPostsResolver,
      SeminarResolver,
      LaboratoryResolver,
      UserService,
      MessagesResolver,
      CommentedPostsResolver,
      PostsResolver,
      UserPostsResolver,
      PreventUnsavedChanges,
      TeachersResolver,
      SubGroupResolver,
      PresentationService,
      PresentationResolver,
      TeachersPostsResolver,
      RecommendationResolver,
      MemberDetailResolver,
      SpecializationResolver,
      MemberListResolver,
      UserRolesResolver,
      ClassesResolver,
      ActivitiesResolver,
      AdminService,
      ListsResolver,
      { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig }
   ],
   entryComponents: [
      RolesModalComponent
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule {}
