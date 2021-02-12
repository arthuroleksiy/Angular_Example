import { Observable } from 'rxjs';
import { AddCourse } from './../interfaces/add-course';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Course } from '../interfaces/course';
import { StudCourse } from '../interfaces/stud-curse';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

constructor(private http: HttpClient) { }

getAllCourses(): Observable<Course[]>{
  return this.http.get<Course[]>('https://localhost:44385/api/Course');
}

addCourse(addCourse: AddCourse){
  return this.http.post('https://localhost:44385/api/Course', addCourse);
}
deleteCourse(courseId: number){
  return this.http.delete('https://localhost:44385/api/Course?courseId=' + courseId);
}

getStudentCourses(studentId): Observable<StudCourse[]>{
  return this.http.get<StudCourse[]>('https://localhost:44385/api/Course/getStudentCourses?studentId=' + studentId);
}

addStudentCourse(studentCourse: any){
  return this.http.post('https://localhost:44385/api/Course/addStudentCourse', studentCourse);
}
deleteStudentCourse(studentCourse: any){
  return this.http.delete('https://localhost:44385/api/Course/deleteStudentCourse?StudentId='+ studentCourse.studentId+ '&CourseId=' + studentCourse.courseId);
}

getCourse(courseId: number){
  return this.http.get('https://localhost:44385/api/Course/getCourse?courseId='+ courseId);
}

}
