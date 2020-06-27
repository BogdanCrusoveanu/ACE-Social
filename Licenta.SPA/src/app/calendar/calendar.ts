
import { AlertifyService } from "./../_services/alertify.service";
import { Activity } from "./../_models/activity";
import { AuthService } from "./../_services/auth.service";
import { ActivityService } from "./../_services/activity.service";
import { Component, ChangeDetectionStrategy, OnInit } from "@angular/core";
import { CalendarEvent, CalendarView } from "angular-calendar";

import {
  isSameDay,
  isSameMonth,
} from "date-fns";

import { ActivatedRoute } from "@angular/router";

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3',
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF',
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA',
  },
  green: {
    primary: '#99F99',
    secondary: '#CCFFE5',
  },
};

@Component({
  selector: "mwl-demo-component",
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: "./calendar.html",
})
export class CalendarComponent implements OnInit {
  activeDayIsOpen: boolean = true;
  view: CalendarView = CalendarView.Month;
  activities: Activity[];
  viewDate: Date = new Date();
  event: CalendarEvent = {
    title: "",
    start: new Date(),
  };

  constructor(
    private activityService: ActivityService,
    private authService: AuthService,
    private alertifyService: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.getActivitiesForUser();
    console.log(this.activities);
    console.log(this.events);
  }

  getActivitiesForUser() {
    this.route.data.subscribe((data) => {
      this.activities = data.activities;
      this.mapEvents(this.activities);
    });
  }

  mapEvents(activities) {
    activities.forEach((element) => {
      if (element.type == "Curs") {
        let calEvent: CalendarEvent = {
          title:
            element.type +
            "  " +
            element.name +
            "   \n Profesor  -" +
            element.teacher +
            "-   " +
            element.duration +
            "h   " +
            " Sala   -" +
            element.className +
            "-",
          start: new Date(element.startDate),
          end: new Date(element.endDate),
          color: colors.blue,
        };
        this.events.push(calEvent);
      } else
      if (element.type == "Seminar") {
        let calEvent: CalendarEvent = {
          title:
            element.type +
            "  " +
            element.name +
            "   \n Profesor  -" +
            element.teacher +
            "-   " +
            element.duration +
            "h   " +
            " Sala  -" +
            element.className +
            "-",
          start: new Date(element.startDate),
          end: new Date(element.endDate),
          color: colors.yellow,
        };
        this.events.push(calEvent);
      } else
      if (element.type == "Laborator") {
        let calEvent: CalendarEvent = {
          title:
            element.type +
            "  " +
            element.name +
            "   \n Profesor  -" +
            element.teacher +
            "-   " +
            element.duration +
            "h   " +
            " Sala  -" +
            element.className +
            "-",
          start: new Date(element.startDate),
          end: new Date(element.endDate),
          color: colors.red,
        };
        this.events.push(calEvent);
      } else {
        let calEvent: CalendarEvent = {
          title:
            element.type +
            "  " +
            element.name +
            "-   " +
            element.duration +
            "h   " +
            " Sala  -" +
            element.className +
            "-",
          start: new Date(element.startDate),
          end: new Date(element.endDate),
          color: colors.green,
        };
        this.events.push(calEvent);
      }
    });
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

  events: CalendarEvent[] = [];
}
