import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import {EmployeeService } from '../employee';
import { Employee } from '../models/employee.model';

@Component({
  imports: [],
  selector: 'app-employee-list',
  styleUrl: './employee-list.css',
  templateUrl: './employee-list.html',
})
export class EmployeeList implements OnInit {


    employees: Employee[] = []

    constructor(private employeeService: EmployeeService, private cdr: ChangeDetectorRef){}

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
}
