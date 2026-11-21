import { Routes } from '@angular/router';
import { EmployeeList } from './features/employees/employee-list/employee-list';
import { CreateEmployee } from './features/employees/create-employee/create-employee';

export const routes: Routes = [
    {path:"employees", component: EmployeeList},
    {path: '', redirectTo: 'employees', pathMatch: 'full'},
    {path:"employees/create", component: CreateEmployee}
];
