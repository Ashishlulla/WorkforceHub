import { Routes } from '@angular/router';
import { EmployeeList } from './features/employees/employee-list/employee-list';
import { CreateEmployee } from './features/employees/create-employee/create-employee';
import { EmployeeDetails } from './features/employees/employee-details/employee-details';
import { DepartmentList } from './features/departments/department-list/department-list';
import { CreateDepartment } from './features/departments/create-department/create-department';
import { DepartmentDetails } from './features/departments/department-details/department-details';
import { ShiftList } from './features/shifts/shift-list/shift-list';
import { ShiftsDetails } from './features/shifts/shifts-details/shifts-details';
import { CreateShift } from './features/shifts/create-shift/create-shift';

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
   {path:'departments/:id', component: DepartmentDetails},
   {path:'departments/edit-department/:id', component: CreateDepartment},

   //Shift routes Configurations
   { path: 'shifts', component: ShiftList },
{ path: 'shifts/create-shift', component: CreateShift },
{ path: 'shifts/:id', component: ShiftsDetails }
];
