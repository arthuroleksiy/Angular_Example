import { UserRegister } from './../../interfaces/user-register';
import { AuthService } from './../../services/auth.service';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) { }

  checkStudForm: FormGroup;
  registerForm: FormGroup;
  studentId: number;
  isIdReceived: boolean;
  checkError : string;
  reigisterError: string;

  ngOnInit() {
    this.checkStudForm = new FormGroup({
      name: new FormControl(''),
      surname: new FormControl('')
    });

    this.registerForm =  new FormGroup({
      login: new FormControl(''),
      password: new FormControl('')
    });
    this.isIdReceived = false;
    this.checkError = '';
    this.reigisterError = '';
  }

  getStudentById(){
    var studCheck = this.checkStudForm.value;
    this.authService.getStudentId(studCheck).subscribe(
      (id: number) =>{
        this.studentId = id;
        this.isIdReceived = true;
      },
      (exc) =>{
        this.checkError = exc.error;
      }
    );
  }

  register(){
    let userRegister = this.registerForm.value;
    userRegister['studentId'] = this.studentId;

    this.authService.register(userRegister).subscribe(
      ()=>{
        this.router.navigate(['/login']);
      },
      (exc) =>{
            this.reigisterError = exc.error;
      }
    )
  }

}
