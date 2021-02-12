import { AdminCoursesComponent } from './../components/admin-courses/admin-courses.component';
import { LoginComponent } from './../components/login/login.component';
import { RegisterComponent } from './../components/register/register.component';
import { StudentsComponent } from '../components/students/students.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentInformationComponent } from '../components/student-information/student-information.component';
import { AuthGuard } from '../helpers/auth-guard';
import { StudentCoursesComponent } from '../components/student-courses/student-courses.component';
import { CourseComponent } from '../components/course/course.component';

const routes: Routes = [
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'course/:id', component: CourseComponent, canActivate: [AuthGuard] , data: {
                    roles: ['Admin', 'Student']
                  }
                },
  { path: 'students', component: StudentsComponent , canActivate: [AuthGuard],
                  data: {
                    roles: ['Admin']
                  }
                },
  { path: 'courseManage', component: AdminCoursesComponent , canActivate: [AuthGuard],
                data: {
                  roles: ['Admin']
                }
              },
  { path: 'studentInformation', component: StudentInformationComponent, canActivate: [AuthGuard],
  data: {
    roles: ['Student']
  }},
  { path: 'studentCourse', component: StudentCoursesComponent, canActivate: [AuthGuard],
  data: {
    roles: ['Student']
  }},


  { path: '', redirectTo: '/students', pathMatch: 'full' }
];

@NgModule({

  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
