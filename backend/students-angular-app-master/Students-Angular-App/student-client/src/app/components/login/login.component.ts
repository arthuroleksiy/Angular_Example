import { Student } from './../../interfaces/Student';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Roles } from 'src/app/constants/Roles';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loginError: string;
  constructor(private authService: AuthService, private router: Router) {

   }

  ngOnInit() {
    this.loginError = '';
    this.loginForm = new FormGroup({
      login: new FormControl(''),
      password: new FormControl('')
    });
  }

  login(){
    this.authService.login(this.loginForm.value).subscribe(
      (user) => {

        if(user.role === Roles.Admin){
            this.router.navigate(['/students']);
        }
        if(user.role == Roles.Student){
          this.router.navigate(['/studentInformation']);
              }
      },
      (exc) => {
        this.loginError = exc.error;
      }
    );


  }

}
