import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from './models/employee.model';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {

  http = inject(HttpClient);
  apiUrl: string = "https://localhost:7249/api/Employee";

  getEmployee():Observable<Employee[]>{
    return this.http.get<Employee[]>(this.apiUrl);
  }

}
