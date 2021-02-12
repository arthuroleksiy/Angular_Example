import { AuthService } from './../../services/auth.service';
import { CourseService } from 'src/app/services/course.service';
import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable, ObservableInput } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Course } from 'src/app/interfaces/Course';
import { ChatService } from 'src/app/services/chat.service';
import { Message } from 'src/app/interfaces/Message';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss']
})
export class CourseComponent implements OnInit {

  courseId: number;
  course: Course = {name: '', description: '', id: 0};
  messages = new Array<Message>();
  userId: number;
  text: string;
  constructor(private route: ActivatedRoute,
    private authService: AuthService,
    private courseService: CourseService,
    private chatService: ChatService,
    private _ngZone: NgZone ) {
      this.subscribeToEvents();
    }

  ngOnInit() {
    this.courseId = Number(this.route.snapshot.paramMap.get('id'));
    this.userId = this.authService.getCurrentUserValue().userId;
    this.text = '';
    this.route.params.pipe(
      switchMap((params: Params) => this.courseService.getCourse(params['id']))
      ).subscribe((course: Course) =>{
        this.course = course;
        console.log(course);
        this.getAllMessages(course.id);
      });

  }
  getAllMessages(courseId: number){
    this.chatService.getAllMessages(courseId).subscribe(
      (messages: Message[]) => {
        this.messages = messages.reverse();
      }
    );
  }

  addMessage(){
    this.chatService.addMessage({ text: this.text, userId: this.userId, date: new Date(), courseId : this.courseId})
    .subscribe(arg => { this.text = '';});

  }
  getCourse(): void{
    this.courseService.getCourse(this.courseId).subscribe(
      (course: Course) => {
        this.course = course;
      },
      (error) => {

      }
    );
  }

  private subscribeToEvents(): void {

    this.chatService.messageReceived.subscribe((message: Message) => {
      this._ngZone.run(() => {
        if (message.courseId === this.courseId) {
          this.messages.push(message);
        }
      });
    });

  }
}
