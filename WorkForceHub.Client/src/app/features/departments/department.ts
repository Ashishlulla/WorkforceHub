import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Department } from './models/department.model';
import { CreateDepartmentModel } from './models/create-department.model';
import { DepartmentDetailsModel } from './models/department-details.model';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  http = inject(HttpClient);

  apiUrl: string = "https://localhost:7249/api/Department";

  getDepartments(): Observable<Department[]>{  
    return this.http.get<Department[]>(this.apiUrl);
  }

  createDepartment(department: CreateDepartmentModel): Observable<Department>{

    return this.http.post<Department>(this.apiUrl, department);
  }

  getDepartmentDetails(id: number):Observable<DepartmentDetailsModel>{
    return this.http.get<DepartmentDetailsModel>(`${this.apiUrl}/details/${id}`);
  }
}
