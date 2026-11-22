import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { EmployeeService } from '../employee';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { EmployeesDetailsModel } from '../models/employees-details.model';
import { DatePipe } from '@angular/common';

@Component({
  imports: [DatePipe, RouterLink],
  selector: 'app-employee-details',
  styleUrl: './employee-details.css',
  templateUrl: './employee-details.html',
})
export class EmployeeDetails implements OnInit {

  employee: EmployeesDetailsModel |null = null;

  constructor(private employeeService: EmployeeService, private activatedRoute:ActivatedRoute, private cdr: ChangeDetectorRef){}

  ngOnInit(): void {
    this.getEmployeesDetails();
  }
  getEmployeesDetails(){

    const id = Number(this.activatedRoute.snapshot.paramMap.get("id"));

    this.employeeService.getEmployeesDetails(id).subscribe({
      next:(data)=>{
        this.employee = data;
        console.log("Employee details:", this.employee);
        this.cdr.detectChanges();
      }, 
      error:(error)=>{
      console.error("error fetching employee:", error);
      }
    });
  }

}

