import { RouterModule, Routes } from '@angular/router';
import { StudentInformationComponent } from '../student-information/student-information.component';
import { StudentComponent } from '../student/student.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

const routes: Routes = [
  { path: "students", component: StudentComponent },
  { path: "studentInformation", component: StudentInformationComponent }
];

@NgModule({

  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
