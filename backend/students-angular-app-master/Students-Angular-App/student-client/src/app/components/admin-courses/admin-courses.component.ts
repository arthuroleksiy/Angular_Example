import { CourseService } from 'src/app/services/course.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Course } from 'src/app/interfaces/course';

@Component({
  selector: 'app-admin-courses',
  templateUrl: './admin-courses.component.html',
  styleUrls: ['./admin-courses.component.scss']
})
export class AdminCoursesComponent implements OnInit {

  addCourseForm: FormGroup;
  courses: Course[];
  constructor(private courseService: CourseService) { }

  ngOnInit() {
    this.addCourseForm = new FormGroup({
      name: new FormControl(''),
      description: new FormControl('')
    });
    this.getCourses();
  }

  getCourses(){
    this.courseService.getAllCourses().subscribe(
        (courses: Course[]) =>{
            this.courses = courses;
        }
    );
  }

  addCourse(){
    this.courseService.addCourse(this.addCourseForm.value).subscribe(
      ()=>{
        console.log('Course added');
        this.getCourses();
      }
    );
  }

  deleteCourse(courseId: number){
        this.courseService.deleteCourse(courseId).subscribe(()=>{
          console.log('course deleted');
         this.getCourses();
        });
  }

}
