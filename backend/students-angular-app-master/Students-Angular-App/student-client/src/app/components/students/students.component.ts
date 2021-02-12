import { Student } from '../../interfaces/Student';
import { StudentService } from 'src/app/services/student.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit {

  constructor(private studentService: StudentService, private formBuilder: FormBuilder) { }

  students: Student[] = [];
  student: Student;
  addStudentForm = this.formBuilder.group({
    name:  [ '', [Validators.required, Validators.minLength(1) ]],
    surname: [ '', [Validators.required, Validators.minLength(1)]] ,
    birthDate :  [ '', [Validators.required ]],
    studentId: 0

  });

  ngOnInit(): void {
    this.studentService.getStudents().subscribe((data) => {
        this.students = data;
        console.log(data);
    });
  }

  addStudent(student: Student): void {
    this.studentService.addStudent(student).subscribe((data :any) => {
      console.log(data);
      this.studentService.getStudents().subscribe((data) => {
        this.students = data;
        console.log(data);
    });
    },
     (error) => {console.log(error);}
    );
  }

  deleteStudent(studentId: number): void{
    this.studentService.deleteStudent(studentId)
    .subscribe((data) => {
      console.log(data);
      this.studentService.getStudents().subscribe((students) => {
        this.students = students;
        console.log(data);
    });
    });
  }

}
