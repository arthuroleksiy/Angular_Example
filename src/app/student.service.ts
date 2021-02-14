import { HttpClient } from '@angular/common/http';
import { Student } from './interfaces/Student';
import { Injectable } from '@angular/core';
import {Observable, throwError} from 'rxjs';
import {map, catchError} from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class StudentService {

getUrl = "https://localhost:44385/api/Students";

constructor(private http: HttpClient) { }

getStudents() : Observable<Student[]> {
  return this.http.get<Student[]>(this.getUrl).pipe(
    map((data: Student[]) => {
      return data;
    }),
      catchError(error => {
        console.log(error);
        return throwError(error);
      }));
}

addStudent(student: Student) {
  return this.http.post<Student>(this.getUrl, student).pipe(
    catchError(error => {
       return throwError(error);
    }));
  }
  deleteStudent(studentId: number) {
    return this.http.delete(this.getUrl + "/" + studentId).pipe(
      catchError(error => {
        return throwError(error);}
        ));
  }
}

