import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from './models/employee.model';
import { CreateEmployeeModel } from './models/create-employee.model';
import { EmployeesDetailsModel } from './models/employees-details.model';
import { UpdateEmployeeModel } from './models/update-employee.model';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {

  http = inject(HttpClient);
  apiUrl: string = "https://localhost:7249/api/Employee";

  getEmployee():Observable<Employee[]>{
    return this.http.get<Employee[]>(this.apiUrl);
  }
  
  addEmployee(createEmployee: CreateEmployeeModel): Observable<Employee> {
    return this.http.post<Employee>(this.apiUrl,  createEmployee);
  }

  getEmployeesDetails(id: number): Observable<EmployeesDetailsModel> {
    return this.http.get<EmployeesDetailsModel>(`${this.apiUrl}/${id}/details`);
  }

  getEmployeeForUpdate(id:number |null): Observable<UpdateEmployeeModel>{

    return this.http.get<UpdateEmployeeModel>(`${this.apiUrl}/${id}/edit`);
  }

  updateEmployee(id: number |null, employee:UpdateEmployeeModel): Observable<UpdateEmployeeModel>{
    return this.http.put<UpdateEmployeeModel>(`${this.apiUrl}/${id}`, employee);
  }

  deleteEmployee(id:number|null):Observable<void>{
     return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
