import { AuthService } from './services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
 public title = "Students-App";
 public login : string;
 isAuth = false;

 constructor(private authService: AuthService, private router: Router){
   this.authService.currentUser.subscribe( user => {
     if( user){
       this.isAuth = true;
       this.login = user.login;
     }
     else{
       this.isAuth = false;
       this.login = '';
     }
   })

 }
 ngOnInit(){
  let currentUser =  this.authService.getCurrentUserValue();
  this.isAuth = currentUser == null ? false : true;
  this.login = currentUser == null ? '' : currentUser.login;
 }
 logout(){

   this.authService.logout();
   this.router.navigate(['/login']);
 }
}
