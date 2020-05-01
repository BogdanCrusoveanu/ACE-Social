import { element } from 'protractor';
import { AlertifyService } from './../_services/alertify.service';
import { Activity } from './../_models/activity';
import { AuthService } from './../_services/auth.service';
import { ActivityService } from './../_services/activity.service';
import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { colors } from '../calendar-utils/colors';
import { User } from '../_models/user';
import { start } from 'repl';

import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours,
} from 'date-fns';

import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'mwl-demo-component',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './calendar.html',
})
export class CalendarComponent implements OnInit{

  activeDayIsOpen: boolean = true;
  view: CalendarView = CalendarView.Month;
  activities: Activity[];
  viewDate: Date = new Date();
  event: CalendarEvent = {
    title: '',
    start: new Date()
  };

  constructor(private activityService: ActivityService,
              private authService: AuthService,
              private alertifyService: AlertifyService,
              private route: ActivatedRoute) {
  }

  ngOnInit() {
   this.getActivitiesForUser();
   console.log(this.activities);
  }

  // getActivitiesForUser() {
  //   this.activityService.getActivitiesForUser(this.authService.decodedToken.nameid).subscribe(data => {
  //     this.activities = data;
  //     this.mapEvents(this.activities);
  //   }, error => {
  //     this.alertifyService.error(error);
  //   })
  // }

  getActivitiesForUser() {
    this.route.data.subscribe((data => {
      this.activities = data.activities;
      this.mapEvents(this.activities);
    }));
  }

  mapEvents(activities) {
    activities.forEach(element => 
     {
        // this.event.title = element.type + ' ' + element.name + ' "\n" Teacher: ' + element.teacher +  ' "\n" '+ element.categoryName 
        // this.event.start = new Date(element.startDate)
        // this.event.end = new Date(element.endDate)
        // this.event = new CalendarEvent();
        // this.event = {
        //   title: element.name,
        //   start: new Date(element.startDate),
        //   end: new Date(element.endDate)
        // }
        // this.events.push(this.event);
        let calEvent: CalendarEvent = {
          title: element.type + '  ' + element.name + '   \nTeacher  -' + 
                 element.teacher + '-   ' + element.duration + 'h   -' + element.className + '-', 
          start: new Date(element.startDate),
          end: new Date(element.endDate)
        }
        this.events.push(calEvent);
    });
    //this.events.push(this.event);
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  events: CalendarEvent[] = [
    {
      title: 'Editable event',
      color: colors.yellow,
      start: new Date(),
      actions: [
        {
          label: '<i class="fa fa-fw fa-pencil"></i>',
          onClick: ({ event }: { event: CalendarEvent }): void => {
            console.log('Edit event', event);
          },
        },
      ],
    },
    {
      title: 'Deletable event',
      color: colors.blue,
      start: new Date(),
      actions: [
        {
          label: '<i class="fa fa-fw fa-times"></i>',
          onClick: ({ event }: { event: CalendarEvent }): void => {
            this.events = this.events.filter((iEvent) => iEvent !== event);
            console.log('Event deleted', event);
          },
        },
      ],
    },
    {
      title: 'Non editable and deletable event',
      color: colors.red,
      start: new Date(),
    },
  ];
}
