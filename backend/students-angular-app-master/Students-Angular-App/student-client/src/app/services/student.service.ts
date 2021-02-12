import { Student } from './../interfaces/Student';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

getUrl = 'https://localhost:44385/api/Students';

constructor(private http: HttpClient) { }

  getStudents(): Observable<Student[]>{
    return this.http.get<Student[]>(this.getUrl);
  }

  addStudent( student: Student): Observable<any>{
    return this.http.post(this.getUrl, student);
  }

  deleteStudent(studentId: number) : Observable<any> {
      return this.http.delete(this.getUrl + '/' + studentId);
  }
}
