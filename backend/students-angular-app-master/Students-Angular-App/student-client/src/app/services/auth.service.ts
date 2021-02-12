import { UserRegister } from './../interfaces/user-register';
import { StudCheck } from './../interfaces/StudCheck';
import { User } from './../interfaces/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from "rxjs/operators";
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
   }

  getStudentId(studCheck: StudCheck){
   return this.http.get('https://localhost:44385/api/Authentication/GetStudentId?Name='+ studCheck.name+'&Surname='+studCheck.surname);
  }
  public getCurrentUserValue(): User{
    return this.currentUserSubject.value;
  }

  register(userRegister: UserRegister){
    return this.http.post('https://localhost:44385/api/Authentication/register', userRegister);
  }

  login(userLogin: any){
    return this.http.post('https://localhost:44385/api/Authentication/', userLogin).pipe(
      map((user: User) =>{
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }
    ));
  }

  logout(){
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

}
