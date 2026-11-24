import { Routes } from '@angular/router';
import { EmployeeList } from './features/employees/employee-list/employee-list';
import { CreateEmployee } from './features/employees/create-employee/create-employee';
import { EmployeeDetails } from './features/employees/employee-details/employee-details';
import { DepartmentList } from './features/departments/department-list/department-list';
import { CreateDepartment } from './features/departments/create-department/create-department';
import { DepartmentDetails } from './features/departments/department-details/department-details';

export const routes: Routes = [
    //Employees routees configurations
    {path:"employees", component: EmployeeList},
    {path: '', redirectTo: 'employees', pathMatch: 'full'},
    {path:"employees/create", component: CreateEmployee},
    {path:'employees/:id', component:EmployeeDetails},
   {path:'employees/:id/edit', component: CreateEmployee},
  
   //departments routes configurations
   {path:'departments', component: DepartmentList},
   {path:'departments/create-department', component: CreateDepartment},
   {path:'departments/:id', component: DepartmentDetails}
];
