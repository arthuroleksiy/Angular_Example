import { AuthService } from './../../services/auth.service';
import { AddStudentCourse } from './../../interfaces/add-student-course';
import { CourseService } from 'src/app/services/course.service';
import { Component, OnInit } from '@angular/core';
import { StudCourse } from 'src/app/interfaces/stud-curse';

@Component({
  selector: 'app-student-courses',
  templateUrl: './student-courses.component.html',
  styleUrls: ['./student-courses.component.scss']
})
export class StudentCoursesComponent implements OnInit {

  iPage  = 4;
  curPage = 1;
  count = 0;

  iPageUnsub  = 4;
  curPageUnsub = 1;
  countUnsub = 0;
  studentId: number;
  courses: StudCourse[];
  constructor(private courseService: CourseService, private authService: AuthService) {

  }

  ngOnInit() {
    this.studentId = this.authService.getCurrentUserValue().studentId;
    this.getStudentCourses();

  }

  getStudentCourses(){
    this.courseService.getStudentCourses(this.studentId).subscribe(
      (courses) =>{
        this.courses = courses;
       // this.countUnsub = 7;
      }
    );

  }

  onDataChange(event){
  this.curPage = event;
  this.getStudentCourses();
  }

  addStudentCourse(course: StudCourse){
    this.courseService.addStudentCourse({courseId: course.id, studentId: this.studentId}).subscribe(
      ()=>{
        this.getStudentCourses();
      }
    );
  }

  deleteStudentCourses(course: StudCourse){
    this.courseService.deleteStudentCourse({courseId: course.id, studentId: this.studentId}).subscribe(
      ()=>{
        this.getStudentCourses();
        console.log('curse for student is deleted');
      }
    );
  }
}
