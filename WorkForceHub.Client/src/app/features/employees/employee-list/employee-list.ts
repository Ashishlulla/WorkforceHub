import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import {EmployeeService } from '../employee';
import { Employee } from '../models/employee.model';
import { Router, RouterLink } from '@angular/router';
import { DepartmentService } from '../../departments/department';
import { ShiftService } from '../../shifts/shift';
import { Department } from '../../departments/models/department.model';
import { Shift } from '../../shifts/models/shift.model';

@Component({
  imports: [RouterLink],
  selector: 'app-employee-list',
  styleUrl: './employee-list.css',
  templateUrl: './employee-list.html',
})
export class EmployeeList implements OnInit {


    employees: Employee[] = [];

    constructor(private employeeService: EmployeeService, private departmentService: DepartmentService, private shiftService: ShiftService, private cdr: ChangeDetectorRef, private router: Router){}

    ngOnInit(): void {
      this.getEmployees();
    }
      
    getEmployees(){
      this.employeeService.getEmployee().subscribe({
        next:(data)=>
        {
          this.employees = data;
          this.cdr.detectChanges();
          console.log(this.employees);
        }, 
      error:(error)=>{
        console.log("Error Fetching employees: ", error);
      }})
    }

    getEmployeesDetails(id:number){
      this.router.navigate([`/employees/${id}`]);
    }
}
