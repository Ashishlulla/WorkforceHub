import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Department } from './models/department.model';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  http = inject(HttpClient);

  apiUrl: string = "https://localhost:7249/api/Department";

  geDepartments(): Observable<Department[]>{  
    return this.http.get<Department[]>(this.apiUrl);
  }
}
