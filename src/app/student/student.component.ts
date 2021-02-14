
import { FormBuilder } from '@angular/forms';
import { Student } from './../interfaces/Student';
import { Component, OnInit } from '@angular/core';
import {StudentService } from './../student.service';


@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  constructor(private studentService: StudentService, private formBuilder: FormBuilder) { }

  students: Student[] = [];

  AddStudentForm = this.formBuilder.group({
    name: "",
    surname: "",
    birthDate: new Date(),
    studentId: 0
  });

  ngOnInit(): void {
    this.studentService.getStudents().subscribe((data) =>
    {
      this.students = data;
      console.log(data);
    });
  }

  addStudent(student: Student): void {
    this.studentService.addStudent(student).subscribe((data: any) => {
      console.log(data);
      this.studentService.getStudents().subscribe((data) =>
      {
        this.students = data;
        console.log(data);
      });
    }, error => console.log(error));
  }

  deleteStudent(studentId: number) {
    this.studentService.deleteStudent(studentId)
    .subscribe((data) => {
      console.log(data);
      this.studentService.getStudents().subscribe((students) =>
      {
        this.students = students;
        console.log(data);
      });
    });
  }
}
